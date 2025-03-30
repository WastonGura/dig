using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    public float checkRadius;
    public LayerMask groundLayer;
    public bool isGround;

    // Update is called once per frame
    private void Update()
    {
        Check();
    }

    public void Check(){
        isGround = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
    }
}
