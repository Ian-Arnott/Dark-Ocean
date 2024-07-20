using System.Collections;
using UnityEngine;

public class Flashbang : MonoBehaviour, IThrowable
{
    [SerializeField] private float _flashDuration = 1f;
    [SerializeField] private float _maxIntensity = 8f;
    private Light _flashLight;
    [SerializeField] private Rigidbody _rb;

    void Start()
    {
        _flashLight = gameObject.AddComponent<Light>();
        _flashLight.type = LightType.Point;
        _flashLight.range = 10f;
        _flashLight.intensity = 0f;

        _rb = gameObject.GetComponent<Rigidbody>();
        if (_rb == null) _rb = gameObject.AddComponent<Rigidbody>();
        
    }

    public void Throw()
    {
       if (_rb != null) _rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _rb.isKinematic = true; // This will stop the Rigidbody from simulating physics
            _rb.detectCollisions = false; // Optionally disable collision detection
            EventManager.instance.EventStun();
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        _flashLight.intensity = _maxIntensity;
        yield return new WaitForSeconds(_flashDuration);
        _flashLight.intensity = 0f;
        Destroy(gameObject);  // Destroy the flashbang after it flashes
    }
}
