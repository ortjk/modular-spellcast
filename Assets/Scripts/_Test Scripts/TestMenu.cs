using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMenu : MonoBehaviour
{
    public Inventory inventory;
    public SpellMenu menu;
    public Wand wand;

    public Spell[] spells;

    private bool _open = false;

    private void OnInteract(InputValue value)
    {
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        _open = !_open;
        if (_open)
        {
            menu.Open(inventory, wand);
        }
        else
        {
            menu.Close(inventory, wand);
        }
    }
    
    private void Awake()
    {
        foreach (var s in spells)
        {
            inventory.AddSpell(s);
        }
    }
}
