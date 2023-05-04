using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private MovementController _movementController;


    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _MoveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;

    #region COMMANDS
    private CommandMove _cmdMovementForward;
    private CommandMove _cmdMovementBack;
    private CommandMove _cmdMovementLeft;
    private CommandMove _cmdMovementRight;
    #endregion

    void Start()
    {
        _movementController = GetComponent<MovementController>();

        // _cmdMovementForward = new CommandMove(_movementController, transform.forward);
        // _cmdMovementBack = new CommandMove(_movementController, -transform.forward);
        // _cmdMovementLeft = new CommandMove(_movementController, -transform.right);
        // _cmdMovementRight = new CommandMove(_movementController, transform.right);
    }

    void Update()
    {
        // Rotate the character based on mouse input
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime * 100.0f);

        // Move the character based on keyboard input
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEvent(new CommandMove(_movementController, transform.forward));
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddEvent(new CommandMove(_movementController, -transform.forward));
        if (Input.GetKey(_moveRight)) EventQueueManager.instance.AddEvent(new CommandMove(_movementController, transform.right));
        if (Input.GetKey(_MoveLeft)) EventQueueManager.instance.AddEvent(new CommandMove(_movementController, -transform.right));
    }

}
