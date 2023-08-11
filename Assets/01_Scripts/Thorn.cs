///kyoungSoo

using UnityEngine;

public class Thorn : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hitable = collision.GetComponent<IHitable>();
        if (hitable != null)
        {
            hitable.Hit(damage);
        }
    }
}
