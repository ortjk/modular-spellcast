using UnityEngine;

public class CooldownReduction: ModifierSpell
{
    public override void ModifySpell(Spell spell)
    {
        
    }
    
    protected override void PreQuery(Spell[] otherSpells, QueryResult result)
    {
        base.PreQuery(otherSpells, result);
        result.Cooldown -= _spellStat.cooldown * 2;
    }
}
