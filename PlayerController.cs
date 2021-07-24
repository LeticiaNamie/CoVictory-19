using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D playerRigidbody;
	bool clicked;
	float position;
	public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
    	Time.timeScale = 1;
        playerRigidbody = GetComponent<Rigidbody2D>();
        GameManager.Instance.GameStarted = false;
        GameManager.Instance.GameOver = false;
    }

    // Update is called once per frame
    void Update()
    { 
    	position = Player.transform.position.y;

        if (Input.GetButtonDown("Jump"))
        {
            if (!GameManager.Instance.GameStarted)
            {
                GameManager.Instance.GameStarted = true;
            }

            if (!GameManager.Instance.GameOver && position <= -245)
            {
                clicked = true;
            }

        }

    }

    private void FixedUpdate()
    {
        if (clicked)
        {
            playerRigidbody.velocity = Vector2.up * GameManager.Instance.ClickForce;
            clicked = false;
        }
    }

}
