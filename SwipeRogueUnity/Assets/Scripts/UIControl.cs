using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIControl : MonoBehaviour {

    GameObject[] endObjects;
    GameObject[] pausedObjects;
    GameObject pauseButton;
    bool theEnd = false;

	// Use this for initialization
	void Start () {
        pausedObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
        pauseButton = GameObject.Find("pauseButton");

        EndHide();
        PauseHide();


    }
	
	// Update is called once per frame
	//void Update () {
	//	
	//}

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


    public void EndOn()
    {
    
    }
    
    public void EndOff()
    {
    
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

    public void LoadLevel(string lvl)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseHide();
        }
        StatManager.instance.DestroySelf();
        Debug.Log("Destroyed StatManager");
        SceneManager.LoadSceneAsync(lvl);
    }

}
