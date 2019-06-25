using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorEventHelper : MonoBehaviour
{
    public delegate void KnifeAnimationEventTrigger();
    public delegate void LoadMenuTrigger();


    public event KnifeAnimationEventTrigger OnKnifeAnimationEvent;
    public event LoadMenuTrigger OnLoadMenu;
    public void HandleKnifeAnimationEvent()
    {
        OnKnifeAnimationEvent?.Invoke();
    }

    public void HandleLoadMenuEvent()
    {
        OnLoadMenu?.Invoke();
    }

}