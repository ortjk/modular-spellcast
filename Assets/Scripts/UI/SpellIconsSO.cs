using UnityEngine;

[System.Serializable]
public struct SpellIcon
{
    public string spellName;
    public Spell spellType;
    public GameObject iconPrefab;
}

[CreateAssetMenu(fileName = "SpellIconsSO", menuName = "Scriptable Objects/SpellIconsSO")]
public class SpellIconsSO : ScriptableObject
{
    public SpellIcon[] spellIcons;
}
