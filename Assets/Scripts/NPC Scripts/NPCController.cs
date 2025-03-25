using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NPCInputs
{
    public Vector3 MoveVector;
    public Vector3 LookVector;
    public bool Attack;
}

public class NPCController : CharacterController
{
    public void SetInputs(ref NPCInputs inputs)
    {
        _moveInputVector = inputs.MoveVector;
        _lookInputVector = inputs.LookVector;
        if(inputs.Attack)
        {
            _attackRequested = true;
        }
    }
}
