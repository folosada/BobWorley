using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    public float aceleracao = 980f;
    public float velocidadeMaxima = 6;    
    private bool isGrounded = true;
    private float forcaPulo = 0f;
    public Tilemap ground;
    private float jumpTime;
	// Use this for initialization
	void Start () {
        forcaPulo = aceleracao * GetComponent<Rigidbody2D>().mass;
	}
	
	// Update is called once per frame
	void Update () {        

        isGrounded = Physics2D.IsTouching(GetComponent<CapsuleCollider2D>(), ground.GetComponent<TilemapCollider2D>());

        CapsuleCollider2D playerCollider = GetComponent<CapsuleCollider2D>();       

        /*if (isGrounded)
        {
            
            playerCollider.bounds
        }*/

        GetComponent<Animator>().SetBool("isGrounded", isGrounded);        
        float movimento = Input.GetAxis("Horizontal");        
        if (movimento < 0)
        {
            GetComponent<Animator>().SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (movimento > 0)
        {
            GetComponent<Animator>().SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = false;
        } else
        {
            GetComponent<Animator>().SetBool("Walking", value: false);
        }
        Rigidbody2D rigibody = GetComponent<Rigidbody2D>();        
        rigibody.velocity = new Vector2(movimento * velocidadeMaxima, rigibody.velocity.y);        
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigibody.AddForce(new Vector2(0, forcaPulo));                
                GetComponent<Animator>().SetBool("Jumping", value: true);
                jumpTime = Time.time;
            }
            if (Time.time - jumpTime > 0.5)
            {
                GetComponent<Animator>().SetBool("Jumping", value: false);
            }            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.Log("Colisão: " + collision);
            Debug.Log("Contato: " + contact);
            Debug.Log("Ponto de Contato: " + contact.point);
        }        
    }
}
