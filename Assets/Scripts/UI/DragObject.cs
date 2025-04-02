using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DragSlot slot = null;
    public Type type;
    public Tooltip tooltip = null;
    
    private DragManager _manager = null;
    private Image _image = null;

    private Vector2 _centerPoint;
    private Vector2 _worldCenterPoint => transform.TransformPoint(_centerPoint);

    public void Init()
    {
        _manager = GetComponentInParent<DragManager>();
        _image = GetComponent<Image>();
        _centerPoint = (transform as RectTransform).rect.center;

        Transform tooltipContainer = transform.parent.parent.GetChild(transform.parent.parent.childCount - 1);
        tooltip = Instantiate(tooltip, tooltipContainer);
        tooltip.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _manager.RegisterDraggedObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_manager.IsWithinBounds(_worldCenterPoint + eventData.delta))
        {
            transform.Translate(eventData.delta);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _manager.UnregisterDraggedObject(this, eventData.hovered);
        _image.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!tooltip.gameObject.activeInHierarchy && _manager.CurrentDraggedObject == null)
        {
            tooltip.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip.gameObject.activeInHierarchy)
        {
            tooltip.gameObject.SetActive(false);
        }
    }

    public void OnDisable()
    {
        tooltip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.gameObject.activeInHierarchy)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }
}
