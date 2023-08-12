using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public float jumpHeight = 10.0f;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1f);

        if (hit.collider != null) 
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
            }
        }
    }
}
