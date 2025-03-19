using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool Alive { get; private set; } = true;
    
    [SerializeField]
    private NPCSO _npcStats;

    private float _currentHealth, _attackCooldown, _distanceFromPlayer;
    private GameObject[] _enemyDrops;
    private NPCInputs _inputs = new NPCInputs();
    private System.Random _randomInteger = new System.Random();

    public void Damage(DamageInfo info)
    {
        _currentHealth -= info.amount;
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
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
        Alive = false;
        int rand = _randomInteger.Next(1, 100);
        foreach (GameObject drop in _enemyDrops)
        {
            if (drop.GetComponent<Loot>()._itemSO._dropPercentage >= rand)
            {
                Instantiate(drop, gameObject.transform.position, Quaternion.identity);
            }
        } 
        AudioManager._audioManager.PlaySoundEffect("EnemyDeath");
        Destroy(gameObject, 1);
    }

    void Update()
    {
        
    }
}
