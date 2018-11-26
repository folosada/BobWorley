using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            if (collision.otherCollider is BoxCollider2D)
            {
                PlayerProperties.jump.Invoke();
                Destroy(GameObject.Find(this.name));
            } else
            {
                PlayerProperties.removeLife();
                PlayerProperties.collideEnemy.Invoke();
            }
        }        
    }
}
