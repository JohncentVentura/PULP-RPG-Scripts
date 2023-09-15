using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator animator;
    [SerializeField] NPCDirTrigger rightDirTrigger;
    [SerializeField] NPCDirTrigger leftDirTrigger;
    [SerializeField] private string blendTreeName;
    [SerializeField] private float startDirection;
    [SerializeField] private bool isDirectionOne;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Direction", startDirection);
    }

    void Update()
    {
        animator.Play(blendTreeName);

        if (!isDirectionOne)
        {
            if (leftDirTrigger.isPlayerDetected)
            {
                animator.SetFloat("Direction", 0f);
            }
            else if (rightDirTrigger.isPlayerDetected)
            {
                animator.SetFloat("Direction", 1f);
            }
        }

    }
}
