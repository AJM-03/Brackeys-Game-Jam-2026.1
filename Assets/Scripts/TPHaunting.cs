using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPHaunting : HauntedObject
{
    [SerializeField] Transform objToScale;
    [SerializeField] private float maxLength = 1f;
    private float startingLength;

    public override void HauntingEvent()
    {
        base.HauntingEvent();
        startingLength = objToScale.localScale.y;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(objToScale.DOScaleY(maxLength, hauntingDuration / 2).SetEase(Ease.InOutSine));
        sequence.Append(objToScale.DOScaleY(startingLength, hauntingDuration / 2).SetEase(Ease.InOutSine));

        sequence.Append(objToScale.DOScaleY(maxLength / 1.5f, hauntingDuration / 2).SetEase(Ease.InOutSine));
        sequence.Append(objToScale.DOScaleY(startingLength, hauntingDuration / 2).SetEase(Ease.InOutSine));

        sequence.Append(objToScale.DOScaleY(maxLength, hauntingDuration / 2).SetEase(Ease.InOutSine));
        sequence.Append(objToScale.DOScaleY(startingLength, hauntingDuration / 2).SetEase(Ease.InOutSine));

        sequence.OnComplete(HauntingEnded);
        sequence.Play();
    }
}
