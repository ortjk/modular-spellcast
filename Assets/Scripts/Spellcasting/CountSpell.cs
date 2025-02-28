using UnityEngine;
using System.Collections.Generic;

public class CountSpell: Spell
{
    private int _count = 0;

    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);
    }
    
    public override void Cast(Vector3 direction)
    {
        PreCast();
        
        _count++;
        Debug.Log("Count is: " + _count);
    }
}
