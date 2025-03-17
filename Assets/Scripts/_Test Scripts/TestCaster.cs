using UnityEngine;

public class TestCaster : MonoBehaviour
{
    public Spell[] spells;

    public void OnAttack()
    {
        foreach (Spell spell in spells)
        {
            spell.Cast(Vector3.right, Vector3.zero);
        }
    }
}
