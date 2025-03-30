using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f;
    public GameObject explosionEffect;

    public void TriggerExplosion()
    {
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}