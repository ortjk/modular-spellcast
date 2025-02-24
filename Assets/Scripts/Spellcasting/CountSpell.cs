using UnityEngine;

public class CountSpell: Spell
{
    private int _count = 0;
    
    public override void Cast(Vector3 direction)
    {
        _count++;
        Debug.Log("Count is: " + _count);
    }
}
