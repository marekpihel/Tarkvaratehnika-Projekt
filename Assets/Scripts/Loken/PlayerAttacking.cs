﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAttacking : MonoBehaviour
{
    private float gridSize = PlayerMovement.gridSize;
    private float slashTeleBackWaitTime = 0.45f;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private bool isAttacking = false;
    private float playerDirection = 0;
    private Vector2 input;
    private Vector3 startPosition;
    private InGameUI inGameUi;


    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        inGameUi = GameObject.FindGameObjectWithTag("Map").GetComponent<InGameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inGameUi.getIsPaused()) {
            if (!isAttacking)
            {
                playerDirection = animator.GetFloat("direction");
                if (Input.GetButtonDown("Fire1") && playerDirection != 0)
                {
                    input = convertDirectionToVector();
                    startPosition = transform.position;
                    boxCollider2D.enabled = false;
                    RaycastHit2D hit = Physics2D.Raycast(startPosition, input, gridSize + 2);
                    if (isWithinHittingRange(hit))
                    {
                        transform.position = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
                        Invoke("returnToOriginalPlace", slashTeleBackWaitTime);
                        isAttacking = true;
                        animateChar();
                    }
                    else
                    {
                        Invoke("setIsAttackFalse", slashTeleBackWaitTime);
                        isAttacking = true;
                        animateChar();
                    }
                }
            }
        }
    }

    private void setIsAttackFalse()
    {
        boxCollider2D.enabled = true;
        isAttacking = false;
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
        {
            return false;
        }
        else if (hit.collider.tag == "Enemy")
        {
            Blob blob = hit.collider.gameObject.GetComponent<Blob>();
            blob.blobHealth -= Player.playerDMG;
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 convertDirectionToVector()
    {
        Vector2 vector = new Vector2();
        if (playerDirection == 1)
        {
            vector = new Vector2(0, 1);
        }
        else if (playerDirection == 2)
        {
            vector = new Vector2(1, 0);
        }
        else if (playerDirection == 3)
        { 
            vector = new Vector2(0, -1)
                ;
        }
        else if (playerDirection == 4)
        {
            vector = new Vector2(-1, 0);
        }
        return vector;
    }

    private void animateChar()
    {
        if (isAttacking)
        {
            animator.SetTrigger("isAttacking");
        }
        else
        {
            print("No Animation to play");
        }
    }
}
