using UnityEngine;

public class ChainLightning: HitscanSpell
{
    [SerializeField] private GameObject _fieldPrefab;

    private void FieldHit(Vector3 origin, GameObject target)
    {
        var hit = target.GetComponent<IDamageable>();
        if (hit != null && !target.CompareTag("Player"))
        {
            CreateBolt(origin, target.transform.position);
            
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.amount = _spellStat.damage;
            hit.Damage(dmgInfo);
        }
    }
    
    protected override void OnHit(Vector3 origin, Vector3 target)
    {
        CreateBolt(origin, target);
        
        Field field = Instantiate(_fieldPrefab, target, Quaternion.identity).GetComponent<Field>();
        field.EffectCallback += FieldHit;
        field.Init();
    }
}