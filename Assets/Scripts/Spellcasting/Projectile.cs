using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction { set; get; }
    public float Speed { set; get; }
    public float Gravity { set; get; }
    
    public IDamageable HitEntity { private set; get; }
    public bool Collided { get; private set; }

    public void Traverse(float dt)
    {
        // apply gravity to direction
        Direction = Vector3.Lerp(Direction, Vector3.down, Gravity * dt);
        // move
        this.transform.Translate(Direction * (Speed * dt));
    }
}
