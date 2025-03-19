using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class Loot : MonoBehaviour
{
    public ItemSO _itemSO;
    public int _value;
    [SerializeField]
    public Wand _wand;

    private GameObject[] _spells;
    private GameObject _spell;
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
            //_spell = _spells[_randomInt.Next(0, _spells.Length-1)];
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
            Destroy(gameObject);
        }
    }
}
