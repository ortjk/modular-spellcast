using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NPCSO _npcStats;

    private int _currentHealth, _attackCooldown, _distanceFromPlayer;
    private GameObject[] _enemyDrops;
    private NPCInputs _inputs = new NPCInputs();
    private System.Random _randomInteger = new System.Random();

    void Start()
    {
        _currentHealth = _npcStats._health;
        _attackCooldown = 0;
        _enemyDrops = _npcStats._drops;
    }

    private void Attack()
    {
    }

    private void Death()
    {
        foreach (GameObject drop in _enemyDrops)
        {
            if(drop.GetComponent<Loot>()._itemSO._dropPercentage >= _randomInteger.Next(1,100))
            {
                Instantiate(drop, gameObject.transform);
            }
        } 
        AudioManager._audioManager.PlaySoundEffect("EnemyDeath");
        Destroy(gameObject, 2);
    }

    void Update()
    {
        if(_currentHealth <= 0)
        {
            Death();
        }
    }
}
