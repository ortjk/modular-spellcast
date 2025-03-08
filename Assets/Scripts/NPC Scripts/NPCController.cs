using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public List<CharacterController> NPCs = new List<CharacterController>();

    private void Update()
    {
        NPCInputs inputs = new NPCInputs();
        inputs.MoveVector = Vector3.zero;
        inputs.LookVector = Vector3.zero;
        inputs.Attack = false;
        for(int i = 0; i < NPCs.Count; i++)
        {
        NPCs[i].SetInputs(ref inputs);
        }
    }
}
