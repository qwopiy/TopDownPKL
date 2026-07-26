using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopUIController : MonoBehaviour
{
    [HideInInspector] public List<ShopCard> shopCards;
    private Image blankImage;

    [Header("Details Card")]
    public TextMeshProUGUI cardName;
    public Image cardImage;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI cardPrice;

    private void Start()
    {
        blankImage = cardImage; // Store the initial blank image for resetting
    }

    public void SelectCard(ShopCard selectedCard)
    {
        foreach (var card in shopCards)
        {
            card.UnHighlightCard();
        }
        selectedCard.HighlightCard();

        cardName.text = selectedCard.upgradeData.upgradeName;
        cardImage.sprite = selectedCard.upgradeData.upgradeIcon;
        cardDescription.text = selectedCard.upgradeData.description;
        cardPrice.text = selectedCard.upgradeData.upgradePrice.ToString();
    }

    public void DeselectAllCards()
    {
        foreach (var card in shopCards)
        {
            card.UnHighlightCard();
        }

        cardName.text = "";
        cardImage.sprite = blankImage.sprite;
        cardDescription.text = "";
        cardPrice.text = "";
    }

    public void ConfirmSelection(ShopCard selectedCard)
    {
        if (selectedCard != null)
        {
            DeselectAllCards();
            selectedCard.gameObject.SetActive(false);
        }
    }

    public void ResetShop()
    {
        foreach (var card in shopCards)
        {
            card.gameObject.SetActive(true);
            card.UnHighlightCard();
        }
    }
}