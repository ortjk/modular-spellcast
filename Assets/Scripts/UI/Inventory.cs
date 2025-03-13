using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    private Dictionary<Type, uint> _unused = new Dictionary<Type, uint>();
    
    public void AddSpell<T>(T spell) where T: Spell
    {
        Type t = spell.GetType();
        if (!_unused.ContainsKey(t))
        {
            _unused.Add(t, 0);
        }
        _unused[t]++;
    }

    public void AddSpell(Type spell)
    {
        if (!_unused.ContainsKey(spell))
        {
            _unused.Add(spell, 0);
        }
        _unused[spell]++;
    }

    public void RemoveSpell<T>(T spell) where T : Spell
    {
        Type t = spell.GetType();
        if (_unused.ContainsKey(t) && _unused[t] > 0)
        {
            _unused[t]--;
        }
    }

    public (Type, uint)[] GetCounts()
    {
        (Type, uint)[] counts = new (Type, uint)[_unused.Count];
        int i = 0;
        foreach (Type t in _unused.Keys)
        {
            counts[i] = (t, _unused[t]);
            i++;
        }
        
        return counts;
    }

    public void Reset()
    {
        _unused.Clear();
    }
}
