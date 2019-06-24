using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorEventHelper : MonoBehaviour
{
    public delegate void AnimationEventTrigger();

    public event AnimationEventTrigger OnAnimationEventTrigger;

    public void HandleAnimationEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }

}