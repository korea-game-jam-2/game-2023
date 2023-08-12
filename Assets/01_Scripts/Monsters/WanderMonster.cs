using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMonster : MonoBehaviour, IHitable
{
    public Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float moveSpeed = 1f;
    public int damage = 1;
    public float senseDistance = 0.2f;
    

    public LayerMask groundLayer;

    private int _directionSign = 1;
    private bool _isDie=false;

    private void Update()
    {
        if (_isDie) return;

        rb2D.velocity = new Vector3(_directionSign * moveSpeed, rb2D.velocity.y);

        Vector3 origin = transform.position;
        Vector3 rayDistance = transform.right * senseDistance;
        if (Physics2D.Linecast(origin, origin + rayDistance * _directionSign, groundLayer))
        {
            _directionSign = -_directionSign;
            spriteRenderer.flipX = _directionSign < 0;
        }

        Debug.DrawLine(origin, origin + rayDistance * _directionSign, Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
        {
            hitable.Hit(damage);
        }
    }

    public void Hit(int damage)
    {
        _isDie = true;
        rb2D.velocity = Vector3.zero;
        animator.SetTrigger("isDie");
        Destroy(gameObject, 1f);
    }
}
