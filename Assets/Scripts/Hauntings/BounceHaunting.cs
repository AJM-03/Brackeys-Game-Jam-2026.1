using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHaunting : HauntedObject
{
    [SerializeField] private float bounceHeight = 1f;
    [SerializeField] private int bounces = 1;

    public override void HauntingEvent()
    {
        base.HauntingEvent();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + bounceHeight, hauntingDuration / 2).SetEase(Ease.OutSine));
        sequence.Append(transform.DOMoveY(transform.position.y, hauntingDuration / 2).SetEase(Ease.InSine));
        sequence.SetLoops(bounces);
        sequence.OnComplete(HauntingEnded);
        sequence.Play();
    }
}
