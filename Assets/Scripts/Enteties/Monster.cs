using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] private GameObject _monster;
    [SerializeField] private List<Transform> _locations;
    [SerializeField] private float _timeMax = 5f; // Maximum time to trigger the game end
    [SerializeField] private int _teleportChance = 20; // Chance of teleporting instead of following player
    [SerializeField] private NavMeshAgent _agent; // Reference to the NavMeshAgent component
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _teleportSound;
    [SerializeField] private AudioSource _chaseSound;
    private float _currentTime = 0f; // Current time the player has been looking at the monster
    private float _timeSinceTeleported = 0f;
    private float _timeSinceMoving = 0f;
    private bool _isFollowing = false; // Flag to indicate if the monster is following the player
    private bool _teleported = true;
    private bool _detected = false;
    private bool _seen = false;
    private bool _isMoving = false;
    private bool _firstStun = true;
    private bool _isStunned = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _speed = 3f;
        _agent.enabled = false;
        TeleportToLocation(_locations[0]); // Start at the first location
        EventManager.instance.OnKeyPickup += IncreaseDifficulty;
        EventManager.instance.OnStun += HandleStun;
        EventManager.instance.OnGameOver += HandleGameOver;
    }

    void Update()
    {
        UpdateMovementState();
        if (_isStunned) return;
        
        if (_teleported) _timeSinceTeleported += Time.deltaTime;
        if (!_isMoving) _timeSinceMoving += Time.deltaTime;
        
        if (IsMonsterVisible() && DetectPlayer()) HandleVisibleMonster();
        else HandleInvisibleMonster();
    }

    private void UpdateMovementState()
    {
        _isMoving = _agent.velocity.sqrMagnitude > 0.1f;
        _animator.SetBool("isMoving", _isMoving);
        if ((!_isFollowing || !_detected) && _chaseSound.isPlaying) _chaseSound.Stop();
    }

    private void HandleVisibleMonster()
    {
        _agent.enabled = false;
        _isFollowing = false;
        _seen = true;
        _currentTime += Time.deltaTime;
        EventManager.instance.LookTimeChange(_currentTime);
        if (_currentTime >= _timeMax) EventManager.instance.EventGameOver(false);
        else if (_currentTime > _timeMax / 2 && !_chaseSound.isPlaying) // Play sound cue if player looks too long
        {
            _chaseSound.Play();
        }
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
        
        if (_teleported && _timeSinceTeleported >= _timeMax) TeleportMonster();
        if (_isFollowing && !_isMoving && _timeSinceMoving >= _timeMax) GoToRoom();
    }

    private void HandleStun()
    {
        if (!_firstStun)
        {
            _animator.SetTrigger("Stun");
            _agent.enabled = false;
            _agent.speed = 0f;
            _seen = false;
            _teleported = false;
            _isFollowing = false;
            _currentTime = 0f;
            _isStunned = true;
            EventManager.instance.LookTimeChange(_currentTime);
            Invoke("RecoverFromStun", 5.0f);
        }
        else _firstStun = false;
    }

    private void RecoverFromStun()
    {
        _isStunned = false;
        GoToRoom();
    }

    private void TeleportMonster()
    {
        Transform targetLocation = GetRandomNonClosestLocation();
        TeleportToLocation(targetLocation);
        _teleportSound.Play(); // Play teleport sound
        _agent.enabled = false;
        _agent.speed = 0f;
        _seen = false;
        _teleported = true;
        _isFollowing = false;
        _timeSinceTeleported = 0f;
        _timeSinceMoving = 0f;
    }

    private void GoToRoom()
    {
        Transform targetLocation = GetRandomNonClosestLocation();
        StartCoroutine(HesitateBeforeMoving(targetLocation));
    }

    private void FollowPlayer()
    {
        StartCoroutine(HesitateBeforeMoving(_player.transform));
    }

    private IEnumerator HesitateBeforeMoving(Transform target)
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // Hesitation duration
        _agent.enabled = true;
        _agent.speed = _speed;
        _agent.SetDestination(target.position);
        _isFollowing = true;
        _seen = false;
        _teleported = false;
        _timeSinceMoving = 0f;
        _timeSinceTeleported = 0f;
        if (target == _player.transform) _chaseSound.Play(); // Play chase sound if following player
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
            return _detected;
        }
        return false;
    }

    private void HandleGameOver(bool isVictory)
    {
        if (!isVictory)
        {
            _monster.SetActive(false);
        }
    }

    private Transform GetSecondNearestLocation()
    {
        List<Transform> sortedLocations = new List<Transform>(_locations);
        sortedLocations.Sort((a, b) => Vector3.Distance(a.position, _player.transform.position).CompareTo(Vector3.Distance(b.position, _player.transform.position)));
        return sortedLocations.Count > 1 ? sortedLocations[1] : sortedLocations[0];
    }

    private Transform GetRandomNonClosestLocation()
    {
        List<Transform> sortedLocations = new List<Transform>(_locations);
        sortedLocations.Sort((a, b) => Vector3.Distance(a.position, _player.transform.position).CompareTo(Vector3.Distance(b.position, _player.transform.position)));
        sortedLocations.RemoveAt(0); // Remove the closest location
        return sortedLocations[Random.Range(0, sortedLocations.Count)];
    }
}
