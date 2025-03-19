using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string _itemName;
    public Sound _pickUpSound;
    public int _valueRangeMin, _valueRangeMax;
    public float _dropPercentage;
}
