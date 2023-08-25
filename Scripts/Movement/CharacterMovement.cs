using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoSingleton<CharacterMovement>
{
    [SerializeField] private CanvasBasedMoveInputs MovementInputs;
    [SerializeField] private Rigidbody _rigidbody;
    public GameObject moveInputPanel;
    

    private bool _moving;

    public bool IsMoving
    {
        get=>_moving; set=>_moving = value;
    }


   [SerializeField] private float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }


    [SerializeField] private float moveSpeedRef=400;

    public float MoveSpeedRef
    {
        get { return moveSpeedRef; }
        set { moveSpeedRef = value;  }
    }


    private void Move(Vector3 direction)
    {
            _rigidbody.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    private void Rotate(Vector3 rotation)
    {
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
        
    }
    private void MoveStatusController(Vector3 direction)
    {
        if (direction.magnitude <= 0) _moving = false;
        else _moving = true;

        
    }

    private void FixedUpdate()
    {
        
       var direction = new Vector3(MovementInputs.Direction.x, 0, MovementInputs.Direction.y);
        
       
        var rotation = new Vector3(MovementInputs.Rotation.x, 0, MovementInputs.Rotation.y);
        Move(direction);
        Rotate(rotation);
        

    }
    private void Update()
    {
        var direction = new Vector3(MovementInputs.Direction.x, 0, MovementInputs.Direction.y);
        MoveStatusController(direction);
        
    }
}
