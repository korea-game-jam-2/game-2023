using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Rigidbody2D rb2D = null;

    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null) {
            if(rb2D.velocity.sqrMagnitude > 15f)
            {
                hitable.Hit(damage);
            }
        }
    }
}
