using UnityEngine;

public abstract class Spell: MonoBehaviour
{
    [Header("Spell Data")]
    [SerializeField] protected SpellStatsSO _spell_stats;
    [SerializeField] protected int _spell_id;

    public abstract void Cast(Vector3 direction);
}
