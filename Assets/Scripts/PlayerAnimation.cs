using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationWithVector(Vector3 dir)
    {
        animator.SetInteger("Horizontal", int.Parse(dir.x.ToString()));
        animator.SetInteger("Vertical", int.Parse(dir.y.ToString()));
    }
}
