using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class ModifierSpell: Spell
{
    [System.NonSerialized] public List<Spell> modifiedSpells = new List<Spell>();
    
    public abstract void ModifySpell(Spell spell);

    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        for (int i = 0; i < otherSpells.Length; i++)
        {
            if (!otherSpells[i].Queried)
            {
                PreQuery(otherSpells, result);

                while (modifiers.Count > 0)
                {
                    ModifierSpell mod = modifiers.Dequeue();
                    otherSpells[i].modifiers.Enqueue(mod);
                    
                    mod.modifiedSpells.RemoveAt(mod.modifiedSpells.Count - 1);
                    mod.modifiedSpells.Add(otherSpells[i]);
                }
                otherSpells[i].modifiers.Enqueue(this);
                modifiedSpells.Add(otherSpells[i]);
                
                Spell[] newOtherSpells = new Spell[otherSpells.Length - 1 - i];
                Array.Copy(otherSpells, i + 1, newOtherSpells, 0, otherSpells.Length - 1 - i);
                
                otherSpells[i].Query(newOtherSpells, result);
                return;
            }
        }
        
        // no modification target was found, so previous modifiers must be reset
        while (modifiers.Count > 0)
        {
            ModifierSpell mod = modifiers.Dequeue();
            mod.modifiedSpells.RemoveAt(mod.modifiedSpells.Count - 1);
            mod.UnQuery(result);
        }
    }
    
    public override void Cast(Vector3 direction)
    {
        PreCast?.Invoke(direction);
    }
    
    public void UnQuery(QueryResult result)
    {
        Queried = false;
        result.ToCast.Dequeue();
        result.ManaCost -= _mana;
        result.Cooldown -= _cooldown;
        result.Count -= 1;
    }
}
