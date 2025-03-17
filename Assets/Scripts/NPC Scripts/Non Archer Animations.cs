using UnityEngine;
using KinematicCharacterController;
using UnityEngine.Timeline;

public class NonArchersAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator mAnimator;
    [SerializeField]
    private KinematicCharacterMotor _motor;
    
    private System.Random _randomNumber = new System.Random();
    private bool _newlySpawned = true;
    
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if(mAnimator != null){
            if(_newlySpawned)
            {
                mAnimator.SetTrigger("Spawn");
                _newlySpawned = false;
            }
            else if(false)//Add Attack Conditions to trigger
            {
                mAnimator.SetTrigger("Attack");
                int AttackChoice = _randomNumber.Next(3);
                if(AttackChoice == 0)
                {
                    mAnimator.SetTrigger("Attack_A");
                }
                else if(AttackChoice == 1)
                {
                    mAnimator.SetTrigger("Attack_B");
                }
                else if(AttackChoice == 2)
                {
                    mAnimator.SetTrigger("Attack_C");
                }
            }
            else if(_motor.GroundingStatus.IsStableOnGround && true)//Add Motion detected to trigger
            {
                mAnimator.SetTrigger("Walking");
            }
            else if(_motor.GroundingStatus.IsStableOnGround && true)//Add No Motion detected to trigger
            {
                mAnimator.SetTrigger("Idle");
            }
            else if(!_motor.GroundingStatus.IsStableOnGround)
            {
                mAnimator.SetTrigger("Falling");
            }
            else if(false)//Add Death Condition to trigger
            {
                mAnimator.SetTrigger("Death");
            }
        }
    }
}
