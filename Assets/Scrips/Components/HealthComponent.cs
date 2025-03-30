using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;
    
    public float GetHealth(){
        return currentHealth;
    }

    public void SetHealth(float health) {
        currentHealth = health;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        SetHealth(currentHealth);
        if (currentHealth <= 0) {
            isDead = true;
        }
    }

    public bool CheckDie(){
        return isDead;
    }

}
