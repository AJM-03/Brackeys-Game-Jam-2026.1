using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private GameObject cinemachineCam;

    [SerializeField] private Slider movementSlider;

    private void Start()
    {
        // Volume
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Sensitivity
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 15f);
        sensitivitySlider.value = savedSensitivity;
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        var axisController = cinemachineCam.GetComponent<CinemachineInputAxisController>();
        foreach (var c in axisController.Controllers)
        {
            if (c.Name == "Look X (Pan)")
            {
                c.Input.Gain = savedSensitivity;
            }

            if (c.Name == "Look Y (Tilt)")
            {
                c.Input.Gain = -savedSensitivity;
            }
        }

        // Movement
        float savedMovement = PlayerPrefs.GetFloat("Movement", 0.7f);
        movementSlider.value = savedMovement;
        movementSlider.onValueChanged.AddListener(SetMovement);
        var cinemachine = cinemachineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachine.AmplitudeGain = savedMovement;
    }

    public void Continue()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void Exit()
    {
        Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float sensitivity)
    {
        var axisController = cinemachineCam.GetComponent<CinemachineInputAxisController>();
        foreach (var c in axisController.Controllers)
        {
            if (c.Name == "Look X (Pan)")
            {
                c.Input.Gain = sensitivity;
            }

            if (c.Name == "Look Y (Tilt)")
            {
                c.Input.Gain = -sensitivity;
            }
        }
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void SetMovement(float movement)
    {
        var cinemachine = cinemachineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachine.AmplitudeGain = movement;
        PlayerPrefs.SetFloat("Movement", movement);
        PlayerPrefs.Save();
    }
}
