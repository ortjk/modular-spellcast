using KinematicCharacterController;
using Unity.Mathematics;
using UnityEngine;

public class CharacterController : MonoBehaviour, ICharacterController
{
    [SerializeField]
    protected KinematicCharacterMotor _motor;

    [SerializeField]
    protected Vector3 _gravity = new Vector3(0f, -30f, 0f);

    [SerializeField]
    protected float _maxStableMoveSpeed = 10f, _stableMovementSharpness = 15f, _normalSpeed, 
    _orientaionSharpness = 10f, _jumpSpeed = 10f;
    
    protected Vector3 _moveInputVector, _lookInputVector;
    protected bool _jumpRequested;
    protected bool _spellCastRequested;
    protected bool _attackRequested;

    protected void Start()
    {
        _motor.CharacterController = this;
        _normalSpeed = _maxStableMoveSpeed;
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void PostGroundingUpdate(float deltaTime)
    {
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        if(_lookInputVector.sqrMagnitude > 0f && _orientaionSharpness > 0f)
        {
            Vector3 smoothedLookInputDirection = Vector3.Slerp(_motor.CharacterForward, _lookInputVector, 1 - Mathf.Exp(-_orientaionSharpness * deltaTime)).normalized;

            currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, _motor.CharacterUp);
        }
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        if(_motor.GroundingStatus.IsStableOnGround)
        {
            float currentVelocityMagnitude = currentVelocity.magnitude;
            Vector3 effectiveGroundNormal = _motor.GroundingStatus.GroundNormal;

            currentVelocity = _motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;

            Vector3 inputRight = Vector3.Cross(_moveInputVector, _motor.CharacterUp);
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * _moveInputVector.magnitude;

            Vector3 targetMovementVelocity = reorientedInput * _maxStableMoveSpeed;

            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-_stableMovementSharpness * deltaTime));
            
            if(_jumpRequested)
            {
                currentVelocity += (_motor.CharacterUp * _jumpSpeed) - Vector3.Project(currentVelocity, _motor.CharacterUp);
                _motor.ForceUnground();
                _jumpRequested = false;
            }
        }
        else
        {
            currentVelocity += _gravity * deltaTime;
            _jumpRequested = false;
        }
    }
}
