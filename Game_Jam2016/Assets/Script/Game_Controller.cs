using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
	public int speedOfDemonDepletion;
	public GameObject m_DemonBar;

	public static bool controller1IsDemon;
	public Text timerText;
	private float currentTime;
	private static GameObject m_GameOverImage;

	//private static int currentDemonSliderAmount;
	private static Slider m_DemonSlider;

	void Start()
	{
   

		m_GameOverImage = transform.Find("GameOver").gameObject;
		controller1IsDemon = false;
		m_DemonSlider = m_DemonBar.GetComponent<Slider>();
		currentTime = 0;
		//StartCoroutine(cr_SwitchControllerSupport());
        if (Input.GetJoystickNames().Length == 3)
        {
            GameObject.Find("Cultist 3").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Cultist 3").GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (Input.GetJoystickNames().Length == 4)
        {
            GameObject.Find("Cultist 3").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Cultist 4").GetComponent<SpriteRenderer>().enabled = true;

            GameObject.Find("Cultist 3").GetComponent<BoxCollider2D>().enabled = true;
            GameObject.Find("Cultist 4").GetComponent<BoxCollider2D>().enabled = true;

        }
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
		timerText.text = currentTime.ToString();
		//m_DemonSlider.value += Time.deltaTime* speedOfDemonDepletion;

		if (m_DemonSlider.value <= 0)
		{
			//Game Over
			GameOver();
			Debug.Log("Game Over");
		}
		else
		{
			m_DemonSlider.value -= Time.deltaTime * speedOfDemonDepletion;
		}

		currentTime += Time.deltaTime;
		
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

	public static void GameOver()
	{
		Time.timeScale = 0;
		m_GameOverImage.SetActive(true);
	}


}
