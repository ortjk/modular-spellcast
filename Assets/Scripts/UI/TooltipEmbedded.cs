using System;
using TMPro;
using UnityEngine;

public class TooltipEmbedded : MonoBehaviour
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

    private SpellStat _stats;

    public void SetText(uint index)
    {
        _stats = statsSO.spellStats[index];
        Debug.Log("settext");
        
        titleText.text = _stats.spellName + " Spell";
        if (_stats.damage > 0) { damageText.text = "Damage: " + _stats.damage; }
        else {damageText.text = "Damage: N/A";}
        cooldownText.text = "Cooldown: " + _stats.cooldown + "s";
        manaText.text = "Mana: " + _stats.mana;
        if (_stats.speed > 0) { speedText.text = "Speed: " + _stats.speed; }
        else {speedText.text = "Speed: N/A";}
        if (_stats.range > 0)
        {
            if (_stats.range < 1) { rangeText.text = "Gravity: " + (1 / _stats.range); }
            else {rangeText.text = "Range: " + _stats.range; }
        }
        else {rangeText.text = "Range: N/A";}
        if (_stats.area > 0) { areaText.text = "Area: " + _stats.area; }
        else {areaText.text = "Area: N/A";}
    }

    public void ResetText()
    {
        _stats = statsSO.spellStats[index];
        
        titleText.text = "Hover For Tooltip";
        damageText.text = "Damage: ";
        cooldownText.text = "Cooldown: ";
        manaText.text = "Mana: ";
        speedText.text = "Speed: ";
        rangeText.text = "Range: ";
        areaText.text = "Area: ";
    }
}
