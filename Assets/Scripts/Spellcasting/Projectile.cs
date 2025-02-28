using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction { set; get; }
    public float Speed { set; private get; }
    public float Gravity { set; private get; }
    
    public bool Collided { get; private set; }

    public void Traverse(float dt)
    {
        // apply gravity to direction
        Direction = Vector3.Lerp(Direction, Vector3.down, Gravity * dt);
        // move
        this.transform.Translate(Direction * (Speed * dt));
    }
}
