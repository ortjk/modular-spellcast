using UnityEngine;

public struct DamageInfo
{
    public float amount;
}

public interface IDamageable
{
    public void Damage(DamageInfo info);
}
