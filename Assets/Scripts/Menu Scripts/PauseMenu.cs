using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject _pauseMenuUI, _settingMenuUI;
    public SpellMenu _spellMenu;
    public Inventory _inventory;
    public Wand _wand;
    public Spell[] _spells;
    public GameObject[] _enemies;
    private PlayerInput playerInput;
    private Image _musicMuteIndicator;
    private Image _SFXMuteIndicator;

    private bool _musicIsMuted = false;
    private bool _SFXIsMuted = false;

    

    void Start()
    {
        playerInput = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInput>();
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        StopTime();
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);   
        ResumeTime();
    }

    public void ToggleSpellMenu(bool _isOpen)
    {
        if (!_isOpen)
        {
            _spellMenu.Open(_inventory, _wand);
            StopTime();
            foreach (var s in _spells)
            {
                _inventory.AddSpell(s);
            }
        }
        else
        {
            _spellMenu.Close(_inventory, _wand);
            ResumeTime();
        }
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);    
    }

    public void OpenSettingsMenu()
    {
        _settingMenuUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
        _musicMuteIndicator = GameObject.FindGameObjectsWithTag("MusicMuteIndicator")[0].GetComponent<Image>();
        _musicMuteIndicator.enabled = _musicIsMuted;
        _SFXMuteIndicator = GameObject.FindGameObjectsWithTag("SFXMuteIndicator")[0].GetComponent<Image>();
        _SFXMuteIndicator.enabled = _SFXIsMuted;
    }

    public void ToggleMusic()
    {
        AudioManager._audioManager.ToggleMusic();
        _musicIsMuted = !_musicIsMuted;
        _musicMuteIndicator.enabled = _musicIsMuted; 
    }

    public void ToggleSoundEffcts()
    {
        AudioManager._audioManager.ToggleSoundEffects();
        _SFXIsMuted = !_SFXIsMuted;
        _SFXMuteIndicator.enabled = _SFXIsMuted;
    }

    public void SettingsToPause()
    {
        _settingMenuUI.SetActive(false);
        _pauseMenuUI.SetActive(true);
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        playerInput.actions.FindActionMap("UIControls").Enable();
        playerInput.actions.FindActionMap("PlayerControls").Disable();  
        _enemies = GameObject.FindGameObjectsWithTag("EnemyNPCController");
        foreach (GameObject enemy in _enemies)
        {
            enemy.GetComponent<NPCController>().enabled = false;
        }
    }
    public void ResumeTime()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerInput.actions.FindActionMap("UIControls").Disable();
        playerInput.actions.FindActionMap("PlayerControls").Enable();
        _enemies = GameObject.FindGameObjectsWithTag("EnemyNPCController");
        foreach (GameObject enemy in _enemies)
        {
            enemy.GetComponent<NPCController>().enabled = true;
        }
    }
}
