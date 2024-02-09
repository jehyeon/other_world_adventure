using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class AnimationElement : MonoBehaviour
{
    [Header("Require")]
    [SerializeField]
    private TextMeshProUGUI animationName;

    private Animator animator;
    private AnimatorOverrideController over;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        // get animator
        animator = GetComponent<Animator>();

        // animation override
        over = new AnimatorOverrideController(
            animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = over;
    }

    public void SetName(string name)
    {
        animationName.text = name;
    }

    public void SetClip(AnimationClip clip)
    {
        //if (animator == null)
        //{
        //    Init();
        //}

        // animation 이름은 "attack"으로 고정
        over["attack"] = clip;
    }
}
