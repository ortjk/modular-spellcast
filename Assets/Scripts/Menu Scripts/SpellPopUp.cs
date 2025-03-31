using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellPopUp : MonoBehaviour
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

    public void SetIndex(uint spellIndex)
    {
        index = spellIndex;
        _stats = statsSO.spellStats[index];
        
        titleText.text = _stats.spellName + " Spell";
        if (_stats.damage > 0) { damageText.text = "Damage: " + _stats.damage; }
        cooldownText.text = "Cooldown: " + _stats.cooldown + "s";
        manaText.text = "Mana: " + _stats.mana;
        if (_stats.speed > 0) { speedText.text = "Speed: " + _stats.speed; }
        if (_stats.range > 0)
        {
            if (_stats.range < 1) { rangeText.text = "Gravity: " + (1 / _stats.range); }
            else {rangeText.text = "Range: " + _stats.range; }
        }
        if (_stats.area > 0) { areaText.text = "Area: " + _stats.area; }
    }
}