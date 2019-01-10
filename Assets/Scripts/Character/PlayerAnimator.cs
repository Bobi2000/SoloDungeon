using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator Animator;

    private Rigidbody Rb;
    
    private void Start()
    {
        this.Animator = GetComponentInChildren<Animator>();
        this.Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Check if character is walking
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            this.Animator.SetBool("isWalking", true);
        }
        else
        {
            this.Animator.SetBool("isWalking", false);
        }
        //Check if character is running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.Animator.SetBool("isRunning", true);
        }
        else
        {
            this.Animator.SetBool("isRunning", false);
        }
    }
}