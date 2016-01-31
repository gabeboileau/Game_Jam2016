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

	public GameObject bottomCollider;
	public GameObject topCollider;
	public GameObject rightCollider;
	public GameObject leftCollider;

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


		if (GetDistanceFromPlayer() <= 5.0f && canAttack)
		{
			Attack();
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
		if (playerTrans != null)
		{
			return (playerTrans.position - transform.position).normalized;
		}
		else
		{
			return Vector3.zero;
		}
	}

	float GetDistanceFromPlayer()
	{
		if (playerTrans != null)
		{
			return Vector3.Distance(transform.position, playerTrans.position);

		}
		else
		{
			return 0;
		}
	}


	void Attack()
	{
		if (GetDirectionToPlayer().x > 0.5)
		{
			m_Animator.SetTrigger("AttackRight");
			rightCollider.SetActive(true);
		}

		else if (GetDirectionToPlayer().x < -0.5)
		{
			m_Animator.SetTrigger("AttackLeft");
			leftCollider.SetActive(true);
		}

		else if (GetDirectionToPlayer().y > 0.5)
		{
			m_Animator.SetTrigger("AttackUp");
			topCollider.SetActive(true);
		}

		else if (GetDirectionToPlayer().y < -0.5)
		{
			bottomCollider.SetActive(true);
			m_Animator.SetTrigger("AttackDown");
		}

		cooldownTimer = attackCooldown;
		canAttack = false;
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
