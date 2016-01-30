using UnityEngine;
using System.Collections;

public class Player_Cultist : MonoBehaviour, IDamageable
{
	public int immunityTime;
	public int playerMovementSpeed;
	public Transform m_ImpSpot;



	private const int MAX_HEALTH = 100;
	private int m_CurrentHealth;

	private float m_VerticalInput = 0;
	private float m_HorizontalInput = 0;

	private bool m_HasImp = false;
	private Imp currentImpInHand;

	private Vector3 m_MoveDirectiom;

	void Awake()
	{
		m_MoveDirectiom = Vector3.zero;
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

		m_MoveDirectiom = new Vector3(m_HorizontalInput, m_VerticalInput, 0);
		transform.position += m_MoveDirectiom * playerMovementSpeed * Time.deltaTime;

		if (m_MoveDirectiom.x > 0.2)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}

		else
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}

	void UpdateInput()
	{
		m_VerticalInput = Input.GetAxis("Vertical");
		m_HorizontalInput = Input.GetAxis("Horizontal");
	}


	void OnCollisionEnter2D(Collision2D aCollision)
	{
		if (!m_HasImp)
		{
			if (aCollision.gameObject.GetComponent<Imp>() != null)
			{
				currentImpInHand = aCollision.gameObject.GetComponent<Imp>();
				aCollision.transform.position = m_ImpSpot.position;
				aCollision.transform.parent = transform;
				m_HasImp = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D aCollider2D)
	{
		if (currentImpInHand != null)
		{
			if (aCollider2D.CompareTag("Fire"))
			{
				//Collided with the fire
				Game_Controller.AddToDemonBar(10);
				GameObject.Destroy(currentImpInHand.gameObject);
				currentImpInHand = null;
				m_HasImp = false;
			}
		}
	}




}
