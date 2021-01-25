using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScene : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas, storyBoardCanvas, creditCanvas;
    [SerializeField] private Sprite[] sprites;

    private int index = 0;
    private void Start()
    {
        menuCanvas.SetActive(false);
        creditCanvas.SetActive(false);
        storyBoardCanvas.SetActive(true);
        storyBoardCanvas.transform.GetChild(0).GetComponent<Image>().sprite = sprites[index];
    }
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("GameScene");
    }

    public void OpenCredits()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        creditCanvas.SetActive(true);
    }

    public void ClosedCredits()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        creditCanvas.SetActive(false);
    }


    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void Next()
    {
        if(index < sprites.Length-1)
        {
            index++;
            storyBoardCanvas.transform.GetChild(0).GetComponent<Image>().sprite = sprites[index];
        } else
        {
            CloseStoryBoard();
        }
        
    }

    private void CloseStoryBoard()
    {
        storyBoardCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void Skip()
    {
        CloseStoryBoard();
    }
}

// GUI screenshots


// story board
// Title = Microgerm Adventure
// Restart should restart the current level
// Create Tutorial
// SoundManager
// Craete quiz
// Create inventory