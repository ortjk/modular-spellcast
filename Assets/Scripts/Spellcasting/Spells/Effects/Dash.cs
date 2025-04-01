using UnityEngine;

public class Dash : Spell
{
    private PlayerController _player;
    public float _speedMultiplier;
    public float _dashTime;
    private float _dashCooldown;
    private bool _cast = false;

    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
    }
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
        _cast = true;
        _player.Dash(_speedMultiplier);
        _dashCooldown = _dashTime;
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

    void Update()
    {
        if(_dashCooldown > 0f)
        {
            _dashCooldown -= Time.deltaTime;
        }
        if(_dashCooldown <= 0f && _cast)
        {
            _player.ResetSpeed();
            _cast = false;
        }
    }
}