using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField] private CharacterSelectAnchor characterSelectAnchor;
    [SerializeField] private GameObject characterSelectGameObject;

    public void SelectCharacter()
    {
        if (characterSelectGameObject == null) return;
        characterSelectAnchor.SetSelectedCharacter(characterSelectGameObject);
    }
}
