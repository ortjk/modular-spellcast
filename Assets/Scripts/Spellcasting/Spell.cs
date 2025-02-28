using UnityEngine;
using System.Collections.Generic;

public abstract class Spell: MonoBehaviour
{
    public struct QueryResult
    {
        public Queue<Spell> ToCast;
        public int ManaCost;
        public float Cooldown;
    }
    
    [Header("Spell Data")]
    [SerializeField] protected SpellStatsSO _spellStats;
    [SerializeField] protected int _spellID;
    
    [System.NonSerialized] public Queue<Spell> Modifiers = new Queue<Spell>();
    
    public bool IsModifier { get; private set; }
    
    protected SpellStat _spellStat;
    protected int _mana;
    protected float _cooldown;

    public abstract void Query(Spell[] otherSpells, QueryResult result);
    public abstract void Cast(Vector3 direction);

    protected virtual void Awake()
    {
        _spellStat = _spellStats.spellStats[_spellID];
        IsModifier = _spellStat.isModifier;
        _cooldown = _spellStat.cooldown;
        _mana = _spellStat.mana;
    }
}
