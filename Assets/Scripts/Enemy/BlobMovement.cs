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

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        waitforseconds = new WaitForSeconds(1);
        haventMoved = true;
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
        if (haventMoved) {
            if (direction == 1)
            {
                moveUp();
            }
            else if (direction == 2)
            {
                moveRight();
            }
            else if (direction == 3)
            {
                moveDown();
            }
            else if (direction == 4)
            {
                moveLeft();
            }
            Invoke("stayStill", 1);
            haventMoved = false;
        }
         
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
    }

    private void moveDown()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", 0);
        animator.SetFloat("value_y", -1);
    }

    private void moveRight()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", 1);
        animator.SetFloat("value_y", 0);
    }

    private void moveUp()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("value_x", 0);
        animator.SetFloat("value_y", 1);
    }
}
