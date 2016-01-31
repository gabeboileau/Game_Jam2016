using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IEnemy
{

	public Transform playerTrans;
	public int speed;
	public float attackCooldown;

	private const int MAX_HEALTH = 10;
	private Animator m_Animator;
	private int m_CurrentHealth;
	private Vector3 m_MoveDirection;
	private float cooldownTimer;
	private bool canAttack = false;

	void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_CurrentHealth = MAX_HEALTH;
		cooldownTimer = attackCooldown;
	}

	void Update()
	{
		m_Animator.SetFloat("Horizontal", GetDirectionToPlayer().x);
		m_Animator.SetFloat("Vertical", GetDirectionToPlayer().y);

		if (playerTrans != null)
		{
			m_MoveDirection = GetDirectionToPlayer()*speed*Time.deltaTime;

			transform.position += m_MoveDirection;
		}


		if (GetDistanceFromPlayer() <= 0.3f)
		{
			//Attack();
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

	Vector3 GetDirectionToPlayer()
	{
		return (playerTrans.position - transform.position).normalized;
	}

	float GetDistanceFromPlayer()
	{
		return Vector3.Distance(transform.position, playerTrans.position);
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
		m_CurrentHealth -= aAmount;
		if (m_CurrentHealth <= 0)
		{
			Death();
		}
	}

	void Death()
	{
		Destroy(gameObject);
	}
}
