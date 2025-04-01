using UnityEngine;
using System;

public class TripleSpell: MultiSpell
{
    protected override uint _max
    {
        get { return 3; }
        set { }
    }
}
