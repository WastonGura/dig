using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float monsterMoveSpeed = 2f;
    public LayerMask obstacleLayer;
    public AttackComponent attackComponent;
    public HealthComponent healthComponent;
    private Rigidbody2D monsterRb;
    private Animator animator;
    private int moveDirection = 1;
    private Vector3 originalScale;
    void Awake()
    {
        monsterRb = GetComponent<Rigidbody2D>();
        monsterRb.freezeRotation = true;
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update() {
        monsterRb.linearVelocity = new Vector2(moveDirection * monsterMoveSpeed, monsterRb.linearVelocityY);
        CheckObstacle();
        SetEnemyDie();
    }

    private void SetEnemyDie(){
        if (healthComponent.CheckDie()){
            animator.SetBool("isDie", true);
            Destroy(gameObject, 1.0f);
        }
        else{
            animator.SetBool("isDie", false); 
        }
    }

    void CheckObstacle() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * moveDirection, 0.5f, obstacleLayer); // 0.5f is an example
        if (hit.collider != null)
        {
            moveDirection *= -1;
            Flip();
        }
        else
        {
        Vector2 raycastOrigin = transform.position + Vector3.down * 0.5f + Vector3.right * 1f * moveDirection; // 增加下移量和水平偏移
        RaycastHit2D groundHit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.5f, obstacleLayer); // 增加检测距离
        if(groundHit.collider == null){
            moveDirection *= -1;
            Flip();
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().healthComponent.TakeDamage(attackComponent.GetAttackDamage());
        }
    }

    private void Flip()
    {
        Vector3 newScale = originalScale;
        newScale.x *= Mathf.Sign(moveDirection);
        transform.localScale = newScale;
    }

}