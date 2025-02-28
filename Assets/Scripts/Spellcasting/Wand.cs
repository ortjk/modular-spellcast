using Unity.VisualScripting;
using UnityEngine;

public class Wand : MonoBehaviour
{
    [Header("Spell Data")]
    public Spell[] spells;
    public int numSlots = 1;
    
    [Header("Wand Stats")]
    public float reloadTime = 0f;

    private int _currentSlot = 0;
    private float _cooldown = 0f;

    public void Use(Vector3 direction)
    {
        if (_cooldown > 0f || spells.Length == 0)
        {
            Debug.Log("Reached " + _cooldown);
            return;
        }
        
        Spell spell = spells[_currentSlot];
        spell.Cast(direction);
        _cooldown += spell.Cooldown;
        
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

    public int GetManaForCast()
    {
        if (spells.Length == 0)
        {
            return 0;
        }

        return spells[_currentSlot].Mana;
    }

    private void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
    }
}
