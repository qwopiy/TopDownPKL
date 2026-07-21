using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSelectAnchor", menuName = "ScriptableObjects/Anchor/CharacterSelectAnchor")]
public class CharacterSelectAnchor : ScriptableObject 
{
    public GameObject selectedCharacter;

    public void SetSelectedCharacter(GameObject character)
    {
        if(character == null) return;
        if (selectedCharacter != null && selectedCharacter == character) return; 
        Debug.Log("Selected character set to: " + character.name);
        selectedCharacter = character;
    }

    public void ClearSelectedCharacter()
    {
        selectedCharacter = null;
    }

    private void OnDisable()
    {
        ClearSelectedCharacter();
    }
}
