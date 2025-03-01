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
        s.PreCast -= ModifySpeed;
        modifiedSpells.RemoveAt(0);
        
        Debug.Log("Remaining spells modified: " + modifiedSpells.Count);
    }
}
