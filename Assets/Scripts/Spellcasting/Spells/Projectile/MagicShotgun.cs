using System.Collections.Generic;
using UnityEngine;

public class MagicShotgun : Spell
{
    [SerializeField]
    private SpellList _spells;
    public SpellIconsSO spellIcons;
    private List<int> fTierSpellsIndex = new List<int>();
    private System.Random _randomInt = new System.Random();

    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);

        while (modifiers.Count > 0)
        {
            modifiers.Dequeue().ModifySpell(this);
        }
    }
    
    public override void Cast(Vector3 direction, Vector3 origin)
    {
        // This spell doesnt work entities are instantiated but at the wrong location and without movement
        for(int i = 0; i < _spellStats.spellStats.Length; i++)
        {
            if(_spellStats.spellStats[i].spellTier == SpellTier.F)
            {
                fTierSpellsIndex.Add(i);
            }
        }
        for(int j = 0; j < 3; j++)
        {
            int num =_randomInt.Next(0, fTierSpellsIndex.Count);
            int spellIndex = fTierSpellsIndex[num];
            _spellStat = _spellStats.spellStats[spellIndex];
            spellIcons.spellIcons[spellIndex].spellType.SetStats();
            spellIcons.spellIcons[spellIndex].spellType.Cast(direction, origin);
        }
    }

    public override void Reset()
    {
        PreCast = null;
        MidCast = null;
        PostCast = null;
        PreCast = (Vector3 direction) => { Queried = false; };
        Queried = false;
        modifiers.Clear();
    }
}
