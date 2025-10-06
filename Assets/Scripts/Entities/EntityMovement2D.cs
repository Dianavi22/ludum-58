using System.Runtime.CompilerServices;
using Rewards.Utils;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement2D : MonoBehaviour
{
    /// <summary>
    /// The horizontal speed of the player.
    /// </summary>
    [SerializeField, Tooltip("Player horizontal speed.")] private float _horizontalSpeed;

    /// <summary>
    /// The height of the player when jumping.
    /// </summary>
    [SerializeField, Tooltip("player jump height.")] private float _jumpHeight;

    /// <summary>
    /// The value assigned to [_rigidbody.gravityScale] when falling.
    /// </summary>
    [SerializeField, Tooltip("The value given to the rigidbody's gravity scale when falling")] private float _fallingGravityScale;

    /// <summary>
    /// The layers that allow the player to jump from.
    /// </summary>
    [SerializeField, Tooltip("What the player can jump from.")] private LayerMask _jumpableLayers;

    /// <summary>
    /// Value applied to the rigidbody's velocity each frame when grounded and no horizontal input is provided.
    /// </summary>
    [SerializeField, Range(0f, 1f), Tooltip("Drag to be applied each frame when grounded.")] private float _friction;

    [SerializeField] private float _velocityGravityTreshhold;
    [SerializeField] SuccessMapManager _successMapManager;

    [SerializeField] ParticleSystem _walkPart;
    [SerializeField] ParticleSystem _hitFloorPart;
    [SerializeField] ParticleSystem _deathPart;
    [SerializeField] ShakyCam _sc;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private PlayerSFXManager _sfxManager;

    /// <summary>
    /// The box collider used to check if the player is grounded.
    /// </summary>
    [SerializeField] private BoxCollider2D _groundChecker;

    /// <summary>
    /// This entity's rigidbody2D.
    /// </summary>
    private Rigidbody2D _rigidbody;

    /// <summary>
    /// Whether the entity is touching something defined with the [_jumpableLayers] mask.
    /// </summary>
    private bool _isOnGround;

    /// <summary>
    /// Whether the entity is falling or not.
    /// </summary>
    private bool _isFalling;

    /// <summary>
    /// The entity's rigidbody default gravity scale. 
    /// </summary>
    private float _baseGravityScale;

    private Respawnable _respawnable;
    private BoxCollider2D _collider;
    private int _jumpCounter;
    [SerializeField] Transform _highestPoint;
    private bool _jumpedHigher;

    #region Object lifecycle
    private void Awake()
    {
        _sfxManager = GetComponentInChildren<PlayerSFXManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<BoxCollider2D>();

        TryGetComponent<Respawnable>(out _respawnable);

        if (_respawnable != null)
        {
            _respawnable.Respawn();
        }

        _baseGravityScale = _rigidbody.gravityScale;
    }

    private bool _wasOnGround;

    private void Update()
    {
        if (PauseMenu.IsPause)
        {
            return;
        }

        // Saut si au sol
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _jumpedHigher = _highestPoint.position.y <= transform.position.y;
            _isOnGround = false;
            DoJump();
        }

        if (!_isOnGround)
        {
            _isFalling = _rigidbody.velocity.y <= _velocityGravityTreshhold;

            if (_isFalling && _rigidbody.gravityScale != _fallingGravityScale * 2)
            {
                _animator.SetBool("jumping", false);
                _animator.SetBool("falling", true);
                _rigidbody.gravityScale = _fallingGravityScale * 2;
            }
            else if (!_isFalling && _rigidbody.gravityScale != _fallingGravityScale)
            {
                _rigidbody.gravityScale = _fallingGravityScale;
            }
        }
        else
        {
            _isFalling = false;
            _animator.SetBool("falling", false);

            if (_rigidbody.gravityScale != _baseGravityScale)
            {
                _rigidbody.gravityScale = _baseGravityScale;
            }

            if (!_wasOnGround)
            {
                _hitFloorPart.Play();
            }
        }

        _wasOnGround = _isOnGround;
    }


    [SerializeField] private float idleTimeMax;
    private float idleTimer = 0f;

    private void FixedUpdate()
    {
        if (SuccessMapManager.IsFading || PauseMenu.IsPause || PauseMenu.IsMainMenu)
            return;

        _isOnGround = 0 < Physics2D.BoxCastAll(transform.position, _groundChecker.size, 0, Vector2.down, 0.65f, _jumpableLayers).Length;

        float xInput = Input.GetAxis("Horizontal");
        bool hasInput = Mathf.Abs(xInput) > 0.01f;

        if (hasInput)
        {
            idleTimer = 0f;

            if (Mathf.Abs(xInput) > 0.01f)
            {
                _animator.SetBool("walking", true);
                _rigidbody.velocity = new Vector2(_horizontalSpeed * Input.GetAxisRaw("Horizontal"), _rigidbody.velocity.y);
                _spriteRenderer.flipX = xInput < 0;

                if (_isOnGround && !_walkPart.isPlaying)
                {
                    _walkPart.Play();
                    _animator.SetBool("jumping", false);
                }

                if (_isOnGround && !_sfxManager.IsPlayingWalk)
                {
                    _sfxManager.PlayWalk();
                }
            }

            if (_isOnGround && xInput == 0)
            {
                _rigidbody.velocity *= _friction;
                _walkPart.Stop();
                _animator.SetBool("walking", false);
                _sfxManager.StopWalk();
            }
        }
        else
        {
            idleTimer += Time.fixedDeltaTime;

            if (_walkPart.isPlaying)
            {
                _walkPart.Stop();
                _animator.SetBool("walking", false);
                _sfxManager.StopWalk();
            }


            if (idleTimer >= idleTimeMax)
            {
                _successMapManager.LaunchSuccessAnim(PlayerPrefsData.AFK_SUCCESS);
                idleTimer = 0f;
            }
        }
    }

    #endregion

    #region Collision Callbacks

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && _jumpedHigher)
        {
            _successMapManager.LaunchSuccessAnim(PlayerPrefsData.MEGA_JUMP);
        }

        _sfxManager.PlayHitGround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death") && _respawnable != null)
        {
            _sc.ShakyCameCustom(0.15f, 0.3f);
            _deathPart.gameObject.transform.position = new Vector3(transform.position.x, -5, 0);
            _deathPart.Play();
            Invoke("Respawn", 0.3f);
            _successMapManager.LaunchSuccessAnim(PlayerPrefsData.DEATH_SUCCESS);
        }
    }

    private void Respawn()
    {
        _respawnable.Respawn();
        _sfxManager.PlayDeath();
    }
    #endregion

    /// <summary>                                       
    /// Makes the player jump and set [_isOnGround] to false.
    /// </summary>
    private void DoJump()
    {
        _sfxManager.PlayJump();
        _sfxManager.StopWalk();
        _animator.SetBool("walking", false);
        _animator.SetBool("jumping", true);
        _rigidbody.gravityScale = _fallingGravityScale;
        idleTimer = 0f;
        _isOnGround = false;
        _rigidbody.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
        _jumpCounter++;

        if (_jumpCounter == 50)
        {
            _successMapManager.LaunchSuccessAnim(PlayerPrefsData.JUMPING_SUCCESS);
        }
    }
}