using UnityEngine;
using KinematicCharacterController;

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
            else if(_enemy.Damaged)
            {
                mAnimator.SetTrigger("Damage");
            }
            else if(_enemy.Attacking)
            {
                mAnimator.SetTrigger("Attack");
            }
            else
            {
                mAnimator.SetTrigger("Walking");
            }
        }
    }
}
