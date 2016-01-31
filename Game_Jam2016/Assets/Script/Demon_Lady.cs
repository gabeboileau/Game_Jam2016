using UnityEngine;
using System.Collections;

public class Demon_Lady : MonoBehaviour, IPlayer
{
	public int playerMovementSpeed;

    private AudioCompo m_Audio;
	private float m_VerticalInput = 0;
	private float m_HorizontalInput = 0;
	private Animator m_Animator;

	private Vector3 m_MoveDirection;

	public GameObject bottomCollider;
	public GameObject topCollider;
	public GameObject rightCollider;
	public GameObject leftCollider;

    

	public float attackCooldown;
	private float cooldownTimer;
	private bool canAttack = false;
  


	void Start()
	{
        m_Audio = GetComponent<AudioCompo>();
		m_Animator = GetComponent<Animator>();
		cooldownTimer = attackCooldown;
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

		if (cooldownTimer > 0)
		{
			UpdateCooldownTimer();
			canAttack = false;
		}
		else
		{
			canAttack = true;
		}
	}

	void UpdateCooldownTimer()
	{
		cooldownTimer -= Time.deltaTime;
	}


	void UpdateInput()
	{
		m_Animator.SetFloat("Horizontal", m_HorizontalInput);
		m_Animator.SetFloat("Vertical", m_VerticalInput);

		if (!Game_Controller.controller1IsDemon)
		{
			m_VerticalInput = Input.GetAxis("Joystick2Vertical");
			m_HorizontalInput = Input.GetAxis("Joystick2Horizontal");

			if (Input.GetButtonDown("Joystick2FireImp") && canAttack)
			{
				Attack();
				cooldownTimer = attackCooldown;
			}
		}
		else
		{
			m_VerticalInput = Input.GetAxis("Joystick1Vertical");
			m_HorizontalInput = Input.GetAxis("Joystick1Horizontal");

			if (Input.GetButtonDown("Joystick1FireImp") && canAttack)
			{
				Attack();
				cooldownTimer = attackCooldown;
			}
		}
	}


	void Attack()
	{
		if (m_MoveDirection.x > 0.5)
		{
			Debug.Log("Right");
			m_Animator.SetTrigger("RightAttack");
			rightCollider.SetActive(true);
		}

		else if (m_MoveDirection.x < -0.5)
		{
			Debug.Log("Left");
			m_Animator.SetTrigger("LeftAttack");
			leftCollider.SetActive(true);
		}

		else if (m_MoveDirection.y > 0.5)
		{
			Debug.Log("Up");
			m_Animator.SetTrigger("UpAttack");
			topCollider.SetActive(true);
		}

		else if (m_MoveDirection.y < -0.5)
		{
			Debug.Log("Down");
			bottomCollider.SetActive(true);
			m_Animator.SetTrigger("DownAttack");
		}
        m_Audio.Attack1Sound();
	}

	public void TakeDamage(int aAmount)
	{
		Game_Controller.RemoveFromDemonBar(10);
	}
}
