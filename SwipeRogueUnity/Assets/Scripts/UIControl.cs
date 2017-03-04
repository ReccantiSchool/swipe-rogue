using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIControl : MonoBehaviour {

    GameObject[] endObjects;
    GameObject[] pausedObjects;
    

	// Use this for initialization
	void Start () {
        pausedObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
        
        EndHide();
        //PauseHide();


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
        foreach (GameObject j in pausedObjects)
        {
            j.SetActive(true);
        }
    }

    public void PauseHide()
    {
        foreach (GameObject j in pausedObjects)
        {
            j.SetActive(false);
        }
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
        SceneManager.LoadSceneAsync(lvl);
    }

}
