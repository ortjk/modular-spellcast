using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BouncingBolt: ProjectileSpell
{
    private List<int> _hits = new List<int>();
    
    private const int MAX = 3;

    public override void Cast(Vector3 direction, Vector3 origin)
    {
        base.Cast(direction, origin);
        _hits.Add(0);
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

        projectile.Direction = Vector3.Reflect(projectile.Direction, projectile.HitNormal);
        projectile.Collided = false;
    }

    private void BounceHitIndices(Stack<int> hitIndices)
    {
        while (hitIndices.Count > 0)
        {
            int i = hitIndices.Pop();
            var projectile = projectileInstances[i];
            OnHit(projectile);

            _hits[i] += 1;
            if (_hits[i] > MAX)
            {
                _hits.RemoveAt(i);
                projectileInstances.RemoveAt(i);
                Destroy(projectile.gameObject);
            }
        }
    }

    protected override void HandleHitIndices(Stack<int> hitIndices)
    {
        BounceHitIndices(hitIndices);
    }
}
