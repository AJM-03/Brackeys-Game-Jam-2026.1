using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHaunting : HauntedObject
{
    [SerializeField] private float bounceHeight = 1f;
    [SerializeField] private List<Transform> moveSpots = new List<Transform>();

    public override void HauntingEvent()
    {
        base.HauntingEvent();

        Transform spot = moveSpots[Random.Range(0, moveSpots.Count)];
        float highest = transform.position.y;
        if (spot.position.y > highest) highest = spot.position.y;

        Vector3 halfway = new Vector3(transform.position.x + (spot.position.x - transform.position.x) / 2,
                                      highest + bounceHeight,
                                      transform.position.z + (spot.position.z - transform.position.z) / 2);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(halfway.y, hauntingDuration / 2).SetEase(Ease.OutSine));
        sequence.Join(transform.DOMoveX(halfway.x, hauntingDuration / 2).SetEase(Ease.InSine));
        sequence.Join(transform.DOMoveZ(halfway.z, hauntingDuration / 2).SetEase(Ease.InSine));

        sequence.Append(transform.DOMoveY(spot.position.y, hauntingDuration / 2).SetEase(Ease.InSine));
        sequence.Join(transform.DOMoveX(spot.position.x, hauntingDuration / 2).SetEase(Ease.OutSine));
        sequence.Join(transform.DOMoveZ(spot.position.z, hauntingDuration / 2).SetEase(Ease.OutSine));
        sequence.OnComplete(HauntingEnded);
        sequence.Play();
    }
}
