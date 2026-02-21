using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHaunting : HauntedObject
{
    [SerializeField] private float hauntingStrength = 1f;

    public override void HauntingEvent()
    {
        base.HauntingEvent();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + hauntingStrength, hauntingDuration / 2).SetEase(Ease.OutSine));
        sequence.Append(transform.DOMoveY(transform.position.y, hauntingDuration / 2).SetEase(Ease.InSine).OnComplete(HauntingEnded));

        sequence.Play();
    }
}
