using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Blur : MonoBehaviour
{

    [SerializeField] private bool startBlured = true;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameManager.instance.notificationCenter.OnBlur += HandleBlurAnimation;

        HandleBlurAnimation(startBlured);
    }

    private void HandleBlurAnimation(bool _blur)
    {
        animator.SetBool("Blur", _blur);
    }

    private void OnDestroy()
    {
        GameManager.instance.notificationCenter.OnBlur -= HandleBlurAnimation;
    }

}
