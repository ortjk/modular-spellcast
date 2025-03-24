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

    public float Cooldown { get; private set; } = 0f;

    private int _currentSlot = 0;
    public float _currentCooldown;
    public float _maxCooldown;
    

    void Start()
    {
        _currentCooldown = 0f;
        _maxCooldown = 1f;
    }

    public void Use(Vector3 direction, Vector3 origin, ref float mana)
    {
        if (_currentCooldown > 0f || spells.Length == 0)
        {
            Debug.Log("Cooldown");
            AudioManager._audioManager.PlaySoundEffect("SpellFail");
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
            Debug.Log("Not enough mana: " + mana + " / " + sqresult.ManaCost);
            
            foreach (Spell s in spells)
            {
                s.Reset();
            }
            return;
        }
        
        sqresult.ManaCost = Mathf.Clamp(sqresult.ManaCost, 0, sqresult.ManaCost);
        mana -= sqresult.ManaCost;

        _maxCooldown += sqresult.Cooldown;
        while (sqresult.ToCast.Count > 0)
        {
            sqresult.ToCast.Dequeue().Cast(direction, origin);
        }
        
        _currentSlot += sqresult.Count;
        if (_currentSlot >= spells.Length)
        {
            _currentSlot -= spells.Length;
            _maxCooldown += reloadTime;
        }
        _currentCooldown = _maxCooldown;
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
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }
}
