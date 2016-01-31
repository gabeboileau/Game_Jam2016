using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
	public int speedOfDemonDepletion;
	public GameObject m_DemonBar;

	public static bool controller1IsDemon;
	public Text timerText;
	public int startTime;
	private float currentTime;

	//private static int currentDemonSliderAmount;
	private static Slider m_DemonSlider;

	void Start()
	{
		controller1IsDemon = false;
		m_DemonSlider = m_DemonBar.GetComponent<Slider>();
		currentTime = startTime;
		//StartCoroutine(cr_SwitchControllerSupport());
	}

	public static void AddToDemonBar(int aAmount)
	{
		m_DemonSlider.value += aAmount;
	}

	public static void RemoveFromDemonBar(int aAmount)
	{
		m_DemonSlider.value -= aAmount;
	}


	void Update()
	{
		timerText.text = Mathf.Floor(currentTime).ToString();
		m_DemonSlider.value -= Time.deltaTime* speedOfDemonDepletion;

		if (m_DemonSlider.value <= 0)
		{
			//Game Over
			Debug.Log("Game Over");
		}

		if (currentTime > 0)
		{
			currentTime -= Time.deltaTime;

		}
		else
		{
			//Game Over
		}

	}

	IEnumerator cr_SwitchControllerSupport()
	{
		while (true)
		{
			Debug.Log("Change");
			controller1IsDemon = !controller1IsDemon;	
			yield return new WaitForSeconds(5.0f);

		}

	}


}
