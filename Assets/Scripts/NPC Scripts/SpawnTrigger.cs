using UnityEditor.Rendering;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] spawnpoints;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject spawnpoint in spawnpoints)
            {
                spawnpoint.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject spawnpoint in spawnpoints)
            {
                spawnpoint.SetActive(true);
            }
        }
    }
}
