using System;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [Header("Stat Acquisition")]
    public SpellStatsSO statsSO;
    public uint index;
    
    [Header("Text")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI areaText;

    private void Awake()
    {
        SpellStat stats = statsSO.spellStats[index];
        
        titleText.text = stats.spellName + " Spell";
        if (stats.damage > 0) { damageText.text = "Damage: " + stats.damage; }
        cooldownText.text = "Cooldown: " + stats.cooldown + "s";
        manaText.text = "Mana: " + stats.mana;
        if (stats.speed > 0) { speedText.text = "Speed: " + stats.speed; }
        if (stats.range > 0)
        {
            if (stats.range < 1) { rangeText.text = "Gravity: " + (1 / stats.range); }
            else {rangeText.text = "Range: " + stats.range; }
        }
        if (stats.area > 0) { areaText.text = "Area: " + stats.area; }
        
    }
}
