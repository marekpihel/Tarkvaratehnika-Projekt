using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private float waitOnLevelSwitch = 1f;
    private bool isMoving = false;
    private bool movementAllowedAfterExit = true;
    private Vector2 input;                                  //  Implement Attack animations
    public int playerHealth = 50;                           //  Implement DMG taken
    public int playerAttackDMG = 1;                         //  Implement DMG done
    private string playerName;                              //  Implement Attack keybinds!
    private double gameTimeElapsed;                         //  Implement Score loading on Death!
    private Text healthText;
    private int currentScore;
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
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));            // Input.GetAxisRaw Võib olla parem, kuid siis peab vaatama animatsioone
            disableDiagonalMovement();
            if (input != Vector2.zero)
                StartCoroutine(move(transform));
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
        animateCharMovement();
        Vector3 startPosition = transform.position;
        float time = 0;
        Vector3 endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        boxCollider2D.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(startPosition, input, gridSize);
        if (isAllowedToMove(hit))
        {
            boxCollider2D.enabled = true;
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
        animateCharMovement();
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
        currentScore = (int)(1 / (Mathf.Sqrt((float)gameTimeElapsed)) * 1000);
        Scoreboard.WriteScoreboard(SetName.getCharacterName(), currentScore);
        Invoke("loadHighScoreScene", waitOnLevelSwitch);
    }

    private void loadHighScoreScene()
    {
        SceneManager.LoadScene("Highscore");
    }


    private void disableDiagonalMovement()
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            input.y = 0;
        else
            input.x = 0;
    }


    private void updateUI()
    {
        healthText.text = playerHealth.ToString();
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + gameTimeElapsed.ToString();
    }


    private void animateCharMovement()
    {
        if (isMoving)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("input_x", input.x);
            animator.SetFloat("input_y", input.y);
        }
        else if (false)
        {
            //Deniss to make pixle art about attack animations!
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
