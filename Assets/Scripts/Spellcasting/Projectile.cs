using UnityEngine;

public abstract class Projectile: Spell
{
    [Header("Projectile")]
    [SerializeField] protected GameObject _projectilePrefab;
    
    protected float _speed;
    protected float _gravity;
    protected Vector3 _direction;

    protected virtual void Traverse(float dt)
    {
        
    }

    protected abstract void OnHit();
}
