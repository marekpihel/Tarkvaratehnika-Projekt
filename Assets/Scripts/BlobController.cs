using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class BlobController : MonoBehaviour
{
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private bool isMoving = false;
    private Vector2 input;
    public int blobHealth = 3;                      
    public int blobAttackDMG = 1;                
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
        if (isDead()) {
            PlayerAttacking.addPointsToCurrentScore(100);
            Destroy(this);
        }
        else
        {

        }
    }

    private bool isDead()
    {
        if (blobHealth <= 0) {
            return true;
        }
        return false;
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


    private void animateCharMovement()
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
}
