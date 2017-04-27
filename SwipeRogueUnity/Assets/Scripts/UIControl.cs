using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIControl : MonoBehaviour {

    GameObject[] endObjects;
    GameObject[] pausedObjects;
    GameObject gameTimerFill;
    GameObject pauseTimerFill;
    GameObject keyIcon;
    GameObject Score;
    GameObject endRestart;

    bool theEnd = false;

	// Use this for initialization
	void Start () {
        pausedObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
        gameTimerFill = GameObject.Find("gameTimerFill");
        pauseTimerFill = GameObject.Find("pauseTimerFill");
        keyIcon = GameObject.Find("KeyIcon");
        Score = GameObject.Find("ScoreCount");
        endRestart = GameObject.Find("endRestart");

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
        pauseTimerFill.SetActive(true);
        gameTimerFill.SetActive(false);
    }

    public void PauseHide()
    {
        Debug.Log(this.pausedObjects.Length);
        foreach (GameObject j in this.pausedObjects)
        {
            j.SetActive(false);
        }

        gameTimerFill.SetActive(true);
        pauseTimerFill.SetActive(false);
    }


    public void EndShow()
    {
        foreach (GameObject j in endObjects)
        {
            j.SetActive(true);
        }
        pauseTimerFill.SetActive(false);
        gameTimerFill.SetActive(false);
        KeyHide();
        Score.transform.position = endRestart.transform.position + new Vector3(0, 250, 0);
    }
    public void EndHide()
    {
        foreach (GameObject j in endObjects)
        {
            j.SetActive(false);
        }
        //Score.transform.position = new Vector3(-396,225,-1);
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
        if (StatManager.instance != null) {
            StatManager.instance.DestroySelf();
            Debug.Log("Destroyed StatManager");
        }
        SceneManager.LoadSceneAsync(lvl);
    }

}
