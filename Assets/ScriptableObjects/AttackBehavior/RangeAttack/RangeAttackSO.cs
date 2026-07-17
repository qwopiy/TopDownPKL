using UnityEngine;

[CreateAssetMenu(fileName = "NewRangeAttack", menuName = "ScriptableObjects/Attack Behaviors/Range")]
public class RangeAttackSO : AttackBehaviorSO
{
    [SerializeField] private GameObject projectilePrefabPlaceholder; // Bisa diisi kapsul kecil
    [SerializeField] private float projectileSpeed = 15f;

    public override void ExecuteAttack(GameObject attacker, Transform target, float damage)
    {
        Debug.Log($"{attacker.name} menembakkan proyektil ke {target.name}!");

        // Logika Jarak Jauh: Spawn peluru placeholder
        // Peluru tersebut nantinya yang akan membawa data 'damage' dan berjalan ke arah target
    }
}
