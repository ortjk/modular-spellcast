using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerCamera _playerCamera;
    [SerializeField]
    private Transform _cameraFollowPoint;
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    public int _maxMana = 100, _maxHealth = 100, _manaPerSecond = 1;
    [SerializeField]
    private Wand _wand;
    private Transform _wandTransform;


    private PlayerInputs _inputs = new PlayerInputs();
    private Vector3 _lookInputVector;
    private Spell[] _spells;

    public int _coins;
    public float _currentMana, _currentHealth;
    

    private void Start()
    {
        _wandTransform = GameObject.FindGameObjectsWithTag("WandTransform")[0].GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        _playerCamera.SetFollowTransform(_cameraFollowPoint);
        _currentHealth = _maxHealth;
        _currentMana = _maxMana;
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
        _inputs.PausePressed = value.isPressed;
        _playerController.SetInputs(ref _inputs);
        _inputs.PausePressed = false;
    } 

    private void OnCast(InputValue value)
    {
        _wand.Use(Vector3.right, Vector3.zero, ref _currentMana);
    }

    private void OnInteract(InputValue value)
    {
        _inputs.SpellMenuPressed = value.isPressed;
        _playerController.SetInputs(ref _inputs);
        _inputs.SpellMenuPressed = false;
        _wand.SetSpells(_spells);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Gold"))
        {
            _coins += other.gameObject.GetComponent<Loot>()._value;
        }
        else if(other.CompareTag("Spell"))
        {
            
        }
        else if(other.CompareTag("Wand"))
        {
            Destroy(GameObject.FindGameObjectsWithTag("PlayerWandModel")[0]);
            _wand.wandGameObject = Instantiate(other.gameObject.GetComponent<Loot>()._wand.wandGameObject, _wandTransform.position, _wandTransform.rotation, _wandTransform);
            _wand.wandGameObject.transform.SetParent(_wandTransform);
            _wand.tag = "PlayerWandModel";
        }
    }

    void Update()
    {
        ManaRegeneration();
    }

    private void LateUpdate()
    {
        _playerCamera.UpdateWithInput(Time.deltaTime, _lookInputVector);
    }
    
    private void ManaRegeneration()
    {
        if(_currentMana < _maxMana)
        {
            _currentMana = _currentMana + (_manaPerSecond * Time.deltaTime);
        }
    }
}
