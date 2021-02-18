using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
	[Range(0, 5)] [SerializeField] private float m_CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround = 0;
	[SerializeField] private Transform m_GroundCheck = null;
	[SerializeField] private Collider2D m_CrouchDisableCollider = null;
	[SerializeField] private float coyoteTime = .2f;
	[SerializeField] private float jumpBufferlen = .1f;
	[SerializeField] private ParticleSystem dust = null;

	const float k_GroundedRadius = .2f;
	public bool m_Grounded;
	const float k_CeilingRadius = .2f;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;
	private float coyoteCounter;
	private float jumpBufferCounter;
	private bool dJump = false;
	public bool isDJumpActive;
	public bool dJumpCheck;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}
		if (OnCrouchEvent == null)
		{
			OnCrouchEvent = new BoolEvent();
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
				{
					OnLandEvent.Invoke();
				}
			}
		}
		if(m_Grounded)
		{
			coyoteCounter = coyoteTime;
			dJump = true;
			dJumpCheck = false;
			this.GetComponent<PlayerCombat>().enabled = true;
		}
		else
		{
			coyoteCounter -= Time.deltaTime;
			m_Grounded = false;
			this.GetComponent<PlayerCombat>().enabled = false;
		}
	}

	public void Dust()
    {
        dust.Play();
    }

	public void Move(float move, bool crouch, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}
				move *= m_CrouchSpeed;
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = false;
				}
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = true;
				}
				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if(jump)
		{
			jumpBufferCounter = jumpBufferlen;
		}
		else
		{
			jumpBufferCounter -= Time.deltaTime;
		}

		if (coyoteCounter > 0 && jumpBufferCounter >= 0)
		{
			Dust();
			m_Grounded = false;
			coyoteCounter = 0;
			jumpBufferCounter = 0;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
		else if(dJump && jump && isDJumpActive)
		{
			dJumpCheck = true;
			float newJ = m_JumpForce;
			if(m_Rigidbody2D.velocity.y < 0)
			{
				newJ = m_JumpForce * 1.8f;
			}
			m_Rigidbody2D.AddForce(new Vector2(0f, newJ));
			dJump = false;
		}
	}

	private void Flip()
	{
		Dust();
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
