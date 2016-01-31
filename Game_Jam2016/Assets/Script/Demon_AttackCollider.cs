using UnityEngine;
using System.Collections;

public class Demon_AttackCollider : MonoBehaviour
{

	void OnEnable()
	{
		StartCoroutine(cr_DeathTimer());
	}

	void OnTriggerEnter2D(Collider2D aCollider2D)
	{
		Debug.Log(aCollider2D.name);
		if (aCollider2D.GetComponent<IEnemy>() != null)
		{
			aCollider2D.GetComponent<IEnemy>().TakeDamage(10);
			gameObject.SetActive(false);
		}
	}

	IEnumerator cr_DeathTimer()
	{
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}
}
