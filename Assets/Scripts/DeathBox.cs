using UnityEditor;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField]
    MenuController _menuController;
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _menuController.GameOver();
        }
    }
}
