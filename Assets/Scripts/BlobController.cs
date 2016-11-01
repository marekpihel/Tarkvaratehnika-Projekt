using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlobController : MonoBehaviour
{
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private bool isMoving = false;
    private bool movementAllowedAfterExit = true;
    private Vector2 input;                                //  Implement Attack animations
    public int blobHealth = 3;                            //  Implement DMG taken
    public int blobAttackDMG = 1;                         //  Implement DMG done
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private int playerHealth;


    public void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    public void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));            // Input.GetAxisRaw Võib olla parem, kuid siis peab vaatama animatsioone
            disableDiagonalMovement();
            if (input != Vector2.zero)
                StartCoroutine(move(transform));
        }
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
        else if (hit.collider.tag == "Player")
        {
            //Implement collision with Player, DMG taken and done.
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
