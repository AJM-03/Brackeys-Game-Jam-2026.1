using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int objectsCaptured;
    [HideInInspector] public Door openDoor;

    void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
