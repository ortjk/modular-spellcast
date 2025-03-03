using UnityEngine;

public class NPCInputHandler : MonoBehaviour
{
    [SerializeField]
    private EnemyCharacterController _characterController;

    private Vector3 _lookInputVector;

    private void HandleCharacterInputs()
    {
        NPCInputs inputs = new NPCInputs();
        inputs.MoveVector = Vector3.zero;
        inputs.LookVector = Vector3.zero;
        inputs.Attack = false;

        _characterController.SetInputs(ref inputs);
    }

    private void Update()
    {
        HandleCharacterInputs();
    }
}
