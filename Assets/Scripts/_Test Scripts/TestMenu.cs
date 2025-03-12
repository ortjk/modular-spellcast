using System;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    public Inventory inventory;
    public SpellMenu menu;
    public Wand wand;

    public Spell[] spells;
    
    private void Awake()
    {
        foreach (var s in spells)
        {
            inventory.AddSpell(s);
        }
    }

    private void Start()
    {
        menu.Open(inventory, wand);
    }
}
