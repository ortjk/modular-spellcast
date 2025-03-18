using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class Loot : MonoBehaviour
{
    public ItemSO _itemSO;
    public GameObject _item;

    private int _value;
    private GameObject[] _spells;
    private GameObject _spell;
    private System.Random _randomInt = new System.Random();
    private Player _player;

    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        _value = 0;
        if(_itemSO._isGold)
        {
            _value = _randomInt.Next(_itemSO._valueRangeMin, _itemSO._valueRangeMax);
        }
        if(_itemSO._isSpell)
        {
            //_spell = _spells[_randomInt.Next(0, _spells.Length-1)];;
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
            //Add Spell Pick up logic 
            //_player._inventory.append(_spell);
            _player._coins += _value;
            AudioManager._audioManager.PlaySoundEffect(_itemSO._pickUpSound._name);
            Destroy(_item);
        }
    }
}
