using UnityEngine;
using System.Collections.Generic;

public class CountSpell: Spell
{
    private int _count = 0;

    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        result.ToCast.Enqueue(this);
        result.ManaCost += _mana;
        result.Cooldown += _cooldown;
    }
    
    public override void Cast(Vector3 direction)
    {
        _count++;
        Debug.Log("Count is: " + _count);
    }
}
