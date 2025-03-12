using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellMenu: MonoBehaviour
{
    public SpellIconsSO spellIconsSO;
    
    public DragSlot[] inventorySlots = {};
    public DragSlot[] wandSlots = {};
    
    public RectTransform defaultLayer;
    
    private Dictionary<Type, GameObject> _iconMap = new Dictionary<Type, GameObject>();

    public void Open(Inventory inventory, Wand wand)
    {
        (Type, uint)[] counts = inventory.GetCounts();
        for (int i = 0; i < counts.Length; i++)
        {
            var g = Instantiate(_iconMap[counts[i].Item1], inventorySlots[i].transform.position, Quaternion.identity);
            g.transform.SetParent(defaultLayer);
            
            DragObject drag = g.GetComponent<DragObject>();
            drag.slot = inventorySlots[i].GetComponent<DragSlot>();
            drag.Init();
        }

        for (int i = 0; i < wand.numSlots; i++)
        {
            wandSlots[i].enabled = true;
            if (i < wand.spells.Length)
            {
                var g = Instantiate(_iconMap[wand.spells[i].GetType()], wandSlots[i].transform.position, Quaternion.identity);
                g.transform.SetParent(defaultLayer);
            
                DragObject drag = g.GetComponent<DragObject>();
                drag.slot = wandSlots[i].GetComponent<DragSlot>();
                drag.Init();
            }
        }
    }

    public void Close()
    {
        
    }

    private void Awake()
    {
        // initialize map
        foreach (SpellIcon i in spellIconsSO.spellIcons)
        {
            Debug.Log(i.spellType.GetType());
            _iconMap.Add(i.spellType.GetType(), i.iconPrefab);
        }
    }
}
