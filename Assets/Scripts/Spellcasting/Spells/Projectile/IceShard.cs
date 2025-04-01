using System.Collections.Generic;
using UnityEngine;

public class IceShard : ProjectileSpell
{
    [SerializeField] private GameObject _spellImpact;

    public void SpellLand(Vector3 origin, GameObject target)
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
        Field impact = Instantiate(_spellImpact, projectile.transform.position, Quaternion.identity).GetComponent<Field>();
        impact.EffectCallback += SpellLand;
        impact.Init();
    }

    protected override void HandleHitIndices(Stack<int> hitIndices)
    {
        DestroyHitIndices(hitIndices);
    }
}
