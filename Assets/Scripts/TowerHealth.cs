using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private UnitStatsManager Stats => GetComponent<UnitStatsManager>();

    private void Update()
    {
        healthBar.maxValue = Stats.CharacterData.maxHealth;
        healthBar.value = Stats.currentHealth;
    }
}