using UnityEngine;

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

	[SerializeField, Range(0f, 5f)] private float _velocityGravityTreshhold;

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

	#region Object lifecycle
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		Debug.Log("Fetched respawnable " + TryGetComponent<Respawnable>(out _respawnable));

		if (_respawnable != null)
		{
			_respawnable.Respawn();
		}

		_baseGravityScale = _rigidbody.gravityScale;
	}

	private void Update()
	{
		// Doing a jump if on the ground.
		if (Input.GetAxisRaw("Jump") == 1 && _isOnGround)
		{
			DoJump();
		}

		if (!_isOnGround)
		{
			_isFalling = _rigidbody.velocity.y <= _velocityGravityTreshhold;

			// Updating the gravity and the gravity scale when falling.
			if (_isFalling && _rigidbody.gravityScale != _fallingGravityScale)
			{
				_rigidbody.gravityScale = _fallingGravityScale;
			}
		}
		else
		{
			_isFalling = false;

			// Reverting the changes from gravity and scale when back on the ground.
			if (_rigidbody.gravityScale != _baseGravityScale)
			{
				_rigidbody.gravityScale = _baseGravityScale;
			}
		}
	}

	private void FixedUpdate()
	{
		float xInput = Input.GetAxis("Horizontal");

		if (0 < Mathf.Abs(xInput))
		{
			_rigidbody.velocity = new Vector2(_horizontalSpeed * Input.GetAxisRaw("Horizontal"), _rigidbody.velocity.y);
		}

		if (_isOnGround && xInput == 0)
		{
			_rigidbody.velocity *= _friction;
		}

	}
	#endregion

	#region Collision Callbacks
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (Tools.IsLayerWithinMask(collision.gameObject.layer, _jumpableLayers))
		{
			_isOnGround = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (Tools.IsLayerWithinMask(collision.gameObject.layer, _jumpableLayers))
		{
			_isOnGround = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Death") && _respawnable != null)
		{
			_respawnable.Respawn();
		}
	}
	#endregion

	/// <summary>
	/// Makes the player jump and set [_isOnGround] to false.
	/// </summary>
	private void DoJump()
	{
		_isOnGround = false;
		_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight);
		//_rigidbody.AddForce(new Vector2(0, _jumpHeight), ForceMode2D.Impulse);
	}
}