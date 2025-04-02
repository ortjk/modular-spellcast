using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private PlayerCamera _playerCamera;
    [SerializeField]
    private Transform _cameraFollowPoint;
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    public int _maxMana = 100, _maxHealth = 100, _manaPerSecond = 1, _healthPerSecond = 2;
    [SerializeField]
    private Wand _wand;
    [SerializeField]
    private SpellList _spells;
    private GameObject _equippedWand;
    private Transform _wandTransform;
    [SerializeField]
    private Inventory _inventory;

    [SerializeField] 
    private MenuController _menuController;
    [SerializeField] 
    private SpellMenu _spellMenu;
    

    private PlayerInputs _inputs = new PlayerInputs();
    private Vector3 _lookInputVector;
    

    public int _coins;
    public float _currentMana, _currentHealth;

    public void Damage(DamageInfo info)
    {
        AudioManager._audioManager.PlayPlayerSound("PlayerHurt");
        _currentHealth -= (int)info.amount;
        if(_currentHealth <= 0)
        {
            _menuController.GameOver();
        }
    }

    private void Start()
    {
        _wandTransform = GameObject.FindGameObjectsWithTag("WandTransform")[0].GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        _playerCamera.SetFollowTransform(_cameraFollowPoint);
        _currentHealth = _maxHealth;
        _currentMana = _maxMana;
        for (int i = 0; i < 2; i++)
        {
            _inventory.AddSpell(typeof(DoubleSpell));
            _inventory.AddSpell(typeof(FireBolt));
            _inventory.AddSpell(typeof(MagicBolt));
            _inventory.AddSpell(typeof(ManaModifier));
            _inventory.AddSpell(typeof(SpeedModifierSpell));
            _inventory.AddSpell(typeof(MagicSpark));
            _inventory.AddSpell(typeof(RockThrow));
            _inventory.AddSpell(typeof(MinorHeal));
            _inventory.AddSpell(typeof(IceShard));
            _inventory.AddSpell(typeof(MeteorBlast));
            _inventory.AddSpell(typeof(Dash));  
        }
    }

    private void OnMove(InputValue value)
    {
        var rawMove = value.Get<Vector2>();
        _inputs.MoveAxisForward = rawMove.y;
        _inputs.MoveAxisRight = rawMove.x;
        
        _playerController.SetInputs(ref _inputs);
    }

    private void OnLook(InputValue value)
    {
        var rawLook = value.Get<Vector2>();
        _lookInputVector = new Vector3(rawLook.x, rawLook.y, 0);

        _inputs.CameraRotation = _playerCamera.transform.rotation;
        _playerController.SetInputs(ref _inputs);
    }

    private void OnJump(InputValue value)
    {
        _inputs.JumpPressed = value.isPressed;
        _playerController.SetInputs(ref _inputs);
        _inputs.JumpPressed = false;
    }

    private void OnPause(InputValue value)
    {
        _menuController.Pause();
    }

    private void OnResume(InputValue value)
    {
        if (_spellMenu.gameObject.activeInHierarchy)
        {
            _spellMenu.Close(_inventory, _wand);
            _menuController.ResumeTime();
        }
        else if(_menuController._settingMenuUI.activeSelf)
        {
            _menuController._settingMenuUI.SetActive(false);
            _menuController.ResumeTime();
        }
        else if(_menuController._pauseMenuUI.activeSelf)
        {
            _menuController.Resume();
        }
    }

    private void OnCast(InputValue value)
    {
        _wand.Use(_playerCamera.transform.forward, _cameraFollowPoint.position, ref _currentMana);
    }

    private void OnInteract(InputValue value)
    {
        _menuController.StopTime(); 
        _spellMenu.Open(_inventory, _wand);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Gold"))
        {
            _coins += other.gameObject.GetComponent<Loot>()._value;
        }
        else if(other.CompareTag("Spell"))
        {
            _menuController.SpellPopUp(Convert.ToUInt32(other.gameObject.GetComponent<Loot>()._spellindex), other.gameObject);
        }
        else if(other.CompareTag("Wand"))
        {
            _menuController.WandPopUp(_wand.reloadTime, other.gameObject.GetComponent<Loot>()._itemSO._reloadTime, _wand.numSlots, other.gameObject.GetComponent<Loot>()._itemSO._wandSlots, other.gameObject);
        }
    }

    public void EquipWand(GameObject newWand)
    {
        Destroy(GameObject.FindGameObjectsWithTag("PlayerWandModel")[0]);
        _equippedWand = Instantiate(newWand.GetComponent<Loot>()._itemSO._wandGameObject, _wandTransform.position, _wandTransform.rotation, _wandTransform);
        _equippedWand.transform.SetParent(_wandTransform);
        _wand.numSlots = newWand.GetComponent<Loot>()._itemSO._wandSlots;
        _wand.reloadTime = newWand.GetComponent<Loot>()._itemSO._reloadTime;
        _equippedWand.tag = "PlayerWandModel";
        Destroy(newWand);
    }

    public void PickUpSpell(int index)
    {
        _inventory.AddSpell(_spells.spellList[index]);
        Destroy(_menuController._spellbook);
    }

    void Update()
    {
        ManaRegeneration();
        HealthRegeneration();
    }

    private void LateUpdate()
    {
        _playerCamera.UpdateWithInput(Time.deltaTime, _lookInputVector);
    }
    
    private void ManaRegeneration()
    {
        if(_currentMana < _maxMana)
        {
            _currentMana += _manaPerSecond * Time.deltaTime;
        }
    }

    private void HealthRegeneration()
    {
        if(_currentHealth < _maxHealth)
        {
            _currentHealth += _manaPerSecond * Time.deltaTime;
        }
    }
}
