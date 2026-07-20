using TMPro;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI upgradeNameText;
    //public Image upgradeImage;
    public TextMeshProUGUI upgradeDescriptionText;

    [Header("Feedback Settings")]
    public GameObject cardHighlighter;
    public Animator animator;

    public void HighlightCard()
    {
        animator.SetTrigger("Highlight");
        cardHighlighter.SetActive(true);
    }

    public void UnHighlightCard()
    {
        animator.SetTrigger("Base");
        cardHighlighter.SetActive(false);
    }

    public void SetUpgradeData(UpgradeDataSO upgradeData)
    {
        upgradeNameText.text = upgradeData.upgradeName;
        //upgradeImage.sprite = upgradeData.upgradeIcon;
        upgradeDescriptionText.text = upgradeData.description;
    }
}
