using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private Spawning _spawner;
    private Wand _wand;
    private StatBar _manaBar;
    private StatBar _healthBar;
    private StatBar _cooldownBar;
    private Image _crosshair;
    private TextMeshProUGUI _coins;
    private TextMeshProUGUI _rounds;

    

    
    void Start()
    {
        _healthBar.SetMax(_player._maxHealth);
        _manaBar.SetMax(_player._maxMana);
        _cooldownBar.SetMax(1);
    }

    // Update is called once per frame
    void Update()
    {
        _healthBar.SetFill(_player._currentHealth);
        _manaBar.SetFill(_player._currentMana);
        _cooldownBar.SetMax(_wand._maxCooldown);
        _cooldownBar.SetFill(_wand._maxCooldown-_wand._currentCooldown);
        _rounds.text = $"Rounds: {_spawner._currentRound}";
        _coins.text = $"Coins: {_player._coins}";
        CursorState();
    }

    void CursorState()
    {
        if(_wand._currentCooldown > 0)
        {
            _crosshair.color = new Color32(255,0,0,100);
        }
        else
        {
            _crosshair.color = new Color32(0,0,0,100);
        }
    }
}
