using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHaunting : HauntedObject
{
    [SerializeField] private MeshRenderer tv;
    [SerializeField] private Material staticMat;
    private Material oldMat;

    public override void HauntingEvent()
    {
        base.HauntingEvent();

        var TempArray = tv.materials;

        oldMat = TempArray[1];
        TempArray[1] = staticMat;

        tv.materials = TempArray;
        StartCoroutine(StaticDelay());
    }

    protected override void HauntingEnded()
    {
        var TempArray = tv.materials;

        TempArray[1] = oldMat;

        tv.materials = TempArray;

        base.HauntingEnded();
    }

    private IEnumerator StaticDelay()
    {
        yield return new WaitForSeconds(hauntingDuration);
        HauntingEnded();
    }
}
