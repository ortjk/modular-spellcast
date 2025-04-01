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
        if(_spellStat.spellSound != null)
        {
            AudioManager._audioManager.PlaySpellSound(_spellStat.spellSound._name);
        }
    }

    protected void TraverseProjectiles(float dt)
    {
        Stack<int> hitIndices = new Stack<int>();
        
        for (int i = 0; i < projectileInstances.Count; i++)
        {
            var projectile = projectileInstances[i];
            
            projectile.Traverse(dt);

            if (projectile.Collided)
            {
                hitIndices.Push(i);
            }
        }
        
        HandleHitIndices(hitIndices);
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

    protected void DestroyHitIndices(Stack<int> hitIndices)
    {
        while (hitIndices.Count > 0)
        {
            int i = hitIndices.Pop();
            var projectile = projectileInstances[i];
            OnHit(projectile);
            Destroy(projectile.gameObject);
            projectileInstances.RemoveAt(i);
        }
    }

    protected abstract void OnHit(Projectile projectile);

    protected abstract void HandleHitIndices(Stack<int> hitIndices);

    protected virtual void Update()
    {
        TraverseProjectiles(Time.deltaTime);
    }
}
