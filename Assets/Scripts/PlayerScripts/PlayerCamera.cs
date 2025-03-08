using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour
{    
    [SerializeField]
    private float _distanceMovementSpeed = 5f, _distanceMovementSharpness = 10f, 
    _rotationSpeed = 10f, _rotationSharpness = 10000f, 
    _followSharpness = 10000f, _minVerticleAngle = -70f, 
    _maxVerticleAngle = 70f, _defaultVerticleAngle = 0f;

    private Transform _followTransform;
    private Vector3 _currentFollowPosition, _planarDirection;
    private float _targetVerticalAngle;
    private float _currentDistance, _targetDistance;

    private void Awake()
    {
        _targetDistance = 0;
        _targetVerticalAngle = 0f;
        _planarDirection = Vector3.forward;
    }

    public void SetFollowTransform(Transform t)
    {
        _followTransform = t;
        _currentFollowPosition = t.position;
        _planarDirection = t.forward;
    }

    private void OnValidate()
    {
        _defaultVerticleAngle = Mathf.Clamp(_defaultVerticleAngle, _minVerticleAngle, _maxVerticleAngle);
    }

    private void HandleRotationInput(float deltaTime, Vector3 rotationInput, out Quaternion targetRotation)
    {
        Quaternion rotationFromInput = Quaternion.Euler(_followTransform.up * (rotationInput.x * _rotationSpeed));
        _planarDirection = rotationFromInput * _planarDirection;
        _planarDirection = Vector3.Cross(_followTransform.up, Vector3.Cross(_planarDirection, _followTransform.up));
        Quaternion planarRot = Quaternion.LookRotation(_planarDirection, _followTransform.up);
        
        _targetVerticalAngle -= rotationInput.y * _rotationSpeed;
        _targetVerticalAngle = Mathf.Clamp(_targetVerticalAngle, _minVerticleAngle, _maxVerticleAngle);
        Quaternion verticalRot = Quaternion.Euler(_targetVerticalAngle, 0, 0);

        targetRotation = Quaternion.Slerp(transform.rotation, planarRot * verticalRot, _rotationSharpness * deltaTime);
        transform.rotation = targetRotation;
    }

    private void HandlePosition(float deltaTime, Quaternion targetRotation)
    {
        _targetDistance +=  _distanceMovementSpeed;
        _targetDistance = 0;
        _currentFollowPosition = Vector3.Lerp(_currentFollowPosition, _followTransform.position, 1f - Mathf.Exp(-_followSharpness * deltaTime));
        Vector3 targetPosition = _currentFollowPosition - ((targetRotation * Vector3.forward) * _currentDistance);
        
        _currentDistance = Mathf.Lerp(_currentDistance, _targetDistance, 1 - Mathf.Exp(-_distanceMovementSharpness * deltaTime));
        transform.position = targetPosition;
    }

    public void UpdateWithInput(float deltaTime, Vector3 rotationInput)
    {
        if(_followTransform)
        {
            HandleRotationInput(deltaTime, rotationInput, out Quaternion targetRotation);
            HandlePosition(deltaTime, targetRotation);
        }
    }
}