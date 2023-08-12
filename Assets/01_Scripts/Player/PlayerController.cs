using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IHitable
{
    public int hp = 3;
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float invincibleTime = 1f;

    public Transform groundCheck = null;
    public LayerMask groundLayer;

    public SpriteRenderer spriteRenderer = null;
    public Animator animator = null;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private bool _isDoubleJumped;
    private float _groundCheckRadius = 0.05f;
    private bool _isLeftView = false;
    private bool _isDie = false;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CollisionCheck();

        if (!_isDie)
        {
            KeyboardHandler();
        }

        animator.SetBool("isJump", !_isGrounded);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        bool rightHit = Physics2D.Linecast(transform.position, transform.position + transform.right * 0.1f, groundLayer);

        bool leftHit = Physics2D.Linecast(transform.position, transform.position + transform.right * -0.1f, groundLayer);

        if (!_isGrounded)
        {
            if (!_isDoubleJumped && Input.GetKeyDown(KeyCode.Space))
            {
                _isDoubleJumped = true;
                Jump();
            }

            if (rightHit)
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Min(_rigidbody2D.velocity.x, 0), _rigidbody2D.velocity.y);
            }
            else if (leftHit)
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Max(_rigidbody2D.velocity.x, 0), _rigidbody2D.velocity.y);
            }
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
    }

    private void KeyboardHandler()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetBool("isMove", horizontalInput != 0f);
        if (horizontalInput != 0)
        {
            bool newDirection = Mathf.Sign(horizontalInput) < 0 ? true : false;
            if(_isLeftView  != newDirection)
            {
                _isLeftView = newDirection;
                spriteRenderer.flipX = _isLeftView;
            }
            _rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, _rigidbody2D.velocity.y);
        }
    }

    private void CollisionCheck()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, _groundCheckRadius, groundLayer))
        {
            _isGrounded = true;
            _isDoubleJumped = false;
        }
        else {
            _isGrounded = false;
        }
    }

    public void Hit(int damage)
    {
        Debug.Log(hp);
        hp -= damage;


        if (hp <= 0) {
            Die();
        }
    }
    public void Die() {
        transform.Rotate(Vector3.forward * 20f * Time.deltaTime);
        _isDie = true;
        animator.SetTrigger("isDie");
    }
}
