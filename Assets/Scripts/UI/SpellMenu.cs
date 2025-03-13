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
        this.gameObject.SetActive(true);
        
        (Type, uint)[] counts = inventory.GetCounts();
        for (int i = 0; i < counts.Length; i++)
        {
            var g = Instantiate(_iconMap[counts[i].Item1], inventorySlots[i].transform.position, Quaternion.identity);
            g.transform.SetParent(defaultLayer);
            
            DragObject drag = g.GetComponent<DragObject>();
            drag.slot = inventorySlots[i].GetComponent<DragSlot>();
            drag.slot.Assign(drag);
            drag.Init();
        }

        for (int i = 0; i < wand.numSlots; i++)
        {
            wandSlots[i].gameObject.SetActive(true);
            if (i < wand.spells.Length)
            {
                var g = Instantiate(_iconMap[wand.spells[i].GetType()], wandSlots[i].transform.position, Quaternion.identity);
                g.transform.SetParent(defaultLayer);
            
                DragObject drag = g.GetComponent<DragObject>();
                drag.slot = wandSlots[i].GetComponent<DragSlot>();
                drag.slot.Assign(drag);
                drag.Init();
            }
        }
    }

    public void Close(Inventory inventory, Wand wand)
    {
        foreach (DragSlot slot in inventorySlots)
        {
            if (slot.Occupied)
            {
                DragObject drag = slot.Contained;
                slot.UnAssign(drag);
                Destroy(drag.gameObject);
            }
        }
        
        for (int i = 0; i < wand.numSlots; i++)
        {
            if (wandSlots[i].Occupied)
            {
                DragObject drag = wandSlots[i].Contained;
                wandSlots[i].UnAssign(drag);
                Destroy(drag.gameObject);
            }
            wandSlots[i].gameObject.SetActive(false);
        }
        
        this.gameObject.SetActive(false);
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
