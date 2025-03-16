using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string _itemName;
    public Sound _pickUpSound;
    public int _valueRangeMin, _valueRangeMax;
    public bool _isGold;
    public bool _isSpell;
    public float _dropPercentage;
}
