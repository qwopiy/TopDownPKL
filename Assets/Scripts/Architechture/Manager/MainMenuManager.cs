using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject ChaaracterSelect;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        ChaaracterSelect.SetActive(false);
    }

    public void ShowCharacterSelect()
    {
        mainMenu.SetActive(false);
        ChaaracterSelect.SetActive(true);
    }
}
