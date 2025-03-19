using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Stats")]
public class NPCSO : ScriptableObject
{
    public int _health;
    public int _attackPower, _attackSpeed, _attackRange;
    public GameObject[] _drops;
    public bool _isBoss;
}
