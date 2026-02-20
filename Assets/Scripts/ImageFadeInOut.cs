using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    public Ease fadeEase = Ease.InOutSine;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        Image image = GetComponent<Image>();
        sequence.Append(image.DOFade(0, fadeSpeed).SetEase(fadeEase));
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.Play();
    }
}
