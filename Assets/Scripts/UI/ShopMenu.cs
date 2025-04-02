using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopMenu : MonoBehaviour
{
    public SpellIconsSO spellIconsSO;
    
    public DragSlot[] inventorySlots = {};
    public DragSlot[] shopSlots = {};
    public DragSlot destroySlot;
    
    public RectTransform defaultLayer;
    
    private Dictionary<Type, GameObject> _iconMap = new Dictionary<Type, GameObject>();
    private Dictionary<Type, GameObject> _prefabMap = new Dictionary<Type, GameObject>();
    private Dictionary<Type, int> _costMap = new Dictionary<Type, int>();
    
    private List<Type> _shopContents = new List<Type>();

    public void Open(Inventory inventory)
    {
        Debug.Log("Opening shop menu");
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

        for (int i = 0; i < shopSlots.Length; i++)
        {
            var icon = _iconMap.ElementAt(Random.Range(0, _iconMap.Count)); // get random value
            
            var g = Instantiate(icon.Value, shopSlots[i].transform.position, Quaternion.identity);
            g.transform.SetParent(defaultLayer);
        
            DragObject drag = g.GetComponent<DragObject>();
            drag.type = icon.Key;
            drag.slot = shopSlots[i].GetComponent<DragSlot>();
            drag.slot.Assign(drag);
            drag.slot.text.text = _costMap[icon.Key].ToString();
            drag.Init();
            
            _shopContents.Add(icon.Key);
        }
    }

    public void Close(Inventory inventory, ref int gold)
    {
        inventory.Reset();
        
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
        
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (shopSlots[i].Occupied)
            {
                DragObject drag = shopSlots[i].Contained;
                shopSlots[i].UnAssign(drag);
                _shopContents.Remove(drag.type);
                Destroy(drag.gameObject);
            }
        }

        foreach (Type t in _shopContents)
        {
            gold -= _costMap[t]; // cost
        }
        
        this.gameObject.SetActive(false);
    }

    private void Awake()
    {
        // initialize maps
        foreach (SpellIcon i in spellIconsSO.spellIcons)
        {
            _iconMap.Add(i.spellType.GetType(), i.iconPrefab);
            _prefabMap.Add(i.spellType.GetType(), i.spellType.gameObject);
            _costMap.Add(i.spellType.GetType(), Random.Range(50, 101));
        }

        destroySlot.OnAssign += () =>
        {
            Destroy(destroySlot.Contained.gameObject);
            destroySlot.UnAssign(destroySlot.Contained);
        };
    }
}
