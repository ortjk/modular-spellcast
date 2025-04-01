using UnityEngine;

public class MinorHeal: Spell
{
    private Player _player;
    public int _healAmmount;
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
        _player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        _player._currentHealth += _healAmmount;
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
