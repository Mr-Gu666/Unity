using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private bool IsMove;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontal);
        if (horizontal == 0)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }
    }
}
