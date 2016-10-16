using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    float speed = 128.0f;                         // Speed of movement
    Rigidbody2D rbody;
    Animator anim;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
    }

    void OnTriggerEnter2D(Collider2D collisionObject) {
        if (collisionObject.name == "trapdoor") {
            Scoreboard.WriteScoreboard(SetName.getCharacterName(), (int)GameTime.getPlayedTime());
            SceneManager.LoadScene("Highscore");
        }
    }
}
