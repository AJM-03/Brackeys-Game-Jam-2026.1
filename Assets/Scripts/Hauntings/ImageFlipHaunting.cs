using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlipHaunting : HauntedObject
{
    [SerializeField] private Sprite sprite;
    private Sprite oldSprite;
    [SerializeField] private Image image;
    [SerializeField] private int timesToFlip;

    public override void HauntingEvent()
    {
        base.HauntingEvent();
        oldSprite = image.sprite;

        StartCoroutine(FlipImage());
    }

    protected override void HauntingEnded()
    {
        image.sprite = oldSprite;

        base.HauntingEnded();
    }

    private IEnumerator FlipImage()
    {
        for (int i = 0; i < timesToFlip; i++)
        {
            image.sprite = sprite;
            yield return new WaitForSeconds(hauntingDuration / (timesToFlip * 2));
            image.sprite = oldSprite;
            yield return new WaitForSeconds(hauntingDuration / (timesToFlip * 2));
        }

        HauntingEnded();
    }
}
