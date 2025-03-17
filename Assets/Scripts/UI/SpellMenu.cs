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
    private Dictionary<Type, GameObject> _prefabMap = new Dictionary<Type, GameObject>();

    public void Open(Inventory inventory, Wand wand)
    {
        this.gameObject.SetActive(true);
        
        (Type, uint)[] counts = inventory.GetCounts();
        int k = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            for (int j = 0; j < counts[i].Item2; j++)
            {
                var g = Instantiate(_iconMap[counts[i].Item1], inventorySlots[k].transform.position, Quaternion.identity);
                g.transform.SetParent(defaultLayer);
            
                DragObject drag = g.GetComponent<DragObject>();
                drag.type = counts[i].Item1;
                drag.slot = inventorySlots[k].GetComponent<DragSlot>();
                drag.slot.Assign(drag);
                drag.Init();

                k++;
            }
        }

        for (int i = 0; i < wand.numSlots; i++)
        {
            wandSlots[i].gameObject.SetActive(true);
            if (i < wand.spells.Length)
            {
                var g = Instantiate(_iconMap[wand.spells[i].GetType()], wandSlots[i].transform.position, Quaternion.identity);
                g.transform.SetParent(defaultLayer);
            
                DragObject drag = g.GetComponent<DragObject>();
                drag.type = wand.spells[i].GetType();
                drag.slot = wandSlots[i].GetComponent<DragSlot>();
                drag.slot.Assign(drag);
                drag.Init();
            }
        }
    }

    public void Close(Inventory inventory, Wand wand)
    {
        inventory.Reset();
        Queue<GameObject> wandSpells = new Queue<GameObject>();
        
        foreach (DragSlot slot in inventorySlots)
        {
            if (slot.Occupied)
            {
                DragObject drag = slot.Contained;
                slot.UnAssign(drag);
                inventory.AddSpell(drag.type);
                Destroy(drag.gameObject);
            }
        }
        
        for (int i = 0; i < wand.numSlots; i++)
        {
            if (wandSlots[i].Occupied)
            {
                DragObject drag = wandSlots[i].Contained;
                wandSlots[i].UnAssign(drag);
                wandSpells.Enqueue(_prefabMap[drag.type]);
                Destroy(drag.gameObject);
            }
            wandSlots[i].gameObject.SetActive(false);
        }

        Spell[] newSpells = new Spell[wandSpells.Count];
        int k = 0;
        while (wandSpells.Count > 0)
        {
            GameObject spell = wandSpells.Dequeue();
            var g = Instantiate(spell, wand.transform);
            newSpells[k] = g.GetComponent<Spell>();
            k++;
        }
        wand.SetSpells(newSpells);
        
        this.gameObject.SetActive(false);
    }

    private void Awake()
    {
        // initialize maps
        foreach (SpellIcon i in spellIconsSO.spellIcons)
        {
            _iconMap.Add(i.spellType.GetType(), i.iconPrefab);
            _prefabMap.Add(i.spellType.GetType(), i.spellType.gameObject);
        }
    }
}
