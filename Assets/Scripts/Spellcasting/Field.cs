using UnityEngine;

public class Field: MonoBehaviour
{
    public delegate void ApplyEffect(GameObject target);
    public ApplyEffect EffectCallback;

    [SerializeField] private float duration;
    [SerializeField] private Collider _collider;

    private float _timer = 0f;

    public void OnTriggerEnter(Collider other)
    {
        EffectCallback?.Invoke(other.gameObject);
    }

    public void Init()
    {
        _collider.enabled = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
