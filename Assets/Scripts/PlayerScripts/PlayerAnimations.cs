using UnityEngine;
using KinematicCharacterController;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private KinematicCharacterMotor _motor;
    private Animator mAnimator;
    private bool _inMotion;
    
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(mAnimator != null){
            if(Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
            {
                _inMotion = true;
            }
            else
            {
                _inMotion = false;
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                mAnimator.SetTrigger("Casting");
            }
            if(_inMotion && _motor.GroundingStatus.IsStableOnGround)
            {
                mAnimator.SetTrigger("Walking");
            }
            else if(!Input.anyKey && _motor.GroundingStatus.IsStableOnGround) 
            {
                mAnimator.SetTrigger("Idle");
            }
            else if(!_motor.GroundingStatus.IsStableOnGround)
            {
                mAnimator.SetTrigger("Falling");
            }
        }
    }
}
