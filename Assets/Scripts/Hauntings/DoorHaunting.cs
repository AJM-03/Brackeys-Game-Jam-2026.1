using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHaunting : HauntedObject
{
    [SerializeField] private GameObject hallway;
    [SerializeField] private GameObject hauntedHallway;
    [SerializeField] private int timesDoorMustBeOpened;
    private int timesDoorHasBeenOpened;
    private bool hallwayDissapeared;

    public void DoorOpened()
    {
        timesDoorHasBeenOpened++;
        if (!hauntingCaptured && timesDoorHasBeenOpened >= timesDoorMustBeOpened)
        {
            hauntingCaptured = true;
            GameManager.Instance.EvidenceFound();
        }
    }

    public void LeftHallway()
    {
        if (hauntingCaptured && !hallwayDissapeared)
        {
            hallwayDissapeared = true;
            hallway.SetActive(true);
            hauntedHallway.SetActive(false);
        }
    }
}
