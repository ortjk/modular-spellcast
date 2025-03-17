using UnityEngine;
using System.Collections.Generic;

public class CountSpell: Spell
{
    private int _count = 0;

    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);
        
        while (modifiers.Count > 0)
        {
            modifiers.Dequeue().ModifySpell(this);
        }
    }
    
    public override void Cast(Vector3 direction)
    {
        PreCast?.Invoke(direction);
        
        _count++;
        Debug.Log("Count is: " + _count);
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
