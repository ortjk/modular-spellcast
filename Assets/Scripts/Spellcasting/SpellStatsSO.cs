using System;
using UnityEngine;

[Serializable]
public struct SpellStat
{
    public string spellName;
    public float damage;
    public float cooldown;
    public int mana;
    public float speed;
    public float range;
    public float area;
    public bool isModifier;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "SpellStatsSO", menuName = "Scriptable Objects/SpellStatsSO")]
public class SpellStatsSO : ScriptableObject
{
    public SpellStat[] spellStats;
}
