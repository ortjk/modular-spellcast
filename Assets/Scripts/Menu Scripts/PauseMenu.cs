using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuUI, _spellMenuUI, _settingMenuUI;

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
        Time.timeScale = 0f;
        _pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        playerInput.actions.FindActionMap("UIControls").Enable();
        playerInput.actions.FindActionMap("PlayerControls").Disable();  
        _enemies = GameObject.FindGameObjectsWithTag("EnemyNPCController");
        foreach (GameObject enemy in _enemies)
        {
            enemy.GetComponent<NPCController>().enabled = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerInput.actions.FindActionMap("UIControls").Disable();
        playerInput.actions.FindActionMap("PlayerControls").Enable();
        _enemies = GameObject.FindGameObjectsWithTag("EnemyNPCController");
        foreach (GameObject enemy in _enemies)
        {
            enemy.GetComponent<NPCController>().enabled = true;
        }    
    }

    public void OpenSpellMenu()
    {
        _spellMenuUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
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
        _musicMuteIndicator.enabled = false;
        _SFXMuteIndicator = GameObject.FindGameObjectsWithTag("SFXMuteIndicator")[0].GetComponent<Image>();
        _SFXMuteIndicator.enabled = false;
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


}
