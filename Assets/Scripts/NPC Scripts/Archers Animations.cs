using UnityEngine;
using KinematicCharacterController;
using UnityEditor.Experimental.GraphView;

public class ArchersAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator mAnimator;
    [SerializeField]
    private KinematicCharacterMotor _motor;
    [SerializeField]
    private Enemy _enemy;
    private bool _newlySpawned = true;
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        _enemy = GetComponentInParent<Enemy>();
    }

    void Update()
    {
        if(mAnimator != null){
            if(_newlySpawned)
            {
                mAnimator.SetTrigger("Spawn");
                _newlySpawned = false;
            }
            else if(!_enemy.Alive)//Add Death Condition to trigger
            {
                mAnimator.SetTrigger("Death");
            }
            else if(false)//Add Attack Conditions to trigger
            {
                mAnimator.SetTrigger("Attack");
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
        }
    }
}
