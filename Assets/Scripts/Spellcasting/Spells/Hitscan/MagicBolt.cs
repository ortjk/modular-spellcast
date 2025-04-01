using UnityEngine;

public class MagicBolt: HitscanSpell
{
    protected override void OnHit(Vector3 origin, Vector3 target)
    {
        CreateBolt(origin, target);
    }
}
