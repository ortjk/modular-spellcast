using UnityEngine;
using System.Collections.Generic;

public class PiercingBolt: ProjectileSpell
{
    private List<HashSet<IDamageable>> _hits = new List<HashSet<IDamageable>>();
    
    public override void Cast(Vector3 direction, Vector3 origin)
    {
        base.Cast(direction, origin);
        _hits.Add(new HashSet<IDamageable>());
    }
    
    protected override void OnHit(Projectile projectile)
    {
        if (projectile.HitEntity != null)
        {
            DamageInfo info = new DamageInfo();
            info.amount = _spellStat.damage;
            projectile.HitEntity.Damage(info);

            projectile.HitEntity = null;
        }

        projectile.Collided = false;
    }
    
    private void PierceHitIndices(Stack<int> hitIndices)
    {
        while (hitIndices.Count > 0)
        {
            int i = hitIndices.Pop();
            var projectile = projectileInstances[i];
            
            if (projectile.HitEntity == null)
            {
                Destroy(projectile.gameObject);
                projectileInstances.RemoveAt(i);
                _hits.RemoveAt(i);
            }
            else if (!_hits[i].Contains(projectile.HitEntity))
            {
                _hits[i].Add(projectile.HitEntity);
                OnHit(projectile);
            }
        }
    }

    protected override void HandleHitIndices(Stack<int> hitIndices)
    {
        PierceHitIndices(hitIndices);
    }
}
