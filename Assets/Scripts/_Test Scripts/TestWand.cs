using UnityEngine;

public class TestWand : MonoBehaviour
{
    public Wand wand;
    public Spell[] spells;

    private void OnAttack()
    {
        wand.Use(Vector3.right);
    }

    private void OnInteract()
    {
        wand.SetSpells(spells);
    }
}
