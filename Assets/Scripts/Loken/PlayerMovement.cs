using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour {
    private float moveSpeed = 128f;
    public static float gridSize = 64f;
    private float waitOnLevelSwitch = 0.5f;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Vector2 input;
    private bool isMoving = false;
    private bool movementAllowedAfterExit = true;
    private float playerDirection = 0;
    private InGameUI inGameUi;

    public float powerUpTime;
    private readonly int LOKEN_MAX_HEALTH = 9;
    private readonly int ATK_POWERUP_DURATION = 30;

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        inGameUi = GameObject.Find("Map").GetComponent<InGameUI>();
    }

    void Update()
    {
        powerUpTime += Time.deltaTime;
        if (powerUpTime >= ATK_POWERUP_DURATION) {
            lowerLokenAtk();
        }
        if (!inGameUi.getIsPaused())
        {
            if (PlayerAttacking.aliveState)
            {
                if (!isMoving)
                {
                    input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    disableDiagonalMovement();
                    if (input != Vector2.zero)
                        StartCoroutine(move(transform));
                }
                if (Input.GetButton("Jump"))
                {
                    this.transform.position = new Vector3(2944, -384, 0);
                } 
            }
            else
            {
                levelEnd();
            }
        }
    }

    private IEnumerator move(Transform transform)
    {
        isMoving = true;
        animateChar();
        Vector3 startPosition = transform.position;
        float time = 0;
        Vector3 endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        boxCollider2D.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(startPosition, input, gridSize + 2);
        boxCollider2D.enabled = true;
        if (isAllowedToMove(hit))
        {
            while (time < 1f)
            {
                time += Time.deltaTime * (moveSpeed / gridSize);
                transform.position = Vector3.Lerp(startPosition, endPosition, time);
                yield return null;
            }
            transform.position = endPosition;
        }
        if (movementAllowedAfterExit)
        {
            isMoving = false;
        }
        animateChar();
        yield return 0;
    }

    private bool isAllowedToMove(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            return true;
        }
        else if (hit.collider.tag == "Exit")
        {
            return true;
        }
        else if (hit.collider.tag == "Enemy")
        {
            Debug.Log("Walking Into Enemy");
            return false;
        }
        else
        {
            return false;
        }
    }
    private void disableDiagonalMovement()
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            input.y = 0;
        }
        else
        {
            input.x = 0;
        }
        decidePlayerDirection();
    }

    private void decidePlayerDirection()
    {
        if (input.x == 1)
        {
            playerDirection = 2;
        }
        else if (input.x == -1)
        {
            playerDirection = 4;
        }
        else if (input.y == 1)
        {
            playerDirection = 1;
        }
        else if (input.y == -1)
        {
            playerDirection = 3;
        }
        animator.SetFloat("direction", playerDirection);
    }

    private void animateChar()
    {
        if (isMoving)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("input_x", input.x);
            animator.SetFloat("input_y", input.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.name == "trapdoorLevel2")
        {
            levelEnd();
        }
        else if (collisionObject.name == "trapdoor") {
            loadLevelTwo();
        }



        else if (collisionObject.name == "HealthPowerUp") {
            if (PlayerAttacking.playerHealth < LOKEN_MAX_HEALTH) {
                PlayerAttacking.playerHealth += 1;
                Destroy(collisionObject.gameObject);
            }
        } 
        else if (collisionObject.name == "AttackPowerUp") {
                PlayerAttacking.playerDMG += 1;
                Destroy(collisionObject.gameObject);
                powerUpTime = 0;
        }
    }
    private void lowerLokenAtk() {
        if (PlayerAttacking.playerDMG > 1) {
            PlayerAttacking.playerDMG -= 1;
        }
    }

    private void loadLevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void levelEnd()
    {
        movementAllowedAfterExit = false;
        Scoreboard.writeToScoreboard(SetName.getCharacterName(), PlayerAttacking.currentScore);
        Invoke("loadHighScoreScene", waitOnLevelSwitch);
    }

    public void loadHighScoreScene()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Highscore");
    }
}
