using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    static public int currentLevel;

    private GameObject pauseCanvas, optionsCanvas, gameCanvas,
        gameOverCanvas, inventoryCanvas, player, questionAnswerCanvas, confirmationCanvas;

    private bool isGameOver, isQuiz;

    public bool IsGameOver { set { isGameOver = value; } }
    public int CurrentLevel { get { return currentLevel;  } set { currentLevel = value; } }



    private void Awake()
    {
        Instance = this;

        isGameOver = false;
        isQuiz = false;
        
    }
    private void Start()
    {
        Load();
        
        BackgroundManager.Instance.ChangeBackground(currentLevel);

        InitialProperties();
        InitialPlayerPosition();
        InitialGraphics();
        InitialHealth();
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {

    }

    private void InitialPlayerPosition()
    {

        Debug.Log("CurrentLevelInitialPlayer " + currentLevel);
        player.SetActive(true);
        switch (currentLevel)
        {
            case 1:
                player.transform.position = new Vector2(-15f, 1.7f);
                //player.transform.position = new Vector2(137f, 1.7f);
                break;
            case 2:
                player.transform.position = new Vector2(-15f, -50f);
                break;
            case 3:
                player.transform.position = new Vector2(-15f, -110f);
                break;
        }


    }

    private void InitialProperties()
    {
        player = GameObject.Find("Player");
        pauseCanvas = GameObject.Find("PauseCanvas");
        optionsCanvas = GameObject.Find("OptionsCanvas");
        gameCanvas = GameObject.Find("GameCanvas");
        gameOverCanvas = GameObject.Find("GameOverCanvas");
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        questionAnswerCanvas = GameObject.Find("QuestionAnswerCanvas");
    }

    private void InitialGraphics()
    {
        pauseCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
        confirmationCanvas.SetActive(false);
        questionAnswerCanvas.SetActive(false);
    }

    private void InitialHealth()
    {
        player.GetComponent<PlayerStatus>().CurrentHealth = 100;
    }

    public void RegenHealth()
    {
        player.GetComponent<PlayerStatus>().CurrentHealth += 30;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Level", currentLevel);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            
            currentLevel = PlayerPrefs.GetInt("Level");
            Debug.Log("Loaded CurrentLevel " + currentLevel);

        } else
        {
            currentLevel = 1;
        }
        
    }

    public void GameOver()
    {
        InitialGraphics();
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    public void IncreaseLevel()
    {
        currentLevel++;
        //LevelManager.Instance.Level = currentLevel;
        //InitialPlayerPosition();
        //BackgroundManager.Instance.ChangeBackground(currentLevel);

        Restart();
    }

    public void PlayAgain()
    {
        InitialGraphics();
        Restart();
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OpenOptions()
    {
        
        optionsCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("TitleScene");
    }

    public void OpenInventory()
    {
        inventoryCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ClosedInventory()
    {
        inventoryCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnClickInventoryButton()
    {
        inventoryCanvas.gameObject.SetActive(false);
    }

    public void ClosedGameCanvas()
    {
        gameCanvas.SetActive(false);
    }

    public void OpenGameCanvas()
    {
        gameCanvas.SetActive(true);
    }


    public void ClosedOptions()
    {
        
        optionsCanvas.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Save();
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void BackgroundMusic(GameObject btn)
    {
        TextMeshProUGUI txt = btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (txt.text.Equals("BGM ON"))
        {
            txt.text = "BGM OFF";
        } else
        {
            txt.text = "BGM ON";
        }
    }

    public void YesQuiz()
    {
        // close confirmation 
        // open question answer canvas
        // start the quiz
        isQuiz = true;
        InitialGraphics();
        gameCanvas.SetActive(false);
        questionAnswerCanvas.SetActive(true);
        QuizManager.instance.DisplayInterface();
    }

    public void NoQuiz()
    {
        // close confirmation
        // back to the game
        ClosedConfirmation();

    }

    public void ClosedQuestionAnswerCanvas()
    {
        questionAnswerCanvas.SetActive(false);
    }

    public void ClosedConfirmation()
    {
        confirmationCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OpenConfirmation()
    {
        confirmationCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void SoundEffects(GameObject btn)
    {
        TextMeshProUGUI txt = btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (txt.text.Equals("SFX ON"))
        {
            txt.text = "SFX OFF";
        }
        else
        {
            txt.text = "SFX ON";
        }
    }
}
