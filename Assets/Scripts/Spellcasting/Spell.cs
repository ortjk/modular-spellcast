using UnityEngine;

public abstract class Spell: MonoBehaviour
{
    [Header("Spell Data")]
    [SerializeField] protected SpellStatsSO _spellStats;
    [SerializeField] protected int _spellID;
    
    public float Cooldown { get; private set; }
    public int Mana { get; private set; }
    
    protected SpellStat _spellStat;

    public abstract void Cast(Vector3 direction);

    protected virtual void Awake()
    {
        _spellStat = _spellStats.spellStats[_spellID];
        Cooldown = _spellStat.cooldown;
        Mana = _spellStat.mana;
    }
}
