using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coins, _rounds;
    [SerializeField]
    private StatBar _manaBar, _healthBar, _cooldownBar;
    [SerializeField]
    private Image _crosshair;
    private Player _player;
    private Spawning _spawner;
    private Wand _wand;
    
    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        _spawner = GameObject.FindGameObjectsWithTag("Spawner")[0].GetComponent<Spawning>();
        _wand = GameObject.FindGameObjectsWithTag("PlayerWand")[0].GetComponent<Wand>();
        _healthBar.SetMax(_player._maxHealth);
        _manaBar.SetMax(_player._maxMana);
        _cooldownBar.SetMax(1);
        _rounds.text = $"Rounds: 1";
        _coins.text = $"Coins: 0";
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
            _crosshair.color = new Color32(0,0,0,255);
        }
    }
}
