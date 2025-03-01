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
                foreach (var m in modifiers)
                {
                    // ensure doublespell is only removed from modifiers once
                    if (found == 0)
                    {
                        m.modifiedSpells.RemoveAt(m.modifiedSpells.Count - 1);
                    }
                    
                    m.modifiedSpells.Add(otherSpells[i]);
                    otherSpells[i].modifiers.Enqueue(m);
                }
                
                Spell[] newOtherSpells = new Spell[otherSpells.Length - 1 - i];
                Array.Copy(otherSpells, i + 1, newOtherSpells, 0, otherSpells.Length - 1 - i);
                otherSpells[i].Query(newOtherSpells, result);
                found += 1;

                if (found >= 2)
                {
                    while (modifiers.Count > 0)
                    {
                        modifiers.Dequeue();
                    }
                    return;
                }
            }
        }
    }

    public override void Cast(Vector3 direction)
    {
        PreCast?.Invoke(direction);
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
