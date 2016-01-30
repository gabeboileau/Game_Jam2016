﻿using UnityEngine;
using System.Collections;

public class Player_Cultist : MonoBehaviour, IDamageable
{
	public int immunityTime;
	public int playerMovementSpeed;
	public Transform m_RightImpSpot;

	private const int MAX_HEALTH = 100;
	private int m_CurrentHealth;

	private float m_VerticalInput = 0;
	private float m_HorizontalInput = 0;

	private bool m_HasImp = false;
	private Imp currentImpInHand;

	private Vector3 m_MoveDirection;


	void Awake()
	{
		m_MoveDirection = Vector3.zero;
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

		m_MoveDirection = new Vector3(m_HorizontalInput, m_VerticalInput, 0);
		transform.position += m_MoveDirection * playerMovementSpeed * Time.deltaTime;
	}

	void UpdateInput()
	{
		if (Game_Controller.controller1IsDemon)
		{
			m_VerticalInput = Input.GetAxis("Joystick2Vertical");
			m_HorizontalInput = Input.GetAxis("Joystick2Horizontal");

			if (Input.GetButtonDown("Joystick2FireImp"))
			{
				FireImp();
			}

		}
		else
		{
			m_VerticalInput = Input.GetAxis("Joystick1Vertical");
			m_HorizontalInput = Input.GetAxis("Joystick1Horizontal");

			if (Input.GetButtonDown("Joystick1FireImp"))
			{
				FireImp();
			}
		}
	}


	void OnCollisionEnter2D(Collision2D aCollision)
	{
		if (!m_HasImp)
		{
			if (aCollision.gameObject.GetComponent<Imp>() != null)
			{
				currentImpInHand = aCollision.gameObject.GetComponent<Imp>();
				aCollision.transform.position = m_RightImpSpot.position;
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
				Destroy(currentImpInHand.gameObject);
				currentImpInHand = null;
				m_HasImp = false;
			}
		}
	}

	void FireImp()
	{
		if (currentImpInHand != null)
		{
			if (currentImpInHand.gameObject.GetComponent<Rigidbody2D>() == null)
			{
				currentImpInHand.gameObject.AddComponent<Rigidbody2D>();
			}

			currentImpInHand.transform.parent= null;
			currentImpInHand.gameObject.GetComponent<Rigidbody2D>().AddForce(m_MoveDirection * 100);
			//GameObject.Destroy(currentImpInHand.gameObject.GetComponent<Rigidbody2D>());

			//currentImpInHand = null;
			m_HasImp = false;
		}
	}


}
