using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int evidenceRequired;
    public int objectsCaptured;
    public Door frontDoor;
    [HideInInspector] public Door openDoor;
    [SerializeField] private GameObject cinemachineCam;


    [SerializeField] private Slider eBar;
    [SerializeField] private TMP_Text eBarText;
    [SerializeField] private float eBarFillSpeed = 0.15f;
    [SerializeField] private Ease eBarFillEase = Ease.InOutSine;

    [SerializeField] private Image recordingImage;
    [SerializeField] private Sprite recordingSprite;
    [SerializeField] private Sprite pausedSprite;

    [SerializeField] private GameObject pauseScreen;
    private bool gamePaused;

    [SerializeField] private Image screenFade;
    [SerializeField] private float screenFadeSpeed = 0.25f;
    [SerializeField] private string nextSceneName;

    void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);

        pauseScreen.GetComponent<PauseMenu>().Init();

        Color tempColor = screenFade.color;
        tempColor.a = 1;
        screenFade.color = tempColor;
        screenFade.DOFade(0, screenFadeSpeed).SetEase(Ease.OutSine);

        UpdateEvidence();
    }

    public void EvidenceFound()
    {
        objectsCaptured++;
        UpdateEvidence();
    }

    private void UpdateEvidence()
    {
        float e = Mathf.InverseLerp(0, evidenceRequired, objectsCaptured);
        e = Mathf.Lerp(0, 100, e);

        if (evidenceRequired == 0) e = 100;

        eBar.DOKill();
        eBar.DOValue(e, eBarFillSpeed).SetEase(eBarFillEase);

        if (e < 100)
            eBarText.text = "GATHER EVIDENCE";
        else
        {
            eBarText.transform.DOShakeScale(0.2f, 1.5f).SetEase(Ease.InOutSine);
            eBarText.text = "ENOUGH EVIDENCE\nLEAVE THE HOUSE";
            frontDoor.OpenDoor(true);
        }

        if (e < 30)
            MusicPlayer.Instance.ChangeSongIntensity(0);
        else if (e < 60)
            MusicPlayer.Instance.ChangeSongIntensity(1);
        else
            MusicPlayer.Instance.ChangeSongIntensity(2);
    }

    public void EndDay()
    {
        cinemachineCam.GetComponent<CinemachineInputAxisController>().enabled = false;
        screenFade.DOFade(1, screenFadeSpeed / 2).SetEase(Ease.InSine).OnComplete(LoadNext);
    }

    private void LoadNext()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void PauseGame()
    {
        if (gamePaused)
        {
            UnpauseGame();
            return;
        }
        gamePaused = true;
        pauseScreen.SetActive(true);
        pauseScreen.GetComponent<AudioSource>().PlayOneShot(pauseScreen.GetComponent<PauseMenu>().pauseSound);
        recordingImage.sprite = pausedSprite;
        recordingImage.GetComponent<ImageFadeInOut>().enabled = false;
        recordingImage.DOKill();
        Color tempColor = recordingImage.color;
        tempColor.a = 1;
        recordingImage.color = tempColor;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        gamePaused = false;
        pauseScreen.GetComponent<AudioSource>().PlayOneShot(pauseScreen.GetComponent<PauseMenu>().unpauseSound);
        recordingImage.sprite = recordingSprite;
        recordingImage.GetComponent<ImageFadeInOut>().enabled = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen.SetActive(false);
    }
}
