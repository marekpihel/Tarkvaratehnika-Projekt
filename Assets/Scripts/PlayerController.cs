using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
Todo:

Implement Attack animations
Implement DMG taken
Implement DMG done
Implement Attack keybinds!
Implement Score loading on Death!
*/

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private float waitOnLevelSwitch = 1f;
    private bool isMoving = false;
    private bool isAttacking = false;
    private bool movementAllowedAfterExit = true;
    private int playerDirection = 0;
    private Vector2 input;                                 
    public int playerHealth = 50;                          
    public int playerAttackDMG = 1;  
    private string playerName;                             
    private double gameTimeElapsed;                         
    private Text healthText;
    public static int currentScore = 0;
    private Animator animator;
    private BoxCollider2D boxCollider2D;


    public void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
        playerName = SetName.getCharacterName();
        gameTimeElapsed = GameTime.getPlayedTime();
    }


    public void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.name == "trapdoor")
            levelEnd();
    }


    public void Update()
    {
        gameTimeElapsed = Mathf.Round((float)GameTime.getPlayedTime());
        if (!isMoving && !isAttacking)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));            // USE GetAxisRaw, edit animations to get same amount of frames, speak with Denis
            disableDiagonalMovement();
            decidePlayerDirection();
            if (input != Vector2.zero)
                StartCoroutine(move(transform));
        }
        if (Input.GetButton("Fire1"))
        {
            print(playerDirection);
            isAttacking = true;
            animateChar();
            isAttacking = false;
        }
        if (Input.GetButton("Jump"))
        {
            print("Teleport");
            this.transform.position = new Vector3(2944, -384, 0);
        }
        if (Input.GetButton("Cancel"))
        {
            escMenu();
        }
        updateUI();
    }


    private IEnumerator move(Transform transform)
    {
        isMoving = true;
        animateChar();
        Vector3 startPosition = transform.position;
        float time = 0;
        Vector3 endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        boxCollider2D.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(startPosition, input, gridSize);
        boxCollider2D.enabled = true;
        if (isAllowedToMove(hit))
        {
            while (time < 1f)
            {
                time += Time.deltaTime * (moveSpeed / gridSize);
                transform.position = Vector3.Lerp(startPosition, endPosition, time);
                yield return null;
            }
        }
        else
        {
            //TO DO SOMETHING WHEN HITS THE WALL
        }
        if (movementAllowedAfterExit)
            isMoving = false;
        animateChar();
        yield return 0;
    }


    private bool isAllowedToMove(RaycastHit2D hit)
    {
        if (hit.collider == null)
            return true;
        else if (hit.collider.tag == "Exit")
            return true;
        else if (hit.collider.tag == "Enemy")
        {
            //Implement collision with enemy, DMG taken and done.
            return false;
        }
        else
            return false;
    }


    private void escMenu()
    {
        SceneManager.LoadScene("MainMenu");
        // Implement for Ceisi to make ingame pause maneu.
    }


    private void levelEnd()
    {
        movementAllowedAfterExit = false;
        Scoreboard.WriteScoreboard(SetName.getCharacterName(), currentScore);
        Invoke("loadHighScoreScene", waitOnLevelSwitch);
    }

    private void loadHighScoreScene()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Highscore");
    }


    private void disableDiagonalMovement()
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            input.y = 0;
        else
            input.x = 0;
    }

    private void decidePlayerDirection()
    {
        if (input.x == 1)
            playerDirection = 2;
        else if (input.x == -1)
            playerDirection = 4;
        else if (input.y == 1)
            playerDirection = 1;
        else if (input.y == -1)
            playerDirection = 3;
    }


    private void updateUI()
    {
        healthText.text = playerHealth.ToString();
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + gameTimeElapsed.ToString();
    }


    private void animateChar()
    {
        if (isMoving && !isAttacking)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("input_x", input.x);
            animator.SetFloat("input_y", input.y);
        }
        else if (!isMoving && isAttacking)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            animator.SetFloat("playerDirection", playerDirection);   
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }


    public static void addPointsToCurrentScore(int points)
    {
        currentScore += points;
    }
}
