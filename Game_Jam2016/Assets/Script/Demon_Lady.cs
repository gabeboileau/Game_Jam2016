using UnityEngine;
using System.Collections;

public class Demon_Lady : MonoBehaviour, IPlayer
{
	public int playerMovementSpeed;


	private float m_VerticalInput = 0;
	private float m_HorizontalInput = 0;
	private Animator m_Animator;

	private Vector3 m_MoveDirection;



	void Start()
	{
		m_Animator = GetComponent<Animator>();
	}

	void Update()
	{
		UpdateInput();

		m_MoveDirection = new Vector3(m_HorizontalInput, m_VerticalInput, 0);
		transform.position += m_MoveDirection * playerMovementSpeed * Time.deltaTime;

		if (m_MoveDirection.magnitude > 0.1)
		{
			m_Animator.SetBool("isMoving", true);
		}
		else
		{
			m_Animator.SetBool("isMoving", false);
		}
	}


	void UpdateInput()
	{
		m_Animator.SetFloat("Horizontal", m_HorizontalInput);
		m_Animator.SetFloat("Vertical", m_VerticalInput);

		if (!Game_Controller.controller1IsDemon)
		{
			m_VerticalInput = Input.GetAxis("Joystick2Vertical");
			m_HorizontalInput = Input.GetAxis("Joystick2Horizontal");

			if (Input.GetButton("Joystick2FireImp"))
			{
				Attack();
			}
		}
		else
		{
			m_VerticalInput = Input.GetAxis("Joystick1Vertical");
			m_HorizontalInput = Input.GetAxis("Joystick1Horizontal");

			if (Input.GetButton("Joystick1FireImp"))
			{
				Attack();
			}
		}
	}

	void Attack()
	{
		if (m_MoveDirection.x > 0.5)
		{
			m_Animator.SetTrigger("RightAttack");
		}

		else if (m_MoveDirection.x < -0.5)
		{
			m_Animator.SetTrigger("LeftAttack");
		}

		else if (m_MoveDirection.y > 0.5)
		{
			m_Animator.SetTrigger("UpAttack");
		}

		else if (m_MoveDirection.y < -0.5)
		{
			m_Animator.SetTrigger("DownAttack");
		}
	}

	public void TakeDamage(int aAmount)
	{
		Game_Controller.RemoveFromDemonBar(10);
	}
}
