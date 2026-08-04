using TMPro;
using UnityEngine;

public class GoldTextUIController : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        PlayerUpgradeManager.Instance.CoinCollected += UpdateCoinAmount;
    }

    private void OnDisable()
    {
        PlayerUpgradeManager.Instance.CoinCollected -= UpdateCoinAmount;
    }
    public void UpdateCoinAmount(int amount)
    {
        textMeshProUGUI.text = $"Current Gold: {amount}";
    }
}