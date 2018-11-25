using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Bau : MonoBehaviour {

    public AudioClip sound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            GetComponent<Animator>().SetBool("collided", true);
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
}
