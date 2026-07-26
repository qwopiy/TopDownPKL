using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObjects/Upgrade Data")]
public class UpgradeDataSO : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite upgradeIcon;
    public int upgradePrice;

    public List<StatsModifier> modifiers;
}