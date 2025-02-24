using UnityEngine;

public class TestCaster : MonoBehaviour
{
    public Spell spell;

    public void OnAttack()
    {
        spell.Cast(Vector3.right);
    }
}
