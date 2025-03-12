using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DragSlot: MonoBehaviour
{
    public bool Occupied { get; private set; } = false;

    [SerializeField] private DragObject _contained;

    public void Assign(DragObject drag)
    {
        if (_contained != null && _contained != drag)
        {
            Debug.LogError($"DragSlot is already occupied");
        }
        
        drag.slot = this;
        _contained = drag;
        Occupied = true;
    }

    public void UnAssign(DragObject drag)
    {
        drag.slot = null;
        _contained = null;
        Occupied = false;
    }
}
