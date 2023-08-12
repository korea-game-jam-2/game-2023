using PlayerState;
using System;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public Rigidbody2D rb2D = null;

    public GameObject pauseMenu;
    public GameObject hitEffect;


    private bool isPause = false;
    private bool _invincible = false;

    public StateMachine<PlayerController> machine;
    private UiManager uiManager = null;

    private void Awake()
    {
        machine = new StateMachine<PlayerController>(new MovableState(), this);
        machine.AddState(new DieState(), this);
        machine.AddState(new FreezedState(), this);
        machine.AddState(new HitState(), this);

        uiManager = FindObjectOfType<UiManager>();
    }

    void Update()
    {
        machine.Execute();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Pause();

            }
            else
            {
                Resume();
            }
        }
    }
    public void SetFreeze(bool freeze)
    {
        if (freeze)
        {
            machine.ChangeState<FreezedState>();
        }
        else
        {
            machine.ChangeState<MovableState>();
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = true;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = false;
    }
    public void Hit(int damage)
    {
        if (_invincible && damage < 1000) return;

        hp -= damage;
        if (hp >= 0)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            uiManager.HealthDown(hp);
            machine.ChangeState<HitState>();
        }

        if (hp <= 0)
        {
            machine.ChangeState<DieState>();
        }

        _invincible = true;
        spriteRenderer.color = spriteRenderer.color * new Color(1f, 1f, 1f, 0.3f);
        Invoke(nameof(ResetInvincible), 2f);
    }
    private void ResetInvincible()
    {
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
        _invincible = false;
    }
    public void ResetState()
    {
        hp = 3;
        machine.ChangeState<MovableState>();
    }

}

namespace PlayerState
{
    public class MovableState : IState<PlayerController>
    {
        private PlayerController _player;
        private bool _isGrounded;
        private bool _isDoubleJumped;
        private float _groundCheckRadius = 0.05f;
        private bool _isLeftView = false;


        public void Enter()
        {
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

        private void ObstacleCheck()
        {
            Vector3 origin = _player.transform.position;
            Vector3 rayDistance = _player.transform.right * 0.1f;
            Vector3 headPosition = new Vector2(0, 0.4f);
            bool rightHit = Physics2D.Linecast(origin, origin + rayDistance, _player.groundLayer);
            if (!rightHit)
            {

                rightHit = Physics2D.Linecast(origin, origin + rayDistance + headPosition, _player.groundLayer);
            }
            bool leftHit = Physics2D.Linecast(origin, origin - rayDistance, _player.groundLayer);
            if (!leftHit)
            {
                leftHit = Physics2D.Linecast(origin, origin - rayDistance + headPosition, _player.groundLayer);
            }
            if (!_isGrounded)
            {
                if (rightHit)
                {
                    _player.rb2D.velocity = new Vector2(Mathf.Min(_player.rb2D.velocity.x, 0), _player.rb2D.velocity.y);

                    Debug.DrawLine(origin, origin + rayDistance, Color.green);
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
            if (_player.rb2D.velocity.y > 1f)
            {
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

        private float _respawnCoolTime = 3f;
        public void Enter()
        {
            _player.animator.SetBool("isDie", true);
            _respawnCoolTime = 3f;
        }

        public void Execute()
        {
            _respawnCoolTime -= Time.deltaTime;
            if (_respawnCoolTime < 0)
            {
                GameManager.Instance.Respawn();
            }
        }

        public void Exit()
        {
            _player.animator.SetBool("isDie", false);
        }

        public void Initialize(PlayerController context)
        {
            _player = context;
        }
    }

    public class FreezedState : IState<PlayerController>
    {
        public void Enter()
        {
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(PlayerController context)
        {
        }
    }
    public class HitState : IState<PlayerController>
    {
        private float _stunTime = 0.5f;
        private PlayerController _player;
        private float _flickerTime = 0.1f;
        private bool _isRed = false;
        public void Enter()
        {
            _stunTime = 0.5f;
            _flickerTime = 0.1f;
            // ³Ë¹é
            //float sign = _player.rb2D.velocity.x > 0 ? -1 : 1;
            //_player.rb2D.AddForce(new Vector2( 150f * sign, 100f));
        }

        public void Execute()
        {
            _stunTime -= Time.deltaTime;
            _flickerTime -= Time.deltaTime;
            if (_flickerTime < 0f)
            {
                _flickerTime = 0.1f;
                float alpha = _player.spriteRenderer.color.a;
                _player.spriteRenderer.color = _isRed ?  new Color (1,0,0, alpha) : new Color(1,1,1,alpha);
                _isRed = !_isRed;
            }
            if (_stunTime < 0f)
            {
                _player.machine.Redo();
            }
        }

        public void Exit()
        {
            _player.spriteRenderer.color = new Color(1,1,1, _player.spriteRenderer.color.a);
        }

        public void Initialize(PlayerController context)
        {
            _player = context;
        }
    }
}
