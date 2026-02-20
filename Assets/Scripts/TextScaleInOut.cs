using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScaleInOut : MonoBehaviour
{
    public float scaleSize = 1.25f;
    public float scaleSpeed = 0.5f;
    public Ease scaleEase = Ease.InOutSine;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        Transform text = GetComponent<Transform>();
        sequence.Append(text.DOScale(scaleSize, scaleSpeed).SetEase(scaleEase));
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.Play();
    }
}
