using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 64);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * 64);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * 64);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * 64);
        }
    }
}
