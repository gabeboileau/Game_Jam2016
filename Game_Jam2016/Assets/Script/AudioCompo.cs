using UnityEngine;
using System.Collections;

public class AudioCompo : MonoBehaviour {

    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Death;
    public AudioClip Special;
    public AudioClip PowerupLoop;
    public AudioClip Hurt;

    private AudioSource AudioLoop;
    private AudioSource Audio;



	// Use this for initialization
	void Start () {
       Audio= GameObject.Find("SoundPlayer").GetComponent<AudioSource>();
       AudioLoop = GameObject.Find("LoopPlayer").GetComponent<AudioSource>();

	    if(PowerupLoop !=null)
        {
            AudioLoop.loop = true;
            AudioLoop.clip = PowerupLoop;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Attack1Sound()
    {
        Audio.PlayOneShot(Attack1);
    }
    public void Attack2Sound()
    {
        Audio.PlayOneShot(Attack2);
    }
    public void DeathSound()
    {
        Audio.PlayOneShot(Death);
    }
     public void SpecialSound()
    {
        Audio.PlayOneShot(Death);
    }
    public void LoopSound()
    {
        AudioLoop.Play();
    }

    public void StopLoop()
    {
        AudioLoop.Stop();
    }
     public void HurtSound()
    {
        Audio.PlayOneShot(Hurt);
    }

}
