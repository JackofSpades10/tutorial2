using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public bool level2 = false;
    public Text score;
    public Text winText;
    public Text lifeText;
    public int lives = 3;
    private int scoreValue = 0;
    public AudioClip winSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        lifeText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
        }

        if (scoreValue == 4 && !level2) {
            transform.position = new Vector3(35, 2, 0);
            lives = 3;
            level2 = true;
        } 
        if (scoreValue > 7) {
            winText.text = "you win! a game by Jack Murray";
            audioSource.PlayOneShot(winSound, 0.7f);
        }
           if (lives <= 0) {
            Destroy(gameObject);
            winText.text = "you lose :(";
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

               if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            lifeText.text = lives.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 2), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}