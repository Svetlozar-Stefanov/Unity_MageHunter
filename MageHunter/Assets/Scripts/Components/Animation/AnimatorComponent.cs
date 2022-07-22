using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void IsMoving(bool isMoving)
    {
        animator.SetBool("IsMoving", isMoving);
    }

    public void IsJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void IsFalling(bool isFalling)
    {
        animator.SetBool("IsFalling", isFalling);
    }
}
