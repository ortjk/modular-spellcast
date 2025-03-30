using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject _pauseMenuUI, _settingMenuUI, _wandPopUpUI, _deathScreenUI;
    public SpellMenu _spellMenu;
    public Inventory _inventory;
    public Wand _wand;
    public Spell[] _spells;
    public GameObject[] _enemies;
    private PlayerInput playerInput;
    private Player player;
    private GameObject _newWand;
    private Image _musicMuteIndicator;
    private Image _SFXMuteIndicator;
    private WandPopUp _wandPopUp;
    private bool _musicIsMuted = false;
    private bool _SFXIsMuted = false;

    

    void Start()
    {
        playerInput = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInput>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        _wandPopUp = _wandPopUpUI.GetComponent<WandPopUp>();
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

    public void StartGame()
    {
        AudioManager._audioManager.PlayMusic("BackgroundMusic");
        SceneManager.LoadSceneAsync("Map", LoadSceneMode.Single);
        ResumeTime();
    }

    public void Quit()
    {
        AudioManager._audioManager.PlayMusic("MainMenuMusic");
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);    
        StopTime();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        _deathScreenUI.SetActive(true);
        StopTime();
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

    public void WandPopUp(float currenWandReload, float newWandReload, int currentWandSlots, int newWandSlots, GameObject newWand)
    {
        _wandPopUpUI.SetActive(true);
        _wandPopUp.currenWandReload.text = $"Reload: {currenWandReload}";
        _wandPopUp.currentWandSlots.text = $"Slots: {currentWandSlots}";
        _wandPopUp.newWandReload.text = $"Reload: {newWandReload}";
        _wandPopUp.newWandSlots.text = $"Slots: {newWandSlots}";
        _newWand = newWand;
        StopTime();
    }

    public void TakeWand()
    {
        _wandPopUpUI.SetActive(false);
        ResumeTime();
        player.EquipWand(_newWand);
        
    }

    public void LeaveWand()
    {
        _wandPopUpUI.SetActive(false);
        Destroy(_newWand);
        ResumeTime();
    }
}
