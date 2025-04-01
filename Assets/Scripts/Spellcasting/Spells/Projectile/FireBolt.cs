using System.Collections.Generic;
using UnityEngine;

public class FireBolt: ProjectileSpell
{
    [SerializeField] private GameObject _explosionPrefab;

    public void ExplosionHit(Vector3 origin, GameObject target)
    {
        var hit = target.GetComponent<IDamageable>();
        if (hit != null && !target.CompareTag("Player"))
        {
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.amount = _spellStat.damage;
            hit.Damage(dmgInfo);
        }
    }
    
    protected override void OnHit(Projectile projectile)
    {
        Field explosion = Instantiate(_explosionPrefab, projectile.transform.position, Quaternion.identity).GetComponent<Field>();
        explosion.EffectCallback += ExplosionHit;
        explosion.Init();
    }

    protected override void HandleHitIndices(Stack<int> hitIndices)
    {
        DestroyHitIndices(hitIndices);
    }
}
