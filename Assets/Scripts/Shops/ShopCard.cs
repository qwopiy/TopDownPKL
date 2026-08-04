using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType { Upgrade, Companion }

public class ShopCard : MonoBehaviour
{
    private static readonly int HighlightHash = Animator.StringToHash("Highlight");
    [Header("Shop Data")]
    public UpgradeType upgradeType;
    public UpgradeDataSO upgradeData;

    [Header("UI Elements")]
    public TextMeshProUGUI cardName;
    public Image cardImage;
    public TextMeshProUGUI cardPrice;

    [Header("Feedback Settings")]
    public GameObject cardHighlighter;
    public Animator animator;

    public void HighlightCard()
    {
        animator.SetBool(HighlightHash, true);
        cardHighlighter.SetActive(true);
    }

    public void UnHighlightCard()
    {
        animator.SetBool(HighlightHash, false);
        cardHighlighter.SetActive(false);
    }
    private void OnEnable()
    {
        SetUpgradeData();
    }

    public void SetUpgradeData()
    {
        cardName.text = upgradeData.upgradeName;
        cardImage.sprite = upgradeData.upgradeIcon;
        cardPrice.text = upgradeData.upgradePrice.ToString();
    }

    public void SetRandomUpgrade()
    {
        Debug.Log(UpgradeRandomizerManager.Instance.gameObject.name);
        upgradeData = UpgradeRandomizerManager.Instance.GetRandomUpgrade();
    }

    public void SetRandomCompanion()
    {
        upgradeData = UpgradeRandomizerManager.Instance.GetRandomCompanion();
    }

    public UpgradeDataSO GetUpgradeData()
    {
        return upgradeData;
    }

    public void ApplyCard(SquadAnchorSO squadAnchor)
    {
        if (upgradeType == UpgradeType.Companion && upgradeData is CompanionDataSO companionData)
        {
            // Handle companion upgrade
            companionData.SpawnCompanion();
            // You can add additional logic here to manage the companion instance if needed
        }
        else
        {
            // Handle regular upgrade
            squadAnchor.ApplyUpgrade(upgradeData);
        }
    }
}
