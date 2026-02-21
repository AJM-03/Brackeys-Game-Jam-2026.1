using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerHaunting : HauntedObject
{
    [SerializeField] private Light lightbulb;

    public override void HauntingEvent()
    {
        base.HauntingEvent();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(lightbulb.DOIntensity(0, 0));
        sequence.AppendInterval(0.05f);
        sequence.Append(lightbulb.DOIntensity(1, 0));
        sequence.AppendInterval(0.15f);

        sequence.SetLoops(3);
        sequence.OnComplete(HauntingEnded);
        sequence.Play();
    }
}
