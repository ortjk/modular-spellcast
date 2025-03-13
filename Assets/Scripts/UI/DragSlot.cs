using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DragSlot: MonoBehaviour
{
    public bool Occupied { get; private set; } = false;

    [SerializeField] public DragObject Contained { get; private set; }

    public void Assign(DragObject drag)
    {
        if (Contained != null && Contained != drag)
        {
            Debug.LogError($"DragSlot is already occupied");
        }
        
        drag.slot = this;
        Contained = drag;
        Occupied = true;
    }

    public void UnAssign(DragObject drag)
    {
        drag.slot = null;
        Contained = null;
        Occupied = false;
    }
}
