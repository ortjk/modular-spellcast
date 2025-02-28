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
        int mana = wand.GetManaForCast();
        Debug.Log("Cost: " + mana + ", current: " + _currentMana);
        if (mana <= _currentMana)
        {
            _currentMana -= mana;
            wand.Use(Vector3.right);
        }
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
