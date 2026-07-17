using UnityEngine;

[RequireComponent(typeof(CompanionAIController))]
public class RecruitableCompanion : MonoBehaviour
{
    [Header("SO Assets")]
    [SerializeField] private SquadAnchorSO squadSet;
    [SerializeField] private GameEventSO onCompanionRecruitedEvent; 

    private CompanionAIController aiController;
    private bool isRecruited = false;

    private void Start()
    {
        aiController = GetComponent<CompanionAIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRecruited) return;

        if (other.CompareTag("Player"))
        {
            AddToSquad();
        }
    }

    private void AddToSquad()
    {
        isRecruited = true;

        aiController.Recruit();

        if (squadSet != null) squadSet.Add(gameObject);

        if (onCompanionRecruitedEvent != null) onCompanionRecruitedEvent.Raise();

        this.enabled = false;

        Debug.Log($"{gameObject.name} BERHASIL DIREKRUT! Jumlah Squad saat ini: {squadSet.members.Count}");
    }
}