using System;
using UnityEngine;

public class PlayerInstantiator : MonoBehaviour
{
    [SerializeField] private CharacterSelectAnchor characterSelectAnchor;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if(characterSelectAnchor.selectedCharacter != null)
        {
            Instantiate(characterSelectAnchor.selectedCharacter, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No character selected to instantiate.");
        }
    }

    private void OnDisable()
    {
        characterSelectAnchor.ClearSelectedCharacter();
    }
}
