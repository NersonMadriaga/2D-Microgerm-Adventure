using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    static public int currentLevel;
    private int quizRetake;
    [SerializeField] private GameObject pauseCanvas, optionsCanvas, gameCanvas,
        gameOverCanvas, inventoryCanvas, player, questionAnswerCanvas, confirmationCanvas, scrollCanvas;

    public int CurrentLevel { get { return currentLevel;  } set { currentLevel = value; } }

    [SerializeField] private TextMeshProUGUI stageText;

    private void Awake()
    {
        Instance = this;
        currentLevel = 1;
        quizRetake = 0;
    }
    private void Start()
    {
        Load();
        Debug.Log("current level" + currentLevel);
        BackgroundManager.Instance.ChangeBackground(currentLevel);

        InitialProperties();
        InitialPlayerPosition();
        InitialGraphics();
        InitialHealth();
        
    }

    private void InitialPlayerPosition()
    {

        player.SetActive(true);
        switch (currentLevel)
        {
            case 1:
                player.transform.position = new Vector2(-15f, 1.7f);
                //player.transform.position = new Vector2(137f, 1.7f);
                stageText.text = "STAGE : 1";
                break;
            case 2:
                player.transform.position = new Vector2(-15f, -50f);
                stageText.text = "STAGE : 2";
                break;
            case 3:
                player.transform.position = new Vector2(-15f, -110f);
                stageText.text = "STAGE : 3";
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
        scrollCanvas = GameObject.Find("ScrollCanvas");
    }

    private void InitialGraphics()
    {
        pauseCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
        confirmationCanvas.SetActive(false);
        questionAnswerCanvas.SetActive(false);
        scrollCanvas.SetActive(false);
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
            if(PlayerPrefs.GetInt("Level") >0 && PlayerPrefs.GetInt("Level") <= 3)
            {
                currentLevel = PlayerPrefs.GetInt("Level");
            } else
            {
                currentLevel = 1;
            }
            

        } else
        {
            currentLevel = 3;
        }
        
    }

    public void GetAllScrolls()
    {
        scrollCanvas.SetActive(true);
    }

    public void HideAllScrolls()
    {
        scrollCanvas.SetActive(false);
    }

    public void GameOver()
    {
        InitialGraphics();
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    public void IncreaseLevel()
    {
        FindObjectOfType<AudioManager>().Play("PauseSFX");
        if (currentLevel>0 && currentLevel < 3)
        {
            currentLevel++;
            Restart();
        }
        else
        {
            GameOver();
        }
    }
        
        //LevelManager.Instance.Level = currentLevel;
        //InitialPlayerPosition();
        //BackgroundManager.Instance.ChangeBackground(currentLevel);

        

    public void PlayAgain()
    {
        InitialGraphics();
        Restart();
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        FindObjectOfType<AudioManager>().Play("PauseSFX");
        Time.timeScale = 0f;
    }
    public void OpenOptions()
    {
        FindObjectOfType<AudioManager>().Play("PauseSFX");
        optionsCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("TitleScene");
    }

    public void OpenInventory()
    {
        FindObjectOfType<AudioManager>().Play("PauseSFX");
        inventoryCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ClosedInventory()
    {
        FindObjectOfType<AudioManager>().Play("Unpause");
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
        FindObjectOfType<AudioManager>().Play("PauseSFX");
        gameCanvas.SetActive(true);
    }


    public void ClosedOptions()
    {
        
        optionsCanvas.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Unpause");
    }

    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("Unpause");
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
            //AudioListener.pause = false;
            //AudioManager.Instance.BackgroundMusicOn();
            FindObjectOfType<AudioManager>().BackgroundMusicOff();
        } else
        {
            //AudioManager.Instance.BackgroundMusicOn();
            FindObjectOfType<AudioManager>().BackgroundMusicOn();
            //AudioListener.pause = false;
            txt.text = "BGM ON";
        }
    }

    public void YesQuiz()
    {
        // close confirmation 
        // open question answer canvas
        // start the quiz
        InitialGraphics();
        gameCanvas.SetActive(false);
        questionAnswerCanvas.SetActive(true);
        QuizManager.Instance.DisplayInterface();
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

    public void RestartQuiz()
    {
        
        Debug.Log(quizRetake + " QuizRetakeCount");
       if(quizRetake < 3)
        {
            questionAnswerCanvas.SetActive(false);
            ScoreManager.Instance.DefaultGraphics();
            ScoreManager.Instance.ResetScore();
            QuizManager.Instance.RestartQuiz();
            gameCanvas.SetActive(true);
            Time.timeScale = 1f;
           
        } else
        {
            gameObject.GetComponent<FailedQuiz>().HideRetakeQuizButton();
        }

        quizRetake++;
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
            FindObjectOfType<AudioManager>().SoundEffectOn();
        }
        else
        {
            txt.text = "SFX ON";
            
            FindObjectOfType<AudioManager>().SoundEffectMute();
        }
    }
}
