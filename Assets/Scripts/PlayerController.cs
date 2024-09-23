//Lucas de la Pena
// 9/22/2024
// This handles player movement, timer, win/lose conditions, and restart button


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed; //public so it will appear in the unity interface
    private int count;
    private float Timer = 60.0f;
    public Text countText;
    public Text loseText;
    public bool gameOver = false;
    public Text winText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //initializing
        //count = 0;
        //countText.text = "Count: " + count.ToString();
        countText.text = "Time: " + Timer.ToString();
        winText.text = "";
        loseText.text = "";
        restartButton.gameObject.SetActive(false);//hides the button at start
        
    }


    void Update()
    {
        //Timer countdown real time
        //Timer = 0.0f;
        Timer -= Time.deltaTime;
        int seconds = (int)(Timer % 60);
        countText.text = "Time: " + seconds.ToString();
        
        if (Timer <= 0)
        {
            if (gameOver)
            {
                Timer = 0.0f;
                return;
            }
            winText.text = "You WIN!";
            restartButton.gameObject.SetActive(true); //Show the button
            Timer = 0.0f;
        }
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            if (gameOver)
            {
                return;
            }
            if (Timer <= 0)
            {
                return;
            }
            gameOver = true;
            loseText.text = "Game Over";
            restartButton.gameObject.SetActive(true); //Show the button
            
        }

        if (other.gameObject.CompareTag("WinnerPickUp"))
        {
            //if collected 4 winner pick ups, player wins

            other.gameObject.SetActive(false); // disappear from scene
            count++;
            if (gameOver)
            {
                return;
            }

            if (count >= 4)
            {
                winText.text = "You Win!";
                restartButton.gameObject.SetActive(true); //Show the button
                gameOver = true;
            }
         
        }


        /*
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); // disappear from scene
            count++;
            countText.text = "Count: " + count.ToString();
        }
        if (count >= 12)
        {
            winText.text = "You Win!";
            restartButton.gameObject.SetActive(true); //Show the button

        }
        */

    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); //restart game
    }

    
}
