using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Imp_SpawnController : MonoBehaviour
{
	public GameObject imp;
	public List<GameObject> listOfImpSpawners;


	void Start()
	{
		StartCoroutine(cr_SpawnImp());
			
	}


	IEnumerator cr_SpawnImp()
	{
		while (true)
		{
			SpawnImp();
			yield return new WaitForSeconds(5);
		}
	}


	void SpawnImp()
	{
		int randomIndex = UnityEngine.Random.Range(0, listOfImpSpawners.Count);
		Instantiate(imp, listOfImpSpawners[randomIndex].transform.position, Quaternion.identity);
		imp.GetComponent<Imp>().listOfPatrolPoints = listOfImpSpawners;
	}
}
