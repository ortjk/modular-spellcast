using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject _toolTip;
    public uint index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _toolTip = GameObject.FindGameObjectsWithTag("Tooltip")[0];
        Debug.Log("set");
        _toolTip.GetComponent<TooltipEmbedded>().SetText(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _toolTip = GameObject.FindGameObjectsWithTag("Tooltip")[0];
        Debug.Log("reset");
        _toolTip.GetComponent<TooltipEmbedded>().ResetText();
    }
}
