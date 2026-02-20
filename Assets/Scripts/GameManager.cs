using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int evidenceRequired;
    public int objectsCaptured;
    [HideInInspector] public Door openDoor;
    [SerializeField] private Slider eBar;
    [SerializeField] private TMP_Text eBarText;
    [SerializeField] private float eBarFillSpeed = 0.15f;
    [SerializeField] private Ease eBarFillEase = Ease.InOutSine;

    void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);

        UpdateEvidence();
    }

    void Update()
    {
        
    }

    public void EvidenceFound()
    {
        objectsCaptured++;

        UpdateEvidence();
    }

    private void UpdateEvidence()
    {
        float e = Mathf.InverseLerp(0, evidenceRequired, objectsCaptured);
        Debug.Log(e+"1");
        e = Mathf.Lerp(0, 100, e);
        Debug.Log(e + "2");

        eBar.DOKill();
        eBar.DOValue(e, eBarFillSpeed).SetEase(eBarFillEase);

        if (e < 100)
            eBarText.text = "GATHER EVIDENCE";
        else
            eBarText.text = "ENOUGH EVIDENCE\nLEAVE THE HOUSE";
    }
}
