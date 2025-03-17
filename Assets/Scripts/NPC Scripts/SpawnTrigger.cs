using UnityEditor.Rendering;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] spawnpoints;
    
    void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < spawnpoints.Length; i++)
        spawnpoints[i].SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < spawnpoints.Length; i++)
        spawnpoints[i].SetActive(true);
    }
}
