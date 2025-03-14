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
    public float _maxMana = 100f, _maxHealth = 100f, _manaPerSecond = 1f;

    private PlayerInputs _inputs = new PlayerInputs();
    private Vector3 _lookInputVector;

    public float _coins;
    public float _currentMana;
    public float _currentHealth;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerCamera.SetFollowTransform(_cameraFollowPoint);
        _currentHealth = _maxHealth;
        _currentMana = _maxMana;
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

    private void ManaRegeneration()
    {
        if(_currentMana < _maxMana)
        {
            _currentMana = _currentMana + (_manaPerSecond * Time.deltaTime);
        }
    }

    void Update()
    {
        ManaRegeneration();
    }

    private void LateUpdate()
    {
        _playerCamera.UpdateWithInput(Time.deltaTime, _lookInputVector);
    }
}
