using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings

	const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
	[SerializeField] public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	[SerializeField] private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	[SerializeField] private Vector2 m_Velocity = Vector2.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
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
				colliders[i].ClosestPoint(transform.position);
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump, float rot)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			//Debug.Log("Made it here! "+move * 10f+"; "+ m_Rigidbody2D.velocity.y);
			// Move the character by finding the target velocity
			Vector2 targetVelocity = m_Rigidbody2D.velocity + new Vector2(move * 20f, 0).Rotate(rot+Mathf.PI/2);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			//Vector2 jumpvec = new Vector2(0f, m_JumpForce).Rotate(rot-Mathf.PI/2);
			//debug.Log("Jumping! "+jumpvec.x+"; "+jumpvec.y );
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce).Rotate(rot+Mathf.PI/2));
		}
		//debug.Log("gravity force:"+Mathf.Cos(rot)+";"+Mathf.Sin(rot));

		m_Rigidbody2D.AddForce(new Vector2(Mathf.Cos(rot),Mathf.Sin(rot))*9.81f, ForceMode2D.Force);
	}
	
	public void InactiveMove(float rot)
	{
		m_Rigidbody2D.velocity = Vector2.zero;

		//m_Rigidbody2D.AddForce(new Vector2(Mathf.Cos(rot),Mathf.Sin(rot))*9.81f, ForceMode2D.Force);
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

public static class Vector2Extension {
     
	public static Vector2 Rotate(this Vector2 v, float rads) {
		float sin = Mathf.Sin(rads);
		float cos = Mathf.Cos(rads);
         
		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}
