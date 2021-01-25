using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    private int index;

    private void Start()
    {
        index = 0;

        LoadTutorial();

        InitialGraphics();

        panels[index].SetActive(true);

    }

    private void InitialGraphics()
    {
        foreach(GameObject g in panels)
        {
            g.SetActive(false);
        }
    }

    private void LoadTutorial()
    {
        if (PlayerPrefs.HasKey("showHtp"))
        {
            ClosedTutorial();
            
        } else
        {
            OpenTutorial();
            
        }
    }

    public void ClosedTutorial()
    {
        GameManager.Instance.OpenGameCanvas();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("showHtp", 1);
    }

    private void OpenTutorial()
    {
        gameObject.SetActive(true);
        
        Time.timeScale = 0f;
    }

    public void NextPanel()
    {
        InitialGraphics();

        if(index < panels.Length - 1)
        {
            index++;
            panels[index].SetActive(true);
        } else
        {
            ClosedTutorial();
        }
    }
}

