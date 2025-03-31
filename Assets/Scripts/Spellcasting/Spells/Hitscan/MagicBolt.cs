using UnityEngine;

public class MagicBolt: Spell
{
    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);

        while (modifiers.Count > 0)
        {
            modifiers.Dequeue().ModifySpell(this);
        }
    }

    public override void Cast(Vector3 direction, Vector3 origin)
    {
        PreCast?.Invoke(direction);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, _spellStat.range, 0xFF))
        {
            var d = hit.collider.gameObject.GetComponent<IDamageable>();
            Debug.DrawRay(origin, direction * _spellStat.range, Color.magenta, 1);
            if (d != null)
            {
                DamageInfo dmgInfo = new DamageInfo();
                dmgInfo.amount = _spellStat.damage;
                d.Damage(dmgInfo);
            }
        }
        if(_spellStat.spellSound != null)
            {
                AudioManager._audioManager.PlaySpellSound(_spellStat.spellSound._name);
            }
    }

    public override void Reset()
    {
        PreCast = null;
        MidCast = null;
        PostCast = null;
        PreCast = (Vector3 direction) => { Queried = false; };
        Queried = false;
        modifiers.Clear();
    }
}
