using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIController : MonoBehaviour
{
    public SquadAnchorSO squadAnchor;
    public List<GameObject> upgradeCardsObj;
    private UpgradeCard selectedUpgrade;

    public void CreateUpgradeCard(int amount) // masih base upgrade, nanti ubah jadi random?
    {
        for (int i = 0; i < amount; i++)
        {
            // TODO: Add logic to select a random upgrade from available upgrades
            upgradeCardsObj[i].GetComponent<UpgradeCard>().SetUpgradeData();
        }
    }

    public void SelectUpgrade(UpgradeCard selectedCard)
    {
        foreach (var card in upgradeCardsObj)
        {
            card.GetComponent<UpgradeCard>().UnHighlightCard();
        }

        Debug.Log("Selected Upgrade: " + selectedCard.name);

        selectedCard.HighlightCard();
        selectedUpgrade = selectedCard;
    }

    public void ConfirmUpgrade()
    {
        if (selectedUpgrade != null)
        {
            UpgradeDataSO selectedUpgradeData = selectedUpgrade.GetUpgradeData();
            Debug.Log("Confirmed Upgrade: " + selectedUpgradeData.upgradeName);
            if (squadAnchor != null)
            {
                squadAnchor.ApplyUpgrade(selectedUpgradeData);
                foreach (var card in upgradeCardsObj)
                {
                    card.GetComponent<UpgradeCard>().UnHighlightCard();
                }
            }
        }
        else
        {
            Debug.LogWarning("No upgrade selected to confirm.");
        }
    }
}
