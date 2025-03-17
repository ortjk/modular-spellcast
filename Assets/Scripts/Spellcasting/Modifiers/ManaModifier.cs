using UnityEngine;

public class ManaModifier: ModifierSpell
{
    public override void ModifySpell(Spell spell)
    {
        
    }

    protected override void PreQuery(Spell[] otherSpells, QueryResult result)
    {
        base.PreQuery(otherSpells, result);
        result.ManaCost -= _mana * 2;
    }
}
