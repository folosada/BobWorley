using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float aceleracao = 980f;
    public float velocidadeMaxima = 6;
    private bool isGrounded = true;
    private float forcaPulo = 0f;
    public Tilemap ground;
    private float jumpTime;
    public Canvas canvas;
    public GameObject winPanel;
    private Scene actualScene;
    private Rigidbody2D rigibody;
    private SpriteRenderer spriteRenderer;
    private bool reloadingScene;
    private Color originalColor;
    private Material material;
    private Color[] LoseColorsToFlashBetween = { Color.white, Color.red };
    private Color[] GainColorsToFlashBetween = { Color.white, Color.green };
    private bool isDead = false;
    public AudioClip jumpAudio;
    public AudioClip damageAudio;
    public AudioClip pickupHealthAudio;
    public AudioClip backgroundSong;
    public AudioClip eatingFoodSong;
    private AudioSource audioSource;
    private Animator animator;
    private TilemapCollider2D platformCollider;
    private float distToGround;
    public Button yesButton;
    public Button noButton;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();       
        audioSource.clip = backgroundSong;
        audioSource.volume = 0.1f;
        audioSource.Play();
        reloadingScene = false;
        forcaPulo = aceleracao * GetComponent<Rigidbody2D>().mass;
        actualScene = SceneManager.GetActiveScene();
        this.rigibody = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerProperties.collideMushroom.AddListener(collideMushroom);
        PlayerProperties.collideEnemy.AddListener(collideEnemy);
        PlayerProperties.dead.AddListener(dead);
        PlayerProperties.loseLifeBlink.AddListener(loseLife);
        PlayerProperties.gainLifeBlink.AddListener(gainLife);
        PlayerProperties.eatFood.AddListener(eatingFood);
        PlayerProperties.jump.AddListener(jump);
        PlayerProperties.win.AddListener(win);
        animator = GetComponent<Animator>();
        platformCollider = ground.GetComponent<TilemapCollider2D>();
        distToGround = platformCollider.bounds.extents.y;
        if (yesButton != null)
        {
            yesButton.onClick.AddListener(yesClick);
        }
        if (noButton != null)
        {
            noButton.onClick.AddListener(noClick);
        }
        
    }

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        originalColor = material.color;        
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            dead();
            isDead = false;
        }

        isGrounded = Physics2D.IsTouching(GetComponent<CapsuleCollider2D>(), ground.GetComponent<TilemapCollider2D>());        
        
        animator.SetBool("isGrounded", isGrounded);

        movimentacao();

        Text vidasLabel = canvas.GetComponentInChildren<Text>();
        vidasLabel.text = "VIDAS: " + PlayerProperties.getTotalLife() + "  COMIDAS: " + PlayerProperties.getTotalFood() + "/5";                                
                
    }

    private bool IsGroundedEx()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1f);
    }

    private bool IsGrounded()
    {
        return Physics2D.IsTouching(GetComponent<CapsuleCollider2D>(), ground.GetComponent<TilemapCollider2D>());
    }

    private void movimentacao()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                jump();
                animator.SetBool("Jumping", value: true);
                jumpTime = Time.time;
            }
        }

        float movimento = Input.GetAxis("Horizontal");
        if (movimento < 0)
        {
            animator.SetBool("Walking", true);
            spriteRenderer.flipX = true;
        }
        else if (movimento > 0)
        {
            animator.SetBool("Walking", true);
            spriteRenderer.flipX = false;
        }
        else
            animator.SetBool("Walking", value: false);
        rigibody.velocity = new Vector2(movimento * velocidadeMaxima, rigibody.velocity.y);                       

        if (IsGrounded())
        {
            if (Time.time - jumpTime > 0.5)
                animator.SetBool("Jumping", value: false);
        }
            
    }

    private void jump()
    {
        audioSource.PlayOneShot(jumpAudio, 1.0f);        
        rigibody.AddForce(new Vector2(0, forcaPulo));
    }

    private void OnBecameInvisible()
    {        
        if (!this.reloadingScene)
        {           
            PlayerProperties.removeLife();
            isDead = true;
        }
        reloadingScene = false;
    }

    private void dead()
    {
        audioSource.Stop();
        this.reloadingScene = true;        
        if (PlayerProperties.getTotalLife() == 0)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(actualScene.buildIndex, LoadSceneMode.Single);
        }                
    }

    private void win()
    {                
        winPanel.SetActive(true);                    
    }

    private void yesClick()
    {
        audioSource.Stop();
        winPanel.SetActive(false);
        this.reloadingScene = true;
        SceneManager.LoadScene(actualScene.buildIndex + 1, LoadSceneMode.Single);
    }

    private void noClick()
    {
        Application.Quit();
    }

    private void collideMushroom()
    {
        if (rigibody.velocity.x > 0)
        {
            rigibody.AddForce(new Vector2(-forcaPulo, forcaPulo));
        } else
        {
            rigibody.AddForce(new Vector2(forcaPulo, forcaPulo));
        }
        
        loseLife();
    }        

    private void collideEnemy()
    {
        rigibody.AddForce(new Vector2(forcaPulo, 0));
        loseLife();
    }

    private void loseLife()
    {
        audioSource.PlayOneShot(damageAudio, 1.0f);
        StartCoroutine(FlashObject(0.15f, 0.1f, false));
    }

    private void gainLife() {
        audioSource.PlayOneShot(pickupHealthAudio, 1.0f);
        StartCoroutine(FlashObject(0.15f, 0.1f, true));
    }

    IEnumerator FlashObject(float time, float intervalTime, bool positive)
    {
        float elapsedTime = 0f;
        int index = 0;

        while (elapsedTime < time)
        {
            material.color = positive ? GainColorsToFlashBetween[index % 2] : LoseColorsToFlashBetween[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }

        //Reset color of the material back to the original color
        material.color = originalColor;
    }

    private void eatingFood()
    {
        audioSource.PlayOneShot(eatingFoodSong, 1.0f);
    }
}
