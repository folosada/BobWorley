using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollision : MonoBehaviour {

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
            PlayerProperties.addFood();
            PlayerProperties.eatFood.Invoke();            
            Destroy(GameObject.Find(this.name));            
        }
    }
}
