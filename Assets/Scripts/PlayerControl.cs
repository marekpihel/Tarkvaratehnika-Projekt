using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;

    private int playerHealth = 50;
    private string playerName;

    private double gameTime;
    private Text healthText;
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private RaycastHit2D hit;
    private BoxCollider2D boxCollider2D;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        healthText = GameObject.Find("healthText").GetComponent<Text>();
        playerName = SetName.getCharacterName();
        gameTime = GameTime.getPlayedTime();
    }

    public void Update()
    {
        gameTime = Mathf.Round((float)GameTime.getPlayedTime());


        if (!isMoving)
        {
            t = 0;
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(move(transform));
            }
        }

        if (Input.GetButton("Jump")) {
            print("Teleport");
            this.transform.position = new Vector3(2944, -384, 0);
        }

        healthText.text = playerHealth.ToString();
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + gameTime.ToString();
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        animator.SetBool("isWalking", true);
        animator.SetFloat("input_x", input.x);
        animator.SetFloat("input_y", input.y);
        startPosition = transform.position;
        t = 0;

        endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        boxCollider2D.enabled = false;
        hit = Physics2D.Raycast(startPosition, input, 64);
        if (hit.collider == null)
        {
            boxCollider2D.enabled = true;
            while (t < 1f)
            {
                t += Time.deltaTime * (moveSpeed / gridSize);
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
        } 
        else if (hit.collider.tag == "Exit")
        {
            boxCollider2D.enabled = true;
            while (t < 1f)
            {
                t += Time.deltaTime * (moveSpeed / gridSize);
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
        }
        else
        {
            //TO DO SOMETHING WHEN HITS THE WALL
        }
        animator.SetBool("isWalking", false);
        isMoving = false;
        yield return 0;
    }



    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.name == "trapdoor") {
            Scoreboard.WriteScoreboard(SetName.getCharacterName(), 300 - (int)GameTime.getPlayedTime());
            SceneManager.LoadScene("Highscore");
        }
    }
}
