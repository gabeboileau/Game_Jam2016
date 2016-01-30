using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public Transform playerTrans;
	public int speed;

	private Vector3 m_MoveDirection;

	void Start()
	{
		
	}

	void Update()
	{
		if (playerTrans != null)
		{
			m_MoveDirection = GetDirectionToPlayer()*speed*Time.deltaTime;
			transform.position += m_MoveDirection;
		}
	}

	Vector3 GetDirectionToPlayer()
	{
		return (playerTrans.position - transform.position).normalized;
	}

}
