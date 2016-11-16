using UnityEngine;
using System.Collections;

public class BlobMovement : MonoBehaviour {
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private bool isMoving = false;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Vector2 movementVector2;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool isAllowedToMove(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            return true;
        }
        else if (hit.collider.tag == "Player")
        {
            Debug.Log("Walks into Player");
            return false;
        }
        else
        {
            return false;
        }
    }

    private void animateCharMovement()
    {
        if (isMoving)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("value_x", movementVector2.x);
            animator.SetFloat("value_y", movementVector2.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
