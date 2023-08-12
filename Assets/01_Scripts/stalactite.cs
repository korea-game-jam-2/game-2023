using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactite : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        IHitable hitable = collision.GetComponent<IHitable>();
        if (hitable != null)
        {
            hitable.Hit(damage);
        }
    }
}
