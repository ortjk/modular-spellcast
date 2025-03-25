using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable
{
    public GameObject attack;
    public Transform target;
    public bool Alive { get; private set; } = true;
    public bool Attacking { get; private set; } = false;

    [SerializeField]
    private NPCSO _npcStats;

    private float _currentHealth, _attackDamage, _attackCooldown, _attackRange, _attackTimer, _distanceFromPlayer;
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
        _attackDamage = _npcStats._attackPower;
        _attackCooldown = _npcStats._attackSpeed;
        _attackRange = _npcStats._attackRange;
        _attackTimer = 0;
        _enemyDrops = _npcStats._drops;
    }

    private void Attack()
    {
        // TODO make virtual so that different attack types are possible
        Attacking = true;
        var a = Instantiate(attack, this.transform.position, Quaternion.identity).GetComponent<MeleeAttack>();
        a.damage = _attackDamage;
        a.timer = _attackCooldown;
    }

    private void Death()
    {
        Alive = false;
        foreach (GameObject drop in _enemyDrops)
        {
            int rand = _randomInteger.Next(1, 100);
            float _dropOffset = Random.Range(-2.0f, 2.0f);
            if (drop.GetComponent<Loot>()._itemSO._dropPercentage >= rand)
            {
                Instantiate(drop, gameObject.transform.position + (Vector3.up * 0.25f) + (Vector3.forward * _dropOffset) + (Vector3.right * _dropOffset), Quaternion.identity);
            }
        } 
        AudioManager._audioManager.PlaySoundEffect(_npcStats._deathSound.GetComponent<Sound>()._name);
        Destroy(gameObject);
        GameObject deathPloom = Instantiate(_npcStats._deathPloom, transform.position, transform.rotation); 
        Destroy(deathPloom, 1f);
    }

    void Update()
    {
        _attackTimer -= Time.deltaTime;
        if (Alive && _attackTimer <= 0)
        {
            Attacking = false;
            _attackTimer = _attackCooldown;
            if (Vector3.Magnitude(target.position - this.transform.position) <= _attackRange)
            {
                this.Attack();
            }
        }
    }
}
