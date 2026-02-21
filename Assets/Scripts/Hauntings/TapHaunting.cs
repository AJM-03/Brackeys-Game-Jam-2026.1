using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHaunting : HauntedObject
{
    [SerializeField] private ParticleSystem particles;

    public override void HauntingEvent()
    {
        base.HauntingEvent();

        particles.Play();

        StartCoroutine(StaticDelay());
    }

    protected override void HauntingEnded()
    {
        particles.Stop();

        base.HauntingEnded();
    }

    private IEnumerator StaticDelay()
    {
        yield return new WaitForSeconds(hauntingDuration);
        HauntingEnded();
    }
}
