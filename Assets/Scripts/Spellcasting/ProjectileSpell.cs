using UnityEngine;
using System.Collections.Generic;

public abstract class ProjectileSpell: Spell
{
    public List<Projectile> projectileInstances = new List<Projectile>();
    
    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);

        while (modifiers.Count > 0)
        {
            modifiers.Dequeue().ModifySpell(this);
        }
    }
    
    public override void Cast(Vector3 direction, Vector3 origin)
    {
        var projectile = GameObject.Instantiate(_spellStat.prefab, this.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Direction = direction;
        projectile.Speed = _spellStat.speed;
        projectile.Gravity = _spellStat.range;
        projectileInstances.Add(projectile);
        
        PreCast?.Invoke(direction);
    }

    protected virtual void TraverseProjectiles(float dt)
    {
        Stack<int> removeIndices = new Stack<int>();
        
        for (int i = 0; i < projectileInstances.Count; i++)
        {
            var projectile = projectileInstances[i];
            
            projectile.Traverse(dt);

            if (projectile.Collided)
            {
                Destroy(projectile.gameObject);
                removeIndices.Push(i);
            }
        }

        while (removeIndices.Count > 0)
        {
            int i = removeIndices.Pop();
            var projectile = projectileInstances[i];
            OnHit(projectile.transform.position, projectile.Direction, projectile.HitEntity);
            Destroy(projectile.gameObject);
            projectileInstances.RemoveAt(i);
        }
    }

    public override void Reset()
    {
        PreCast = null;
        MidCast = null;
        PostCast = null;
        PreCast = (Vector3 direction) => { Queried = false; };
        Queried = false;
        modifiers.Clear();
    }

    protected abstract void OnHit(Vector3 position, Vector3 direction, IDamageable entity);

    protected virtual void Update()
    {
        this.TraverseProjectiles(Time.deltaTime);
    }
}
