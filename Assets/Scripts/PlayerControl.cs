using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 128f;
    private float gridSize = 64f;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private Animator animator;
    private Rigidbody2D rigidBody2D;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(move(transform));
            }
        }
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        animator.SetBool("isWalking", true);
        animator.SetFloat("input_x", input.x);
        animator.SetFloat("input_y", input.y);
        startPosition = transform.position;
        t = 0;

        endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        animator.SetBool("isWalking", false);
        isMoving = false;
        yield return 0;
    }

    void OnTriggerEnter2D(Collider2D collisionObject) {
        if (collisionObject.name == "trapdoor") {
            Scoreboard.WriteScoreboard(SetName.getCharacterName(), (int)GameTime.getPlayedTime());
            SceneManager.LoadScene("Highscore");
        }
    }
}
