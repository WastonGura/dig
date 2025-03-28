using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public InputSystem_Actions inputControl;
    private Rigidbody2D rigit;
    public float MoveSpeed = 3.0f;
    public Vector2 inputDirection = Vector2.zero;

    void Start()
    {
        rigit = GetComponent<Rigidbody2D>();
        inputControl = new InputSystem_Actions();
    }

    void Update()
    {
        HandleInput();

        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


    }

    public void Move()
    {
        rigit.linearVelocityX = inputDirection.x * MoveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        rigit.AddForce(Vector2.up * 200);
    }
}
