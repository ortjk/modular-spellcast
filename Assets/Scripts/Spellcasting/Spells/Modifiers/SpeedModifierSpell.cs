using System.Linq;
using UnityEngine;

public class SpeedModifierSpell: ModifierSpell
{
    public override void ModifySpell(Spell spell)
    {
        spell.PreCast += ModifySpeed;
    }

    private void ModifySpeed(Vector3 direction)
    {
        Spell s = modifiedSpells[0];
        ProjectileSpell p = s.GetComponent<ProjectileSpell>();
        
        if (p != null)
        {
            p.projectileInstances[^1].Speed += _spellStat.speed;
        }
        
        s.PreCast -= ModifySpeed;
        modifiedSpells.RemoveAt(0);
    }
}
