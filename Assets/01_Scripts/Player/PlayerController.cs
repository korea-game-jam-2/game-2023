using PlayerState;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHitable
{
    public int hp = 3;
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float invincibleTime = 1f;

    public Transform groundCheck = null;
    public LayerMask groundLayer;

    public LayerMask monsterHitboxLayer;

    public SpriteRenderer spriteRenderer = null;
    public Animator animator = null;
    public Rigidbody2D rb2D = null;

    private StateMachine<PlayerController> _machine;

    private void Awake()
    {
        _machine = new StateMachine<PlayerController>(new MovableState(), this);
        _machine.AddState(new DieState(), this);
    }

    void Update()
    {
        _machine.Execute();
    }
    public void Hit(int damage)
    {
        hp -= damage;

        if (hp <= 0) {
            _machine.ChangeState<DieState>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == monsterHitboxLayer)
        {
            IHitable hitable = collision.GetComponentInParent<IHitable>();
            if (hitable != null) {
                hitable.Hit(1);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        }
    }
}

namespace PlayerState{
    public class MovableState : IState<PlayerController>
    {
        private PlayerController _player;
        private bool _isGrounded;
        private bool _isDoubleJumped;
        private float _groundCheckRadius = 0.05f;
        private bool _isLeftView = false;


        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            CheckGround();
            KeyboardHandler();

            if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            ObstacleCheck();

            if (!_isGrounded)
            {
                if (!_isDoubleJumped && Input.GetKeyDown(KeyCode.Space))
                {
                    _isDoubleJumped = true;
                    Jump();
                }
            }
        }

        private  void ObstacleCheck()
        {
            Vector3 origin = _player.transform.position;
            Vector3 rayDistance = _player.transform.right * 0.1f;
            bool rightHit = Physics2D.Linecast(origin, origin + rayDistance, _player.groundLayer);
            bool leftHit = Physics2D.Linecast(origin, origin - rayDistance, _player.groundLayer);

            if (!_isGrounded)
            {
                if (rightHit)
                {
                    _player.rb2D.velocity = new Vector2(Mathf.Min(_player.rb2D.velocity.x, 0), _player.rb2D.velocity.y);

                    Debug.DrawLine(origin, origin + rayDistance,  Color.green);
                }
                else if (leftHit)
                {
                    _player.rb2D.velocity = new Vector2(Mathf.Max(_player.rb2D.velocity.x, 0), _player.rb2D.velocity.y);

                    Debug.DrawLine(origin, origin - rayDistance, Color.green);
                }
            }
        }

        public void Exit()
        {
        }

        public void Initialize(PlayerController context)
        {
            _player = context;
        }
        private void KeyboardHandler()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            _player.animator.SetBool("isMove", horizontalInput != 0f);
            if (horizontalInput != 0)
            {
                CheckViewSide(horizontalInput);

                _player.rb2D.velocity = new Vector2(horizontalInput * _player.moveSpeed, _player.rb2D.velocity.y);
            }
        }

        private void CheckViewSide(float horizontalInput)
        {
            bool newDirection = Mathf.Sign(horizontalInput) < 0 ? true : false;
            if (_isLeftView != newDirection)
            {
                _isLeftView = newDirection;
                _player.spriteRenderer.flipX = _isLeftView;
            }
        }
        private void Jump()
        {
            _player.rb2D.velocity = new Vector2(_player.rb2D.velocity.x, _player.jumpForce);
            _player.animator.SetBool("isJump", true);
        }
        private void CheckGround()
        {
            if (_player.rb2D.velocity.y > 1f) {
                _isGrounded = false;
                return;
            }

            if (Physics2D.OverlapCircle(_player.groundCheck.position, _groundCheckRadius, _player.groundLayer))
            {
                _isGrounded = true;
                _isDoubleJumped = false;
                _player.animator.SetBool("isJump", false);
            }
            else
            {
                _isGrounded = false;
            }

            
        }
    }
    
    public class DieState : IState<PlayerController>
    {
        private PlayerController _player;

        public void Enter()
        {
            _player.animator.SetTrigger("isDie");
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(PlayerController context)
        {
            _player = context;
        }
    }
}
