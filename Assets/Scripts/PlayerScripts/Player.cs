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

    private PlayerInputs _inputs = new PlayerInputs();
    private Vector3 _lookInputVector;

    private void Start()
    {
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
        _inputs.JumpPressed = false;
    }

    private void OnPause(InputValue value)
    {
        _inputs.PausePressed = value.isPressed;
        _playerController.SetInputs(ref _inputs);
        _inputs.PausePressed = false;
    } 

    private void OnCast(InputValue value)
    {
        _inputs.SpellCastPressed = value.isPressed;
        _playerController.SetInputs(ref _inputs);
        _inputs.SpellCastPressed = false;
    }

    private void LateUpdate()
    {
        _playerCamera.UpdateWithInput(Time.deltaTime, _lookInputVector);
    }
}
