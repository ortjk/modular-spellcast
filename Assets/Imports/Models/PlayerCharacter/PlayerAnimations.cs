using UnityEngine;
using KinematicCharacterController;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private KinematicCharacterMotor _motor;
    private Animator mAnimator;
    private bool _inMotion;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
            else if(_inMotion && _motor.GroundingStatus.IsStableOnGround)
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
