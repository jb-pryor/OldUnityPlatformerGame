using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    private Animator animator;
    private bool isBlocking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleBlockInput();
    }

    void HandleBlockInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isBlocking = true;
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            isBlocking = false;
            animator.SetBool("IdleBlock", false);
        }
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }
}
