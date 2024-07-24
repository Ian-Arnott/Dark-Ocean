using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 2f;
    [SerializeField] private Flashlight _flash;

    // BINDING  KEYS
    [SerializeField] private KeyCode _interact = KeyCode.E;
    [SerializeField] private KeyCode _toggle = KeyCode.Mouse0;
    [SerializeField] private KeyCode _throwFlashbang = KeyCode.Mouse1;

    #region COMMANDS
    private CommandLight _commandLight;
    #endregion

    // SWITCH LIGHT
    [SerializeField] private KeyCode _flashlight = KeyCode.Alpha1;
    [SerializeField] private KeyCode _lantern = KeyCode.Alpha2;
    [SerializeField] private List<Flashlight> _flashlights;
    private bool _isOn;

    // FLASHBANG PREFAB
    [SerializeField] private GameObject _flashbangPrefab;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private int _stunlightAmount;
    [SerializeField] private float _throwCooldown;
    [SerializeField] private float _throwDelta;

    // MONSTER JUMPSCARE
    [SerializeField] private GameObject _monster;
    [SerializeField] private Animator _monsterAnimator;
    [SerializeField] private FirstPersonController _firstPersonController;

    void Start()
    {
        _isOn = false;
        ChangeInventory(0);
        EventManager.instance.ThrowLight(_stunlightAmount);
        EventManager.instance.OnGameOver += HandleGameOver;
    }

    private void HandleGameOver(bool isVictory)
    {
        if (!isVictory)
        {
            StartCoroutine(Jumpscare());
        }
    }

    IEnumerator Jumpscare()
    {
        _monster.SetActive(true);
        Destroy(_firstPersonController);
        Vector3 startPosition = _monster.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0,1,0);

        float duration = 0.3f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            _monster.transform.position = Vector3.Lerp(startPosition,endPosition,elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _monster.transform.position = endPosition;
        _monsterAnimator.Play("Demon|Spasm");
    }

    void Update()
    {
        if (_throwDelta > 0)
        {
            _throwDelta -= Time.deltaTime;
        } else _throwDelta = 0;

        if (Input.GetKeyDown(_interact))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _interactDistance))
            {
                if (hit.transform.CompareTag("Console"))
                {
                    EventQueueManager.instance.AddEvent(new CommandDoor(hit.transform.parent.GetComponent<IDoor>()));
                }
            }
            Debug.DrawRay(transform.position, transform.forward * _interactDistance, Color.green, 1f); // Draw the raycast
        }
        if (Input.GetKeyDown(_toggle))
        {
            EventQueueManager.instance.AddEventToQueue(_commandLight);
            _isOn = !_isOn;
        }
        if (Input.GetKeyDown(_flashlight)) ChangeInventory(0);
        if (Input.GetKeyDown(_lantern)) ChangeInventory(1);

        if (Input.GetKeyDown(_throwFlashbang) && _stunlightAmount > 0 && _throwDelta <= 0)
        {
            _throwDelta = _throwCooldown;
            ThrowFlashbang();
        }
    }

    private void ChangeInventory(int index)
    {
        if (_isOn)
        {
            EventQueueManager.instance.AddEventToQueue(_commandLight);
            _isOn = false;
        }
        foreach (var light in _flashlights) light.gameObject.SetActive(false);
        _flashlights[index].gameObject.SetActive(true);
        EventManager.instance.LightChange(index);
        _flash = _flashlights[index];
        _commandLight = new CommandLight(_flash);
    }

    private void ThrowFlashbang()
    {
        GameObject flashbang = Instantiate(_flashbangPrefab, _throwPoint.position, _throwPoint.rotation);
        IThrowable throwable = flashbang.GetComponent<IThrowable>();
        throwable.Throw();
        _stunlightAmount -= 1;
        EventManager.instance.ThrowLight(_stunlightAmount);
    }
}
