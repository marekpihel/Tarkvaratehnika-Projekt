using UnityEngine;
using System.Collections;

public class BlobAttack : MonoBehaviour {
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private bool isAttacking = false;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool isWithinHittingRange(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            return true;
        }
        else if (hit.collider.tag == "Player")
        {
            Debug.Log("Is Attacking Player");

            return false;
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
