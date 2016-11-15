using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour {
    private float gridSize = PlayerMovement.gridSize;
    private float slashTeleBackWaitTime = 0.45f;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private bool isAttacking = false;
    private float playerDirection = 0;
    private Vector2 input;
    private Vector3 startPosition;
    public static int playerHealth = 9;
    public static int playerDMG = 1;
    public static int currentScore = 0;
    public static bool aliveState = true;
    

    void Start () {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive())
        {
            if (!isAttacking)
            {
                playerDirection = animator.GetFloat("direction");
                if (Input.GetButtonDown("Fire1") && playerDirection != 0)
                {
                    input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    startPosition = transform.position;
                    boxCollider2D.enabled = false;
                    RaycastHit2D hit = Physics2D.Raycast(startPosition, input, gridSize);
                    if (isWithinHittingRange(hit))
                    {
                        transform.position = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
                        Invoke("returnToOriginalPlace", slashTeleBackWaitTime);
                        isAttacking = true;
                        animateChar();
                    }
                    else
                    {
                        isAttacking = true;
                        Invoke("returnToOriginalPlace", slashTeleBackWaitTime);
                        animateChar();
                    }
                }
            }
        }
        else
        {
            //Calls levelEnd function in PlayerMovement  
        }
    }

    private void returnToOriginalPlace()
    {
        transform.position = startPosition;
        boxCollider2D.enabled = true;
        isAttacking = false;
    }

    private bool isWithinHittingRange(RaycastHit2D hit)
    {
        if (hit.collider == null)
            return false;
        else if (hit.collider.tag == "Enemy")
        {
            Debug.Log("Attacks Enemy");
            BlobController blob = hit.collider.gameObject.GetComponent<BlobController>();
            blob.blobHealth -= playerDMG;
            Debug.Log(blob.blobHealth);
            return true;
        }
        else
            return false;
    }

    private bool isAlive()
    {
        if (playerHealth <= 0)
        {
            aliveState = false;
            return false;
        }
        else
        {
            aliveState = true;
            return true;
        }
    }

    private void animateChar()
    {
        if (isAttacking)
            animator.SetTrigger("isAttacking");
        else
            print("No Animation to play");
    }

    public static void addPointsToCurrentScore(int points)
    {
        currentScore += points;
    }
}
