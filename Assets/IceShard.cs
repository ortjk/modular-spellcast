using UnityEngine;

public class IceShard : ProjectileSpell
{
    [SerializeField] private GameObject _spellImpact;

    public void SpellLand(GameObject target)
    {
        var hit = target.GetComponent<IDamageable>();
        if (hit != null && !target.CompareTag("Player"))
        {
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.amount = _spellStat.damage;
            hit.Damage(dmgInfo);
        }
    }
    
    protected override void OnHit(Vector3 position, Vector3 direction, IDamageable entity)
    {
        Field impact = Instantiate(_spellImpact, position, Quaternion.identity).GetComponent<Field>();
        impact.EffectCallback += SpellLand;
        impact.Init();
    }
}
