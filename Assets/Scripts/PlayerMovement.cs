using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public bool lockMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // quit if movement is locked
        if(lockMovement) return;
        
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        // Debug.Log(change);
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove(){
        if (change != Vector3.zero) {
            MoveCharacter();
            animator.SetFloat("movex", change.x);
            animator.SetFloat("movey", change.y);
            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        Vector3 movePosition = transform.position + change * speed * Time.fixedDeltaTime;
        myRigidbody.MovePosition(movePosition);
    }


}
