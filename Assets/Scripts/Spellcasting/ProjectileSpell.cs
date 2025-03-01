using UnityEngine;
using System.Collections.Generic;

public abstract class ProjectileSpell: Spell
{
    protected List<Projectile> _projectileInstances = new List<Projectile>();
    
    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);

        while (modifiers.Count > 0)
        {
            modifiers.Dequeue().ModifySpell(this);
        }
    }
    
    public override void Cast(Vector3 direction)
    {
        PreCast?.Invoke(direction);
        
        var projectile = GameObject.Instantiate(_spellStat.prefab, this.transform.position, Quaternion.identity, this.transform).GetComponent<Projectile>();
        projectile.Direction = direction;
        projectile.Speed = _spellStat.speed;
        projectile.Gravity = _spellStat.range;
        _projectileInstances.Add(projectile);
    }

    protected virtual void TraverseProjectiles(float dt)
    {
        Stack<int> removeIndices = new Stack<int>();
        
        for (int i = 0; i < _projectileInstances.Count; i++)
        {
            var projectile = _projectileInstances[i];
            
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
            var projectile = _projectileInstances[i];
            OnHit(projectile.transform.position, projectile.Direction);
            _projectileInstances.RemoveAt(i);
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

    protected abstract void OnHit(Vector3 position, Vector3 direction);

    protected virtual void Update()
    {
        this.TraverseProjectiles(Time.deltaTime);
    }
}
