using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    void Start()
    {
        if(spellList.Count == 0)
        {
            MakeList();
        }   
    }

    public List<Type> spellList = new List<Type>();
    public void MakeList()
    {
        spellList.Add(typeof(DoubleSpell));
        spellList.Add(typeof(FireBolt));
        spellList.Add(typeof(MagicBolt));
        spellList.Add(typeof(MagicSpark));
        spellList.Add(typeof(SpeedModifierSpell));
        spellList.Add(typeof(ManaModifier));
        spellList.Add(typeof(IceShard));
        spellList.Add(typeof(RockThrow));
        spellList.Add(typeof(MeteorBlast));
    }
}
