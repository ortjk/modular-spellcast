using UnityEngine;

public struct PlayerInputs
{
    public float MoveAxisForward;
    public float MoveAxisRight;
    public float MoveAxisUp;
    public bool JumpPressed;
    public bool SpellCastPressed;
    public Quaternion CameraRotation;
}

public class PlayerController : CharacterController
{
    [SerializeField]
    protected PlayerCamera _playerCamera;

    public void SetInputs(ref PlayerInputs inputs)
    {
        Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward), 1f);
        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, _motor.CharacterUp).normalized;

        if(cameraPlanarDirection.sqrMagnitude == 0f)
        {
            cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, _motor.CharacterUp).normalized;
        }
    
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _motor.CharacterUp);

        _moveInputVector = cameraPlanarRotation * moveInputVector;
        
        _lookInputVector = cameraPlanarDirection;
        
        if(inputs.JumpPressed)
        {
            _jumpRequested = true;
        }
        if(inputs.SpellCastPressed)
        {
            _spellCastRequested = true;
        }
    }
}
