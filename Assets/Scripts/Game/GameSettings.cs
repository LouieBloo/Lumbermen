using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.AudioSettings;

public class GameSettings : MonoBehaviour
{
    public BasicUnitMovement playerMovement;
    public Joystick joystick;
    public static GameSettings Instance;

    public bool isMobile = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            isMobile = true;
            Application.targetFrameRate = 60;
        }
        else
        {
            playerMovement.joystick = null;
        }
    }

    public float getXDirection()
    {
        if (isMobile) { return joystick.Horizontal; }
        return Input.GetAxis("Horizontal");
    }

    public float getYDirection()
    {
        if (isMobile) { return joystick.Vertical; }
        return Input.GetAxis("Vertical");
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
