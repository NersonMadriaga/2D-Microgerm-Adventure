using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
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