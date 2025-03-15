using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuUI, _spellMenuUI, _settingMenuUI;

    public GameObject[] _enemies;
    private PlayerInput playerInput;
    

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
        _enemies = GameObject.FindGameObjectsWithTag("EnemyNPC");
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
        _enemies = GameObject.FindGameObjectsWithTag("EnemyNPC");
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

    public void Settings()
    {
        _settingMenuUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
    }


}
