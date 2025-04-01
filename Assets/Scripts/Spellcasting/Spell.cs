using UnityEngine;
using System.Collections.Generic;

public abstract class Spell: MonoBehaviour
{
    public class QueryResult
    {
        public Queue<Spell> ToCast = new Queue<Spell>();
        public int ManaCost = 0;
        public float Cooldown = 0f;
        public int Count = 0;
    }

    public delegate void SpellStateHandler(Vector3 direction);
    
    [Header("Spell Data")]
    [SerializeField] protected SpellStatsSO _spellStats;
    [SerializeField] protected int _spellID;
    
    [System.NonSerialized] public Queue<ModifierSpell> modifiers = new Queue<ModifierSpell>();
    public SpellStateHandler PreCast;
    public SpellStateHandler MidCast;
    public SpellStateHandler PostCast;

    public bool Queried { get; protected set; } = false;
    public bool IsModifier { get; private set; }
    
    protected SpellStat _spellStat;
    protected int _mana;
    protected float _cooldown;

    public abstract void Query(Spell[] otherSpells, QueryResult result);
    public abstract void Cast(Vector3 direction, Vector3 origin);
    public abstract void Reset();

    protected virtual void PreQuery(Spell[] otherSpells, QueryResult result)
    {
        Queried = true;
        
        result.ToCast.Enqueue(this);
        result.ManaCost += _mana;
        result.Cooldown += _cooldown;
        result.Count += 1;
    }
    
    protected virtual void Awake()
    {
        _spellStat = _spellStats.spellStats[_spellID];
        IsModifier = _spellStat.isModifier;
        _cooldown = _spellStat.cooldown;
        _mana = _spellStat.mana;
        Debug.Log("awake");
        PreCast += (Vector3 direction) => { Queried = false; };
    }

    public virtual void SetStats()
    {
        _spellStat = _spellStats.spellStats[_spellID];
        IsModifier = _spellStat.isModifier;
        _cooldown = _spellStat.cooldown;
        _mana = _spellStat.mana;
    }
}
