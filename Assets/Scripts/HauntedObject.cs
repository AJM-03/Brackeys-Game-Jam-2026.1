using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class HauntedObject : MonoBehaviour
{
    [SerializeField] private bool timedHaunting = true;
    [SerializeField] private float minTimeBetweenHaunting = 5f;
    [SerializeField] private float maxTimeBetweenHaunting = 20f;
    [SerializeField] private int capturesRequired = 0;
    private float hauntingTimer;
    [SerializeField] protected float hauntingDuration = 0.5f;
    [SerializeField] private bool hauntingHappensAfterCapture = false;

    protected bool hauntingCaptured = false;
    private bool onCamera = false;
    private bool hauntingHappening = false;
    [SerializeField] private float captureTime = 0.2f;
    [SerializeField][Range(0f, 1f)] private float captureAngle = 0.5f;
    [SerializeField] private float captureDistance = 5f;
    private float captureTimer = 0;


    void Awake()
    {
        ResetHauntingTimer();

        if (captureTime > hauntingDuration) 
            captureTime = hauntingDuration / 1.5f;
    }

    void Update()
    {
        if (!timedHaunting) return;
        if (GameManager.Instance.objectsCaptured >= capturesRequired &&
            (!hauntingCaptured || hauntingHappensAfterCapture))
        {
            hauntingTimer -= Time.deltaTime;
            if (hauntingTimer <= 0)
            {
                hauntingHappening = true;
                captureTimer = 0;
                HauntingEvent();
                ResetHauntingTimer();
            }
        }

        if (hauntingHappening && !hauntingCaptured && OnCamera())
        {
            captureTimer += Time.deltaTime;
        }
    }

    private void ResetHauntingTimer()
    {
        hauntingTimer = Random.Range(minTimeBetweenHaunting, maxTimeBetweenHaunting);
    }

    protected virtual void HauntingEnded()
    {
        hauntingHappening = false;

        if (captureTimer >= captureTime && !hauntingCaptured)
        {
            hauntingCaptured = true;
            GameManager.Instance.EvidenceFound();
            transform.DOShakeScale(hauntingDuration, 2f).SetEase(Ease.InOutSine);
        }

        captureTimer = 0;
    }

    private bool OnCamera()
    {
        onCamera = true;

        Transform cam = Camera.main.transform;
        Vector3 lookingAngle = (transform.position - cam.position);  // Angle between the object and the camera

        // Distance
        float distance = lookingAngle.magnitude;  // Distance between the object and camera
        //Debug.Log(distance);
        if (distance > captureDistance)
            onCamera = false;

        // Angle
        if (onCamera)
        {
            lookingAngle = lookingAngle / distance;  // Normalizes the direction
            float result = Vector3.Dot(cam.forward, lookingAngle);  // Gets the difference between this angle and camera's forwards direction
            //Debug.Log(result + "  -  " + lookingAngle);
            if (result < captureAngle)
                onCamera = false;
        }

        // Ray
        if (onCamera)
        {
            Ray ray = new Ray(cam.position, lookingAngle);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            //Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject != gameObject)
                onCamera = false;
        }

        return onCamera;
    }


    public virtual void HauntingEvent()
    {

    }
}
