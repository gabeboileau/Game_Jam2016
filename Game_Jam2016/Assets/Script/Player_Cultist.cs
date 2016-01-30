using UnityEngine;
using System.Collections;

public class Player_Cultist : MonoBehaviour, IDamageable
{
	public int immunityTime;
	public int playerMovementSpeed;

	private const int MAX_HEALTH = 100;

	private int m_CurrentHealth;

	private float m_VerticalInput = 0;
	private float m_HorizontalInput = 0;

	private Vector3 move;

	void Awake()
	{
		move = Vector3.zero;
		m_CurrentHealth = MAX_HEALTH;
	}


	public void TakeDamage(int aAmountOfDamage)
	{
		//TODO: Make the player flash and be immune for several seconds
		m_CurrentHealth -= aAmountOfDamage;
	}

	void Update()
	{
		UpdateInput();

		move = new Vector3(m_HorizontalInput, m_VerticalInput, 0);
		transform.position += move* playerMovementSpeed * Time.deltaTime;

	}

	void UpdateInput()
	{
		m_VerticalInput = Input.GetAxis("Vertical");
		m_HorizontalInput = Input.GetAxis("Horizontal");
	}


	void OnCollisionEnter2D(Collision2D aCollision)
	{
		Debug.Log(aCollision.gameObject.name);
		if (aCollision.gameObject.GetComponent<Imp>() != null)
		{
			aCollision.transform.parent = transform;
		}
	}

}
