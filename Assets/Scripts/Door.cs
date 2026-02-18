using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openRotation;
    [SerializeField] private float openTime = 1.5f;
    [SerializeField] private Ease openEase = Ease.InOutSine;
    [SerializeField] private Transform rotationPoint;
    public bool doorLocked;
    private bool open;

    public override void Interact()
    {
        base.Interact();

        if (open)
            CloseDoor();
        else
            OpenDoor();
    }

    public void OpenDoor()
    {
        if (doorLocked) return;

        Transform cam = Camera.main.transform;
        Vector3 lookingAngle = (transform.position - cam.position).normalized; 

        Debug.Log(lookingAngle);

        Vector3 rotation = openRotation;
        if (lookingAngle.x < 0) rotation = -rotation;


        rotationPoint.DOLocalRotate(rotation, openTime).SetEase(openEase);
        open = true;

        if (GameManager.Instance.openDoor != null && GameManager.Instance.openDoor != this)
            GameManager.Instance.openDoor.CloseDoor();
        GameManager.Instance.openDoor = this;
    }

    public void CloseDoor(bool animate = true)
    {
        if (animate)
            rotationPoint.DOLocalRotate(closedRotation, openTime).SetEase(openEase);
        else
            rotationPoint.localRotation = Quaternion.Euler(closedRotation);
        open = false;
        if (GameManager.Instance.openDoor != null && GameManager.Instance.openDoor == this)
            GameManager.Instance.openDoor = null;
    }
}
