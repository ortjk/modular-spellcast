using UnityEngine;

public class NPCSO : ScriptableObject
{
    public int _healthMin, _healthMax;
    public int _attackPower, _attackSpeed, _attackRange;
    public GameObject _enemy;
    public bool _isBoss;
}
