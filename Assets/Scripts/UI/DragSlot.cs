using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DragSlot: MonoBehaviour
{
    public bool Occupied { get; private set; } = false;

    [SerializeField] public DragObject Contained { get; private set; }

    public TextMeshProUGUI text;
    
    public delegate void AssignCallback();
    public AssignCallback OnAssign;

    public void Assign(DragObject drag)
    {
        if (Contained != null && Contained != drag)
        {
            Debug.LogError($"DragSlot is already occupied");
        }
        
        drag.slot = this;
        Contained = drag;
        Occupied = true;
        
        OnAssign?.Invoke();
    }

    public void UnAssign(DragObject drag)
    {
        drag.slot = null;
        Contained = null;
        Occupied = false;
    }
}
