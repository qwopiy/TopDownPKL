using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public SquadAnchorSO squadAnchor;
    public List<GameObject> upgradeCardsObj;
    private ShopCard selectedUpgrade;
    private ShopUIController shopUIController;

    private void Start()
    {
        shopUIController = GetComponent<ShopUIController>();
        // Initialize the shop UI controller with the list of upgrade cards
        if (shopUIController != null)
        {
            shopUIController.shopCards = new List<ShopCard>();
            foreach (var cardObj in upgradeCardsObj)
            {
                ShopCard card = cardObj.GetComponent<ShopCard>();
                if (card != null)
                {
                    shopUIController.shopCards.Add(card);
                }
            }
        }

        ResetShop();
    }

    public void SelectUpgrade(ShopCard selectedCard)
    {
        shopUIController.SelectCard(selectedCard);

        Debug.Log("Selected Upgrade: " + selectedCard.name);

        selectedUpgrade = selectedCard;
    }

    public void ConfirmUpgrade()
    {
        if (selectedUpgrade != null)
        {
            UpgradeDataSO selectedUpgradeData = selectedUpgrade.GetUpgradeData();
            Debug.Log("Confirmed Upgrade: " + selectedUpgradeData.upgradeName);

            if (PlayerUpgradeManager.Instance.TryPurchaseUpgrade(selectedUpgradeData.upgradePrice))
            {
                Debug.Log("Upgrade purchased successfully.");
            }
            else
            {
                Debug.LogWarning("Not enough coins to purchase the upgrade.");
                return;
            }

            if (squadAnchor != null)
            {
                selectedUpgrade.ApplyCard(squadAnchor);

                shopUIController.ConfirmSelection(selectedUpgrade);
            }
        }
        else
        {
            Debug.LogWarning("No upgrade selected to confirm.");
        }
    }

    public void ResetShop()
    {
        shopUIController.ResetShop();
        selectedUpgrade = null;

        for (int i = 0; i < upgradeCardsObj.Count; i++)
        {
            if (i < 3)
            {
                upgradeCardsObj[i].GetComponent<ShopCard>().SetRandomUpgrade();
            }
            else
            {
                upgradeCardsObj[i].GetComponent<ShopCard>().SetRandomCompanion();
            }
        }
    }
}
