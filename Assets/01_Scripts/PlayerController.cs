using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private bool _isDoubleJumped;
    private float _groundCheckRadius = 0.05f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CollisionCheck();
        KeyboardHandler();

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

         if (!_isGrounded)
        {
            if (!_isDoubleJumped && Input.GetKeyDown(KeyCode.Space))
            {
                _isDoubleJumped = true;
                Jump();
            }

            bool rightHit = Physics2D.Linecast(transform.position, transform.position + transform.right * 0.1f, groundLayer);

            bool leftHit = Physics2D.Linecast(transform.position, transform.position + transform.right * -0.1f, groundLayer);

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
        _rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, _rigidbody2D.velocity.y);
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
}
