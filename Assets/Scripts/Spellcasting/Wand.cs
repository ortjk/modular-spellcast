using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Wand : MonoBehaviour
{
    [Header("Spell Data")]
    public Spell[] spells;
    public int numSlots = 1;
    
    [Header("Wand Stats")]
    public float reloadTime = 0f;

    private int _currentSlot = 0;
    private float _cooldown = 0f;

    public void Use(Vector3 direction, ref int mana)
    {
        if (_cooldown > 0f || spells.Length == 0)
        {
            return;
        }
        
        Spell spell = spells[_currentSlot];
        
        Spell[] otherSpells = new Spell[spells.Length - 1];
        SetOtherSpells(otherSpells);
        
        var sqresult = new Spell.QueryResult();
        sqresult.ToCast = new Queue<Spell>();
        spell.Query(otherSpells, sqresult);

        if (sqresult.ManaCost > mana)
        {
            return;
        }

        _cooldown += sqresult.Cooldown;
        while (sqresult.ToCast.Count > 0)
        {
            sqresult.ToCast.Dequeue().Cast(direction);
        }
        
        _currentSlot += 1;
        if (_currentSlot >= spells.Length)
        {
            _currentSlot = 0;
            _cooldown += reloadTime;
        }
    }

    public void SetSpells(Spell[] newSpells)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            GameObject.Destroy(spells[i].GameObject());
        }
        
        spells = new Spell[newSpells.Length];
        for (int i = 0; i < newSpells.Length; i++)
        {
            spells[i] = newSpells[i];
            spells[i].transform.SetParent(transform);
        }
        _currentSlot = 0;
    }

    // gets all other spells than the current one in order of when they will be cast next
    //
    // e.g. for spells = [ A, B, C, D, E, F ], _currentSlot = 2
    // returns [ D, E, F, A, B ]
    private void SetOtherSpells(Spell[] otherSpells)
    {
        int offset = spells.Length - _currentSlot - 1;
        
        for (int i = 0; i < offset; i++)
        {
            otherSpells[i] = spells[i + _currentSlot + 1];
        }

        for (int i = 0; i < _currentSlot; i++)
        {
            otherSpells[i + offset] = spells[i];
        }
    }

    private void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
    }
}
