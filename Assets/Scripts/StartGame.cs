using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private bool starting = false; 
    [SerializeField] private Image screenFade;
    [SerializeField] private float screenFadeSpeed = 0.25f;
    private string nextSceneName;

    public void ClickStart(string sceneName)
    {
        if (starting) return;
        starting = true;
        nextSceneName = sceneName;

        screenFade.DOFade(1, screenFadeSpeed / 2).SetEase(Ease.InSine).OnComplete(LoadNext);

    }

    private void LoadNext()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
