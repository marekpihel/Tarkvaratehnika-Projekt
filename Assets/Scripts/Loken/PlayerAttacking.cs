using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour {
    private float rayCastRange = 64f;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private bool isAttacking = false;
    private int playerDirection = 0;
    private Vector2 input;
    public static int playerHealth = 9;
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
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            decidePlayerDirection();
            if (Input.GetButton("Fire1") && playerDirection != 0)
            {
                isAttacking = true;
                animateChar();
                isAttacking = false;
            }
        }
        else
        {
            //Calls function in PlayerMovement  
        }
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
        {
            animator.SetBool("isAttacking", true);
            animator.SetFloat("direction", playerDirection);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
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
        else
            playerDirection = 0;
    }

    public static void addPointsToCurrentScore(int points)
    {
        currentScore += points;
    }
}
