using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIControl : MonoBehaviour {

    GameObject[] endObjects;
    GameObject[] pausedObjects;
    GameObject pauseButton;
    GameObject keyIcon;

    bool theEnd = false;

	// Use this for initialization
	void Start () {
        pausedObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
        pauseButton = GameObject.Find("pauseButton");
        keyIcon = GameObject.Find("KeyIcon");

        EndHide();
        PauseHide();
        KeyHide();

    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.instance.hasKey)
        {
            KeyShow();
        }

	}

    public void PauseOn()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            print("Paused");
            PauseShow();
        }
    }
    public void PauseOff()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseHide();
        }
    }

    public void PauseShow()
    {
        foreach (GameObject j in this.pausedObjects)
        {
            j.SetActive(true);
        }
    }

    public void PauseHide()
    {
        Debug.Log(this.pausedObjects.Length);
        foreach (GameObject j in this.pausedObjects)
        {
            j.SetActive(false);
        }
        // Debug.Log(this.pausedObjects.Length);
    }


    public void EndShow()
    {
        foreach (GameObject j in endObjects)
        {
            j.SetActive(true);
        }
        pauseButton.SetActive(false);
    }
    public void EndHide()
    {
        foreach (GameObject j in endObjects)
        {
            j.SetActive(false);
        }
    }

    public void KeyShow()
    {
        keyIcon.SetActive(true);
    }
    public void KeyHide()
    {
        keyIcon.SetActive(false);
    }


    public void LoadLevel(string lvl)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseHide();
        }
        SceneManager.LoadSceneAsync(lvl);
    }

}
