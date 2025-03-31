using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
    public ItemSO _itemSO;
    public int _value;
    public int _spellindex;
    public SpellList _spells;
    private System.Random _randomInt = new System.Random();
   

    void Start()
    {
        _value = 0;
        if(CompareTag("Gold"))
        {
            _value = _randomInt.Next(_itemSO._valueRangeMin, _itemSO._valueRangeMax);
        }
        if(CompareTag("Spell"))
        {
            _spellindex = _randomInt.Next(0, _spells.spellList.Count-1);;
        }
    }

    private void OnValidate()
    {
        if(_itemSO == null) {return;}
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {          
            AudioManager._audioManager.PlaySoundEffect(_itemSO._pickUpSound._name);
            if(gameObject.CompareTag("Gold"))
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
