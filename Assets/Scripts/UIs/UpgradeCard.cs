using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [Header("Upgrade Data")]
    public UpgradeDataSO upgradeData;

    [Header("UI Elements")]
    public TextMeshProUGUI upgradeNameText;
    public Image upgradeImage;
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
    private void OnEnable()
    {
        SetUpgradeData();
    }

    public void SetUpgradeData()
    {
        upgradeNameText.text = upgradeData.upgradeName;
        upgradeImage.sprite = upgradeData.upgradeIcon;
        upgradeDescriptionText.text = upgradeData.description;
    }

    public UpgradeDataSO GetUpgradeData()
    {
        return upgradeData;
    }
}
