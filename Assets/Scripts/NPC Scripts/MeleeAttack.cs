using UnityEngine;

public class MeleeAttack: MonoBehaviour
{
    public float damage;
    public float timer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.amount = damage;
            p.Damage(damageInfo);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
