using UnityEngine;
using TMPro;

public class WandTooltip : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI slotText;
    public TextMeshProUGUI cooldownText;

    private void OnEnable()
    {
        Wand wand = GameObject.FindGameObjectWithTag("PlayerWand").GetComponent<Wand>();

        titleText.text = wand.name;
        slotText.text = "Max Slots: " + wand.numSlots;
        cooldownText.text = "Reload Time: " + wand.reloadTime + " s";
    }
}
