using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIController : MonoBehaviour
{
    public List<GameObject> upgradeCardsObj;

    public void CreateUpgradeCard(UpgradeDataSO upgradeData, int amount) // masih base upgrade, nanti ubah jadi random?
    {
        for (int i = 0; i < amount; i++)
        {
            upgradeCardsObj[i].SetActive(true);
            upgradeCardsObj[i].GetComponent<UpgradeCard>().SetUpgradeData(upgradeData);
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
    }

    public void ClearUpgradeCards()
    {
        foreach (var card in upgradeCardsObj)
        {
            card.SetActive(false);
        }
    }
}
