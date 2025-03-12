using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform
        _defaultLayer = null,
        _dragLayer = null;

    private Rect _boundingBox;

    private DragObject _currentDraggedObject = null;
    public DragObject CurrentDraggedObject => _currentDraggedObject;

    private DragSlot _startSlot = null;

    private void Awake()
    {
        SetBoundingBoxRect(_dragLayer);
    }

    public void RegisterDraggedObject(DragObject drag)
    {
        _startSlot = drag.slot;
        _startSlot.UnAssign(drag);
        
        _currentDraggedObject = drag;
        drag.transform.SetParent(_dragLayer);
    }

    public void UnregisterDraggedObject(DragObject drag, List<GameObject> hovered)
    {
        drag.transform.SetParent(_defaultLayer);
        _currentDraggedObject = null;

        bool found = false;
        foreach (GameObject h in hovered)
        {
            Debug.Log(h.name);
            DragSlot slot = h.GetComponent<DragSlot>();
            if (slot != null)
            {
                if (!slot.Occupied)
                {
                    slot.Assign(drag);
                    found = true;
                }
            }
        }

        if (!found)
        {
            _startSlot.Assign(drag);
        }
        
        drag.transform.position = drag.slot.transform.position;
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _boundingBox.Contains(position);
    }

    private void SetBoundingBoxRect(RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        _boundingBox = new Rect(position, size);
    }
}
