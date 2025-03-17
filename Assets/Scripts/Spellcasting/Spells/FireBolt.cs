using UnityEngine;

public class FireBolt: ProjectileSpell
{
    [SerializeField] private GameObject _explosionPrefab;

    public void ExplosionHit(GameObject target)
    {
        var hit = target.GetComponent<IDamageable>();
        if (hit != null)
        {
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.amount = _spellStat.damage;
            hit.Damage(dmgInfo);
        }
    }
    
    protected override void OnHit(Vector3 position, Vector3 direction, IDamageable entity)
    {
        Field explosion = Instantiate(_explosionPrefab, position, Quaternion.identity).GetComponent<Field>();
        explosion.EffectCallback += ExplosionHit;
        explosion.Init();
    }
}
