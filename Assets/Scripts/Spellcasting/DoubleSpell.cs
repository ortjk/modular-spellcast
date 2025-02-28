using UnityEngine;
using System;

public class DoubleSpell: Spell
{
    public override void Query(Spell[] otherSpells, QueryResult result)
    {
        PreQuery(otherSpells, result);

        int found = 0;
        for (int i = 0; i < otherSpells.Length; i++)
        {
            if (!otherSpells[i].Queried)
            {
                Spell[] newOtherSpells = new Spell[otherSpells.Length - 1 - i];
                Array.Copy(otherSpells, i + 1, newOtherSpells, 0, otherSpells.Length - 1 - i);
                otherSpells[i].Query(newOtherSpells, result);
                found += 1;

                if (found >= 2)
                {
                    return;
                }
            }
        }
    }

    public override void Cast(Vector3 direction)
    {
        PreCast();
    }
}
