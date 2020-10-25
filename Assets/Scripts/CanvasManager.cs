﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public Image MainMenuImage;
    private int progression = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMainMenuClick()
    {
        if(progression == 0)
        {
            
            MainMenuImage.GetComponent<Animator>().SetBool("clicked", true);
            progression++;
        }
        else if(progression == 1)
        {
            MainMenuImage.GetComponent<Animator>().SetBool("clicked1", true);
        }
        
        //MainMenuImage.GetComponent<Animator>().SetBool("clicked", false);
    }

    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }


}
