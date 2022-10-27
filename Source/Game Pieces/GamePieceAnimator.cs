using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePieceAnimator : MonoBehaviour
{
    public Animator animator;


    // Change animation
    public virtual void ChangeAnimation(string animationName)
    {
        animator.Play(animationName);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
