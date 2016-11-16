using UnityEngine;
using System.Collections;
using System;

public class BlobMovement : MonoBehaviour {
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private bool isMoving = false;
    private float cooldown;
    private int direction;
    private Animator animator;
    private WaitForSeconds waitforseconds;
    private bool haventMoved;
    private BoxCollider2D boxCollider2D;
    private bool movementStatus;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        waitforseconds = new WaitForSeconds(1);
        haventMoved = true;
        boxCollider2D = GetComponent<BoxCollider2D>();
        movementStatus = true;
    }

    // Update is called once per frame
    void Update() {
        if (cooldown <= 0 ) {
            direction = (int)Mathf.Floor(UnityEngine.Random.Range(0, 5));
            haventMoved = true;
            cooldown = 3;
        } else {
            cooldown -= Time.deltaTime;
        }
        if (isMovementDisabled()) {
            if (haventMoved) {
                if (direction == 1)
                {
                    if (isAllowedToMove(new Vector2(0, 1)))
                    {
                        moveUp();
                    }
                }
                else if (direction == 2)
                {
                    if (isAllowedToMove(new Vector2(1, 0)))
                    {
                        moveRight();
                    }
                }
                else if (direction == 3)
                {
                    if (isAllowedToMove(new Vector2(0, -1)))
                    {
                        moveDown();
                    }
                }
                else if (direction == 4)
                {
                    if (isAllowedToMove(new Vector2(-1, 0)))
                    {
                        moveLeft();
                    }
                }
                Invoke("stayStill", 1);
                haventMoved = false;
            }
        }
         
    }

    private bool isMovementDisabled()
    {
        return movementStatus;
    }

    public void disableMovement() {
        movementStatus = false;
    }

    private void stayStill()
    {
        animator.SetBool("isWalking", false);
        animator.SetFloat("value_x", 0);
        animator.SetFloat("value_y", 0);       
    }

    private void moveLeft()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", -1);
        animator.SetFloat("value_y", 0);
        move(new Vector2(-1, 0));
    }

    private void moveDown()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", 0);
        animator.SetFloat("value_y", -1);
        move(new Vector2(0, -1));
    }

    private void moveRight()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", 1);
        animator.SetFloat("value_y", 0);
        move(new Vector2(1, 0));
    }

    private void moveUp()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", 0);
        animator.SetFloat("value_y", 1);
        move(new Vector2(0, 1));
    }

    private void move(Vector2 direction) {
        float time = 0;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = new Vector2(startPosition.x + direction.x * gridSize, startPosition.y + direction.y * gridSize);
        while (time < 1f)
        {
            time += Time.deltaTime * (moveSpeed / gridSize);
            transform.position = Vector2.Lerp(startPosition, endPosition, time);
        }
        transform.position = endPosition;
    }

    private bool isAllowedToMove(Vector2 input)
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = new Vector3(startPosition.x, startPosition.y);
        boxCollider2D.enabled = false;
        RaycastHit2D hit128 = Physics2D.Raycast(startPosition, input, gridSize * 2);
        boxCollider2D.enabled = true;
        if (hit128.collider != null && hit128.collider.name == "Player")
        {
            return false;
        }
        else
        {
            boxCollider2D.enabled = false;
            RaycastHit2D hit64 = Physics2D.Raycast(startPosition, input, gridSize+13);
            boxCollider2D.enabled = true;
            if (hit64.collider == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
