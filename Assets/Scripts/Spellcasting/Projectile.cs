using System;
using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction { set; get; }
    public float Speed { set; get; }
    public float Gravity { set; get; }
    
    public IDamageable HitEntity { set; get; }
    public Vector3 HitNormal { private set; get; }
    public bool Collided { set; get; }

    public void Traverse(float dt)
    {
        // apply gravity to direction
        Direction = Vector3.Lerp(Direction, Vector3.down, Gravity * dt);
        // move
        transform.Translate(Direction * (Speed * dt));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ignore Collision"))
        {
            Collided = true;
            HitNormal = (transform.position - other.ClosestPoint(transform.position)).normalized;
            
            var hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                HitEntity = hit;
            }
        }
    }
}
