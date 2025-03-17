using UnityEngine;

public class TestWand : MonoBehaviour
{
    public Wand wand;
    public Spell[] spells;

    public int maxMana;
    public int regen;
    
    private int _currentMana;
    private float _regenTimer;
    
    private void OnAttack()
    {
        wand.Use(Vector3.right, Vector3.zero, ref _currentMana);
    }

    private void OnInteract()
    {
        wand.SetSpells(spells);
    }

    private void Start()
    {
        _currentMana = maxMana;
    }

    private void Update()
    {
        _regenTimer += Time.deltaTime;
        
        if (_regenTimer >= 0.5f)
        {
            _regenTimer = 0;
            _currentMana = Mathf.Clamp(_currentMana + regen, 0, maxMana);
        }
    }
}
