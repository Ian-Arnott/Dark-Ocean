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
    private float _currentTime = 0f; // Current time the player has been looking at the monster
    private bool _isFollowing = false; // Flag to indicate if the monster is following the player

    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;
    private bool _detected;
    private bool _seen;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        TeleportToLocation(_locations[0]); // Start at the first location
    }

    void Update()
    {
        if (IsMonsterVisible() && DetectPlayer())
        {
            _currentTime += Time.deltaTime;
            EventManager.instance.LookTimeChange(_currentTime);
            // Trigger event when time reaches maximum
            if (_currentTime >= _timeMax)
            {
                EventManager.instance.EventGameOver(false);
            }
            _agent.enabled = false;
            _isFollowing = false;
            _seen = true;
        }
        else
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

            int aux = Random.Range(0, 100);

            if (_seen && !_isFollowing && aux < _teleportChance)
            {
                // Teleport to a random location
                int randomIndex = Random.Range(0, _locations.Count);
                TeleportToLocation(_locations[randomIndex]);
                _seen = false;
            }
            else if (_seen && !_isFollowing && aux > _teleportChance)
            {
                _agent.enabled = true;
                _agent.SetDestination(_player.transform.position);
                _isFollowing = true;
                _seen = false;
            }
            else if (!_seen && _isFollowing && aux > _teleportChance)
            {
                _agent.SetDestination(_player.transform.position);
                _isFollowing = true;
            }
        }
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
