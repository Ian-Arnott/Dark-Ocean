using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] private List<Transform> _locations;
    [SerializeField] private float _timeMax = 5f; // Maximum time to trigger the game end
    [SerializeField] private int _teleportChance = 20; // Chance of teleporting instead of following player
    [SerializeField] private NavMeshAgent _agent; // Reference to the NavMeshAgent component
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    private float _currentTime = 0f; // Current time the player has been looking at the monster
    private float _timeSinceTeleported = 0f;
    private bool _isFollowing = false; // Flag to indicate if the monster is following the player
    private bool _teleported = true;
    private bool _detected = false;
    private bool _seen = false;
    private Vector3 _previousPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _speed = 3f;
        _agent.enabled = false;
        _previousPosition = transform.position;
        TeleportToLocation(_locations[0]); // Start at the first location
        EventManager.instance.OnKeyPickup += IncreaseDifficulty;
    }

    void Update()
    {
        if(_teleported) _timeSinceTeleported += Time.deltaTime;

        if (IsMonsterVisible() && DetectPlayer()) HandleVisibleMonster();
        else HandleInvisibleMonster();

    }

    private void HandleVisibleMonster()
    {
        _currentTime += Time.deltaTime;
        EventManager.instance.LookTimeChange(_currentTime);

        if (_currentTime >= _timeMax) EventManager.instance.EventGameOver(false);

        _agent.enabled = false;
        _isFollowing = false;
        _seen = true;
    }

    private void HandleInvisibleMonster()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            EventManager.instance.LookTimeChange(_currentTime);
        }
        else
        {
            _currentTime = 0f;
            EventManager.instance.LookTimeChange(_currentTime);
        }

        int d100 = 0;
        if (_seen) d100 = Random.Range(0, 100);

        if (_seen && !_isFollowing && d100 < _teleportChance) TeleportMonster();
        else if (_seen && !_isFollowing && d100 >= _teleportChance) FollowPlayer();
        else if (!_seen && _isFollowing && d100 >= _teleportChance) FollowPlayer();
        
        if (_teleported && _timeSinceTeleported >= 2 * _timeMax) TeleportMonster();
        
    }

    private void TeleportMonster()
    {
        int randomIndex = Random.Range(0, _locations.Count);
        TeleportToLocation(_locations[randomIndex]);
        _agent.enabled = false;
        _agent.speed = 0f;
        _seen = false;
        _teleported = true;
        _timeSinceTeleported = 0f;
    }

    private void FollowPlayer()
    {
        _agent.enabled = true;
        _agent.speed = _speed;
        _agent.SetDestination(_player.transform.position);
        _isFollowing = true;
        _seen = false;
        _teleported = false;
        _timeSinceTeleported = 0f;
    }


    private void IncreaseDifficulty()
    {
        _speed += 1f;
        _timeMax -= 0.5f;
        _teleportChance -= 5;
    }
    private bool IsMonsterVisible()
    {
        // Check if the monster is within the screen bounds
        Vector3 monsterScreenPos = Camera.main.WorldToViewportPoint(transform.position);
        return monsterScreenPos.x >= 0f && monsterScreenPos.x <= 1f && monsterScreenPos.y >= 0f && monsterScreenPos.y <= 1f && monsterScreenPos.z > 0f;
    }

    private void TeleportToLocation(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
    }

    private bool DetectPlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + _offset, direction, out hit, Mathf.Infinity))
        {
            _detected = hit.transform.CompareTag(_player.tag);
            // Debug.Log(_detected);
            return _detected;
        }
        return false;
    }

}
