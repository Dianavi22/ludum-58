//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class EntityMovement2D : MonoBehaviour
//{
//    /// <summary>
//    /// The horizontal speed of the player.
//    /// </summary>
//    [SerializeField, Tooltip("Player horizontal speed.")] private float _horizontalSpeed;

//    /// <summary>
//    /// The height of the player when jumping.
//    /// </summary>
//    [SerializeField, Tooltip("player jump height.")] private float _jumpHeight;

//    /// <summary>
//    /// Modified applied to gravity when falling.
//    /// </summary>
//    [SerializeField, Tooltip("Modifier to the gravity when falling.")] private Vector2 _gravityModifier;

//    /// <summary>
//    /// The value assigned to [_rigidbody.gravityScale] when falling.
//    /// </summary>
//    [SerializeField, Tooltip("The value given to the rigidbody's gravity scale when falling")] private float _fallingGravityScale;

//    /// <summary>
//    /// The layers that allow the player to jump from.
//    /// </summary>
//    [SerializeField, Tooltip("What the player can jump from.")] private LayerMask _jumpableLayers;

//    /// <summary>
//    /// This entity's rigidbody2D.
//    /// </summary>
//    private Rigidbody2D _rigidbody;

//    /// <summary>
//    /// Whether the entity is touching something defined with the [_jumpableLayers] mask.
//    /// </summary>
//    private bool _isOnGround;

//    /// <summary>
//    /// Whether the entity is falling or not.
//    /// </summary>
//    private bool _isFalling;

//    /// <summary>
//    /// The default project's gravity.
//    /// </summary>
//    private Vector2 _baseGravity;

//    /// <summary>
//    /// The entity's rigidbody default gravity scale. 
//    /// </summary>
//    private float _baseGravityScale;

//    private void Awake()
//    {
//        _rigidbody = GetComponent<Rigidbody2D>();

//        _baseGravity = Physics2D.gravity;
//        _baseGravityScale = _rigidbody.gravityScale;
//    }

//    private void Update()
//    {   
//        // Doing a jump if on the ground.
//        if (Input.GetAxisRaw("Jump") == 1 && _isOnGround)
//        {
//            DoJump();
//        }

//        if (!_isOnGround)
//        {
//            _isFalling = _rigidbody.linearVelocityY <= 0;

//            // Updating the gravity and the gravity scale when falling.
//            if (_isFalling && (Physics2D.gravity == _baseGravity || _rigidbody.gravityScale != _fallingGravityScale))
//            {
//                Physics2D.gravity *= _gravityModifier;
//                _rigidbody.gravityScale = _fallingGravityScale;
//            }
//        }
//        else
//        {
//            _isFalling = false;

//            // Reverting the changes from gravity and scale when back on the ground.
//            if (_rigidbody.gravityScale != _baseGravityScale || Physics2D.gravity != _baseGravity)
//            {
//                Physics2D.gravity /= _gravityModifier;
//                _rigidbody.gravityScale = _baseGravityScale;
//            }
//        }
//    }

//    private void FixedUpdate()
//    {
//        _rigidbody.AddForceX(_horizontalSpeed * Input.GetAxisRaw("Horizontal"));
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (Tools.IsLayerWithinMask(collision.gameObject.layer, _jumpableLayers))
//        {
//            _isOnGround = true;
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (Tools.IsLayerWithinMask(collision.gameObject.layer, _jumpableLayers))
//        {
//            _isOnGround = false;
//        }
//    }

//    /// <summary>
//    /// Makes the player jump and set [_isOnGround] to false.
//    /// </summary>
//    private void DoJump()
//    {
//        _isOnGround = false;
//        _rigidbody.AddForceY(_jumpHeight, ForceMode2D.Impulse);
//    }
//}