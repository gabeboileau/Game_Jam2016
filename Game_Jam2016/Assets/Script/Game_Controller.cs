using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
	public int speedOfDemonDepletion;
	public GameObject m_DemonBar;

	private static int currentDemonSliderAmount;
	private static Slider m_DemonSlider;

	void Start()
	{
		m_DemonSlider = m_DemonBar.GetComponent<Slider>();
	}


	public static void AddToDemonBar(int aAmount)
	{
		m_DemonSlider.value += aAmount;
	}

	void Update()
	{
		m_DemonSlider.value -= Time.deltaTime* speedOfDemonDepletion;
	}
}
