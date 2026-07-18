using JetBrains.Annotations;
using UnityEngine;

public class UISkillTest : MonoBehaviour
{
    [SerializeField] private TransformAnchorSO transformAnchorSO;

    private GameObject player;
    private ActiveSkillManager activeSkillManager;

    void Start()
    {
        if (transformAnchorSO != null && transformAnchorSO.value != null)
        {
            player = transformAnchorSO.value.gameObject;
            activeSkillManager = player.GetComponent<ActiveSkillManager>();
        }
    }

    public void CastFirstSkill() { 
       activeSkillManager?.CastSkill(0);
    }
}
