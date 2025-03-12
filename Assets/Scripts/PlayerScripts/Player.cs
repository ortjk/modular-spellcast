using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerCamera _playerCamera;
    [SerializeField]
    private Transform _cameraFollowPoint;
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private GameObject _pauseMenuUI;

    private PlayerInput playerInput;
    private PlayerInputs _inputs = new PlayerInputs();
    private Vector3 _lookInputVector;
    public GameObject[] _enemies;

    private void Start()
    {
        playerInput = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInput>();
        _inputs.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        _playerCamera.SetFollowTransform(_cameraFollowPoint);
    }

    private void OnMove(InputValue value)
    {
        var rawMove = value.Get<Vector2>();
        _inputs.MoveAxisForward = rawMove.y;
        _inputs.MoveAxisRight = rawMove.x;
        
        _playerController.SetInputs(ref _inputs);
    }

    private void OnLook(InputValue value)
    {
        var rawLook = value.Get<Vector2>();
        _lookInputVector = new Vector3(rawLook.x, rawLook.y, 0);

        _inputs.CameraRotation = _playerCamera.transform.rotation;
        _playerController.SetInputs(ref _inputs);
    }

    private void OnJump(InputValue value)
    {
        _inputs.JumpPressed = value.isPressed;
        _playerController.SetInputs(ref _inputs);
    }

    private void OnPause(InputValue value)
    {
        if(value.isPressed && !_inputs.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            _pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            _enemies = GameObject.FindGameObjectsWithTag("EnemyNPC");
            foreach (GameObject enemy in _enemies)
            {
                enemy.GetComponent<NPCController>().enabled = false;
            }
            playerInput.actions.FindActionMap("PlayerControls").Disable();
            playerInput.actions.FindActionMap("UIControls").Enable();    
            _inputs.GameIsPaused = true;
        }
    } 

    private void OnResume(InputValue value)
    {
        if(value.isPressed && _inputs.GameIsPaused)
        {
             Cursor.lockState = CursorLockMode.Locked;
            _pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            _enemies = GameObject.FindGameObjectsWithTag("EnemyNPC");
            foreach (GameObject enemy in _enemies)
            {
                enemy.GetComponent<NPCController>().enabled = true;
            }
            playerInput.actions.FindActionMap("PlayerControls").Enable();
            playerInput.actions.FindActionMap("UIControls").Disable();
            _inputs.GameIsPaused = false;
        }
    }

    private void LateUpdate()
    {
        _playerCamera.UpdateWithInput(Time.deltaTime, _lookInputVector);
    }
}
