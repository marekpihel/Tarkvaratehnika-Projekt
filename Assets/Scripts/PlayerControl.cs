using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerControl : MonoBehaviour
{
    int playerHealth = 50;
    float speed = 128.0f;                         // Speed of movement
    Rigidbody2D rbody;
    Animator anim;
    Text healthText;
    string playerName;
    double gameTime;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
        playerName = SetName.getCharacterName();
        gameTime = GameTime.getPlayedTime();
    }

    void Update()
    {
        gameTime = Mathf.Round((float)GameTime.getPlayedTime());
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement.x);
            anim.SetFloat("input_y", movement.y);
        } else
        {
            anim.SetBool("isWalking", false);
        }

        rbody.MovePosition(rbody.position + movement * speed * Time.deltaTime);

        healthText.text = playerHealth.ToString();
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + gameTime.ToString();
    }

    void OnTriggerEnter2D(Collider2D collisionObject) {
        if (collisionObject.name == "trapdoor") {
            Scoreboard.WriteScoreboard(SetName.getCharacterName(), (int)GameTime.getPlayedTime());
            SceneManager.LoadScene("Highscore");
        }
    }
}
