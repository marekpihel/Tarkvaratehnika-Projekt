using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private float waitOnLevelSwitch = 0.5f;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Vector2 input;
    private bool isMoving = false;
    private bool movementAllowedAfterExit = true;



    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAttacking.aliveState)
        {
            if (!isMoving)
            {
                input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));            // USE GetAxisRaw, edit animations to get same amount of frames, speak with Denis
                disableDiagonalMovement();
                if (input != Vector2.zero)
                    StartCoroutine(move(transform));
            }

            //FOR DEMOING PURPOSE

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
    private void disableDiagonalMovement()
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            input.y = 0;
        else
            input.x = 0;
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
        if (collisionObject.name == "trapdoor")
            levelEnd();
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
