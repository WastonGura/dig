using UnityEngine;

public class DestructibleTile : MonoBehaviour
{
    public GameObject orePrefab;
    public ParticleSystem destroyEffect;

    public void DestroyTile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        if (orePrefab != null){
            Instantiate(orePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}