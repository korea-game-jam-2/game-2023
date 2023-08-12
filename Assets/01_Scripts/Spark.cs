
using UnityEngine;


public class Spark : MonoBehaviour
{
    Animator anim;
    public bool isMirror = false;
    public int damage = 1;
    
    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        anim.SetBool("isMirror", isMirror);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hitable = collision.GetComponent<IHitable>();
        if (hitable != null)
        {
            hitable.Hit(damage);
        }
    }
}
