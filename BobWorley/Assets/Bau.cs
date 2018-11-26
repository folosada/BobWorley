using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Bau : MonoBehaviour {

    public AudioClip sound;
    private bool collided;

	// Use this for initialization
	void Start () {
        collided = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !collided)
        {
            collided = true;
            GetComponent<Animator>().SetBool("collided", collided);
            GetComponent<AudioSource>().PlayOneShot(sound);
            PlayerProperties.win.Invoke();
        }
    }
}
