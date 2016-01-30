using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour
{
	public Transform playerTrans;
	public GameObject enemy;
	public int timeBetweenSpawns;

	void Start()
	{
		StartCoroutine(cr_Spawner());
	}


	IEnumerator cr_Spawner()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBetweenSpawns);
			Spawn();
		}
		
	}




	public void Spawn()
	{
		Instantiate(enemy, transform.position, Quaternion.identity);
		enemy.GetComponent<Enemy>().playerTrans = playerTrans;
	}
}
