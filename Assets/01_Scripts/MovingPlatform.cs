
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public Animator anim;
    public bool isMirror = false;
    
    private void Start()
    {
        anim.SetBool("isMirror", isMirror);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
