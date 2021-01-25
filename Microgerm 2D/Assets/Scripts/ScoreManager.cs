using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private GameObject passedCanvas, failedCanvas;

    [SerializeField] private int passingScore1, passingScore2, passingScore3;

    private int passingScore;

    private int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        passedCanvas = GameObject.Find("PassedCanvas");
        failedCanvas = GameObject.Find("FailedCanvas");
        score = 0;
        DefaultGraphics();
    }

    public void DefaultGraphics()
    {
        passedCanvas.gameObject.SetActive(false);
        failedCanvas.gameObject.SetActive(false);
    }

    public void ResetScore()
    {
        score = 0;
    }

    private int GetPassingScore()
    {
        switch (GameManager.Instance.CurrentLevel)
        {
            case 1:
                passingScore = passingScore1;
                break;
            case 2:
                passingScore = passingScore2;
                break;
            case 3:
                passingScore = passingScore3;
                break;
            default:
                break;
        }
        return passingScore;
    }

    public void CheckScore()
    {
        if(score >= GetPassingScore())
        {
            passedCanvas.gameObject.SetActive(true);
            failedCanvas.gameObject.SetActive(false);

            passedCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Score : " + score;
        } else
        {
           failedCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Score : " + score;
           failedCanvas.gameObject.SetActive(true);
           passedCanvas.gameObject.SetActive(false);
        }
    }

}
