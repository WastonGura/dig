using System.ComponentModel.Composition;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{   
    public float MoveSpeed = 3.0f;
    public float flyForce = 1.5f;
    public float digRadius = 1f;
    public bool isFlying;
    public bool isFall;
    // inputController
    public PlayerInputController inputControl;
    public Vector2 inputDirection = Vector2.zero;
    public float digDistance = 0.2f;
    public LayerMask diggableLayer;
    // HealthComponent
    public HealthComponent healthComponent;
    private Rigidbody2D rigit;
    private Animator animator;

    public GameObject bombPrefab;
    public Transform bombSpawnPoint;
    public int bombCount = 3;
    public float bombCooldown = 2f;
    private float lastBombTime;
    private bool canPlaceBomb = true;

    private void Awake()
    {
        rigit = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        inputControl = new PlayerInputController();
        inputControl.Enable();
        lastBombTime = -bombCooldown;
    }

    public void Update()
    {
        HandleInput();
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
        UpdateSpriteState();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateSpriteState()
    {
        bool isMoving = Mathf.Abs(inputDirection.x) > 0.1f;
        bool isDiging = inputControl.Player.Dig.IsPressed();
        bool isRight = inputDirection.x > 0.1f;
        bool isLeft = inputDirection.x < -0.1f;

        
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isDiging", isDiging);
        animator.SetBool("isFall", isFall);
        animator.SetBool("isRight", isRight);
        animator.SetBool("isLeft", isLeft);

    }

    private void HandleInput()
    {
        if (inputDirection.y > 0.2f)
        {
            Fly();
        }

        if (inputControl.Player.Dig.IsPressed())
        {
            Dig();
        }

        if (inputControl.Player.Bomb.IsPressed())
        {
            PlaceBomb();
        }

    }

    public void Move()
    {
        rigit.linearVelocity = new Vector2(inputDirection.x * MoveSpeed * Time.deltaTime, rigit.linearVelocity.y);
    }

    public void Fly()
    {
        rigit.AddForce(new Vector2(0, flyForce * Time.deltaTime), ForceMode2D.Impulse);
        isFlying = true;
    }

    public void Dig(){
        Vector2 digDirection = Vector2.down;
        if (inputDirection.x != 0) {
            digDirection = new Vector2(inputDirection.x, 0).normalized;
        }
        
        // 调整射线起点为角色底部中心
        Vector2 rayStart = new Vector2(
            transform.position.x,
            GetComponent<Collider2D>().bounds.min.y // 获取碰撞体底部Y坐标
        );
        
        // 增加可视化调试射线（正式版可移除）
        Debug.DrawRay(rayStart, digDirection * digDistance, Color.red, 1f);
        
        RaycastHit2D hit = Physics2D.Raycast(rayStart, digDirection, digDistance, diggableLayer);
        if (hit.collider != null) {
            if (hit.collider.TryGetComponent<Tilemap>(out var tilemap))
            {
                Vector3Int cellPosition = tilemap.WorldToCell(hit.point);
                tilemap.SetTile(cellPosition, null);
            }
            else
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isFlying = false;
        }
    }
    public void PlaceBomb()
    {
        if (canPlaceBomb && bombCount > 0 && Time.time > lastBombTime + bombCooldown)
        {
            Instantiate(bombPrefab, bombSpawnPoint.position, bombSpawnPoint.rotation);
            bombCount--;
            lastBombTime = Time.time;
            canPlaceBomb = false;
            Invoke(nameof(ResetBombState), bombCooldown);
        }
    }

    private void ResetBombState()
    {
        canPlaceBomb = true;
    }

}
