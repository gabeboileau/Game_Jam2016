using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Imp : MonoBehaviour
{
	public int speed;
	public List<GameObject> listOfPatrolPoints;

	private Vector3 m_MoveDirection;
	private GameObject m_CurrentPatrol;

	void Awake()
	{
		speed = 2;
		//listOfPatrolPoints = new List<GameObject>();

		if (listOfPatrolPoints != null)
		{
			m_CurrentPatrol = ChooseRandomPatrol();
		}
	}

	void Update()
	{
		Debug.Log(Vector3.Distance(transform.position, m_CurrentPatrol.transform.position));
		if (Vector3.Distance(transform.position, m_CurrentPatrol.transform.position) > 0.5)
		{
			
			m_MoveDirection = GetDirectionToPatrol(m_CurrentPatrol)*speed*Time.deltaTime;
			transform.position += m_MoveDirection;
		}
		else
		{
			m_CurrentPatrol = ChooseRandomPatrol();
		}
		
	}

	GameObject ChooseRandomPatrol()
	{
		int ranIndex = UnityEngine.Random.Range(0, listOfPatrolPoints.Count);
		return listOfPatrolPoints[ranIndex];
	}


	Vector3 GetDirectionToPatrol(GameObject aPatrolPosition)
	{
		return (aPatrolPosition.transform.position - transform.position).normalized;
	}
}
