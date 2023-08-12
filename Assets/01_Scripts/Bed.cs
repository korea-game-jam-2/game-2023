using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public float jumpHeight = 10.0f;
    public float value = 0.5f;

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, Vector2.up, Color.red, value); 
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up,  value, LayerMask.GetMask("Player"));

        if (hit.collider != null) 
        {
            hit.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
        }
    }
}
