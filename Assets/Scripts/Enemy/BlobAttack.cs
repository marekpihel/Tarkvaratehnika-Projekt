using UnityEngine;
using System.Collections;

public class BlobAttack : MonoBehaviour {
    private bool isAttacking = false;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Vector3 startPosition;
    private float teleBackWaitTime = 0.5f;
    private float cooldown;
    private BlobMovement blobMovement;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        blobMovement = GetComponent<BlobMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (cooldown <= 0)
        {
            cooldown = 3;
            startPosition = transform.position;
            boxCollider2D.enabled = false;
            RaycastHit2D hitEast = Physics2D.Raycast(this.transform.position, new Vector2(1, 0), PlayerMovement.gridSize * 2);
            RaycastHit2D hitWest = Physics2D.Raycast(this.transform.position, new Vector2(-1, 0), PlayerMovement.gridSize * 2);
            RaycastHit2D hitNorth = Physics2D.Raycast(this.transform.position, new Vector2(0, 1), PlayerMovement.gridSize * 2);
            RaycastHit2D hitSouth = Physics2D.Raycast(this.transform.position, new Vector2(0, -1), PlayerMovement.gridSize * 2);
            if (isWithinHittingRange(hitEast))
            {
                transform.position = new Vector3(startPosition.x + 1 * PlayerMovement.gridSize, startPosition.y + 0 * PlayerMovement.gridSize, startPosition.z);
                Invoke("returnToOriginalPlace", teleBackWaitTime);
                isAttacking = true;
                animateChar();
            }
            else if (isWithinHittingRange(hitWest))
            {
                transform.position = new Vector3(startPosition.x + -1 * PlayerMovement.gridSize, startPosition.y + 0 * PlayerMovement.gridSize, startPosition.z);
                Invoke("returnToOriginalPlace", teleBackWaitTime);
                isAttacking = true;
                animateChar();
            }
            else if (isWithinHittingRange(hitNorth))
            {
                transform.position = new Vector3(startPosition.x + 0 * PlayerMovement.gridSize, startPosition.y + 1 * PlayerMovement.gridSize, startPosition.z);
                Invoke("returnToOriginalPlace", teleBackWaitTime);
                isAttacking = true;
                animateChar();
            }
            else if (isWithinHittingRange(hitSouth))
            {
                transform.position = new Vector3(startPosition.x + 0 * PlayerMovement.gridSize, startPosition.y + -1 * PlayerMovement.gridSize, startPosition.z);
                Invoke("returnToOriginalPlace", teleBackWaitTime);
                isAttacking = true;
                animateChar();
            }
            else
            {
                Invoke("setIsAttackFalse", teleBackWaitTime);
                isAttacking = true;
                animateChar();
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void setIsAttackFalse()
    {
        boxCollider2D.enabled = true;
    }

    private void returnToOriginalPlace()
    {
        transform.position = startPosition;
        boxCollider2D.enabled = true;
    }

    private bool isWithinHittingRange(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            return false;
        }
        else if (hit.collider.tag == "Player")
        {
            blobMovement.disableMovement();
            Debug.Log("Attacks Player");
            PlayerAttacking.playerHealth -= this.GetComponent<Blob>().blobAttackDMG;
            Debug.Log(PlayerAttacking.playerHealth);
            return true;
        }
        else
        {
            return false;
        }
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
