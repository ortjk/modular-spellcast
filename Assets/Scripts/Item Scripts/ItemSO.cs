using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    [Header("General")]
    public string _itemName;
    public Sound _pickUpSound;
    public float _dropPercentage;
    [Header("Gold")]
    public int _valueRangeMin, _valueRangeMax;
    [Header("Wand")]
    public float _reloadTime;
    public int _wandSlots;
    public GameObject _wandGameObject;
}
