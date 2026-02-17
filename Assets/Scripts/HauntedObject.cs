using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HauntedObject : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenHaunting = 5f;
    [SerializeField] private float maxTimeBetweenHaunting = 20f;
    [SerializeField] private int capturesRequired = 0;
    private float hauntingTimer;

    [SerializeField] private UnityEvent hauntingEvent;
    [SerializeField] private float hauntingDuration = 0.5f;
    [SerializeField] private float hauntingStrength = 1f;
    [SerializeField] private Ease hauntingEase = Ease.InOutSine;

    private bool hauntingCaptured = false;
    private bool onCamera = false;
    private bool hauntingHappening = false;
    [SerializeField] private float captureTime = 0.2f;
    private float captureTimer = 0;


    void Awake()
    {
        ResetHauntingTimer();

        if (captureTime > hauntingDuration) 
            captureTime = hauntingDuration / 1.5f;
    }

    void Update()
    {
        if (GameManager.Instance.objectsCaptured >= capturesRequired)
        {
            hauntingTimer -= Time.deltaTime;
            if (hauntingTimer <= 0)
            {
                hauntingHappening = true;
                captureTimer = 0;
                hauntingEvent.Invoke();
                ResetHauntingTimer();
            }
        }

        if (hauntingHappening && onCamera && !hauntingCaptured)
        {
            captureTimer += Time.deltaTime;
        }
    }

    private void ResetHauntingTimer()
    {
        hauntingTimer = Random.Range(minTimeBetweenHaunting, maxTimeBetweenHaunting);
    }

    private void HauntingEnded()
    {
        hauntingHappening = false;

        if (captureTimer >= captureTime && !hauntingCaptured)
        {
            hauntingCaptured = true;
            GameManager.Instance.objectsCaptured ++;
            transform.DOShakeScale(hauntingDuration, hauntingStrength).SetEase(hauntingEase);
        }

        captureTimer = 0;
    }

    public void ShakeHaunting()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + hauntingStrength, hauntingDuration / 2).SetEase(Ease.OutSine));
        sequence.Append(transform.DOMoveY(transform.position.y, hauntingDuration / 2).SetEase(Ease.InSine).OnComplete(HauntingEnded));

        sequence.Play();
    }

    public void ShakeHaunting1()
    {
        transform.DOShakePosition(hauntingDuration, hauntingStrength).SetEase(hauntingEase).OnComplete(HauntingEnded);
    }
}
