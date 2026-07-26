using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private IMovementProvider movementProvider;

    private AutoAttacker autoAttacker;

    private ActiveSkillManager activeSkillManager;
    private PassiveSkillManager passiveSkillManager;

    private Animator animator;

    private int currentStateHash;

    public bool isAttacking = false;
    public bool isInAbility = false;

    public readonly int PlayerHurt = Animator.StringToHash("Hurt");
    public readonly int PlayerIdle = Animator.StringToHash("Idle");
    public readonly int PlayerWalk = Animator.StringToHash("Run");

    private void Awake()
    {
        animator = GetComponent<Animator>();

        autoAttacker = GetComponent<AutoAttacker>();

        movementProvider = GetComponent<IMovementProvider>();

        activeSkillManager = GetComponent<ActiveSkillManager>();

        passiveSkillManager = GetComponent<PassiveSkillManager>();
        
    }

    private void Start()
    {
        currentStateHash = PlayerIdle;

        autoAttacker.OnAttackExecuted += PlayAttackAnimation;
        if (activeSkillManager != null)
        {
            activeSkillManager.OnSkillExecuted += PlaySkillAnimation;
        }
        if (passiveSkillManager != null)
        {
            passiveSkillManager.OnSkillExecuted += PlaySkillAnimation;
        }
    }
        
    private void OnDisable()
    {
        autoAttacker.OnAttackExecuted -= PlayAttackAnimation;
        if (activeSkillManager != null)
        {
            activeSkillManager.OnSkillExecuted -= PlaySkillAnimation;
        }
        if (passiveSkillManager != null)
        {
            passiveSkillManager.OnSkillExecuted -= PlaySkillAnimation;
        }
    }

    void Update()
    {
        if (movementProvider != null)
        {
            HandleMovementAnimation();
        }
    }

    private void HandleMovementAnimation()
    {
        if (IsAnimationLocked()) return;
        if (isInAbility) return;

        if (movementProvider.IsMoving)
        {
            ChangeAnimationState(PlayerWalk);
        }  
        else
            ChangeAnimationState(PlayerIdle);
    }

    private void PlayHurt() => ChangeAnimationState(PlayerHurt);
    private void PlayAttackAnimation(int animHash)
    {
        ChangeAnimationState(animHash);
    }

    private void PlaySkillAnimation(int animHash)
    {
        ChangeAnimationState(animHash);
    }

    private void ChangeAnimationState(int newState)
    {
        if (currentStateHash == newState) return;

        animator.CrossFade(newState, 0.2f, layer: 0, 0f);   
        currentStateHash = newState;
    }

    private bool IsAnimationLocked()
    {
        if (currentStateHash != PlayerWalk && currentStateHash != PlayerIdle)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.shortNameHash != currentStateHash)
                return true;

            if (stateInfo.normalizedTime < 1f)
            {
                return true;
            }
        }

        return false;
    }
}
