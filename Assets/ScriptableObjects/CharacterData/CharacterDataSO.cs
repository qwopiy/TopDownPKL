using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/Character Data")]
public class CharacterDataSO : ScriptableObject
{
    [Header("Base Info")]
    public string characterName;

    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 15f;

    [Header("Combat Stats")]
    public float attackDamage = 10f;
    public float attackRange = 3f;
    public float attackRate = 1f; 
}