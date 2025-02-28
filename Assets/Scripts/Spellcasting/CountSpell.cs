using UnityEngine;
using System.Collections.Generic;

public class CountSpell: Spell
{
    private int _count = 0;
    
    public override void Cast(Vector3 direction, Spell[] otherSpells)
    {
        _count++;
        Debug.Log("Count is: " + _count);
    }
}
