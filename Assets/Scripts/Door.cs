using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable
{
    [SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openRotation;
    [SerializeField] private float openTime = 1.5f;
    [SerializeField] private Ease openEase = Ease.InOutSine;
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private UnityEvent openEvent;
    [SerializeField] private UnityEvent closeEvent;
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

    public void OpenDoor(bool overrideLock = false)
    {
        if (doorLocked && !overrideLock) return;

        Transform cam = Camera.main.transform;
        Vector3 lookingAngle = (transform.position - cam.position).normalized; 

        Debug.Log(lookingAngle);

        Vector3 rotation = openRotation;
        if (lookingAngle.x < 0) rotation = -rotation;


        rotationPoint.DOLocalRotate(rotation, openTime).SetEase(openEase);
        open = true;
        openEvent.Invoke();

        if (this == GameManager.Instance.frontDoor) return;

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
        closeEvent.Invoke();
        if (GameManager.Instance.openDoor != null && GameManager.Instance.openDoor == this)
            GameManager.Instance.openDoor = null;
    }
}
