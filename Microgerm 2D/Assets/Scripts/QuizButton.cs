using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuizButton : MonoBehaviour
{
    [SerializeField] private Sprite normal, correct, incorrect;
    private string choice, correctAnswer, isTrueText;
    private bool isTrue, isBoolean;

    public string IsTrueText
    {
        get
        {
            return isTrueText;
        }

        set
        {
            isTrueText = value;
        }
    }
    public bool IsBoolean
    {
        get
        {
            return isBoolean;
        }

        set
        {
            isBoolean = value;
        }
    }
    public bool IsTrue
    {
        get
        {
            return isTrue;
        }

        set
        {
            isTrue = value;
        }
    }

    public string Choice
    {
        set
        {
            choice = value;
        }
    }

    public string CorrectAnswer
    {
        set
        {
            correctAnswer = value;
        }
    }
    private void Start()
    {
        DefaultGraphics();
        gameObject.GetComponent<Button>().interactable = true;
    }

    private void DefaultGraphics()
    {
        gameObject.GetComponent<Image>().sprite = normal;
    }
    public void OnAnswered()
    {
        if(gameObject.GetComponent<Image>().sprite != correct)
        {
            if (isBoolean)
            {
                if ((isTrueText.Equals("True") && isTrue) || (isTrueText.Equals("False") && isTrue == false))
                {
                    CorrectAnswered();
                    ScoreManager.instance.Score += 1;
                } else
                {
                    IncorrectAnswered();
                }
            } else
            {
                if (choice.Equals(correctAnswer))
                {
                    CorrectAnswered();
                    ScoreManager.instance.Score += 1;
                }
                else
                {
                    IncorrectAnswered();
                }
            }
        }
        else
        {
            QuizManager.instance.NextQuestion();
        }

      

        //DisableButton();

        QuizManager.instance.OnAnswered();
    }

    private void CorrectAnswered()
    {
        Debug.Log("Correct Answer");
        gameObject.GetComponent<Image>().sprite = correct;
        
    }

    private void IncorrectAnswered()
    {
        Debug.Log("Incorrect Answer");
        gameObject.GetComponent<Image>().sprite = incorrect;

        QuizManager.instance.RevealCorrectAnswer();
    }

    public void RevealCorrectAnswer()
    {
        if (isBoolean)
        {
            if ((isTrueText.Equals("True") && isTrue) || (isTrueText.Equals("False") && isTrue == false))
            {
                CorrectAnswered();
            }
        } else
        {
            if (choice.Equals(correctAnswer))
            {
                CorrectAnswered();
            }
        }
        
    }

    public void EnableAllButton()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void EnabledButton()
    {
        Debug.Log("Enabled Button");
        if (choice.Equals(correctAnswer))
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        
    }

    public void DisableButton()
    {
        if (isBoolean)
        {
            if ((isTrueText.Equals("True") && isTrue == false) || (isTrueText.Equals("False") && isTrue == true))
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        } else
        {
            if (choice.Equals(correctAnswer) == false)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        
    }

    private void OnDestroy()
    {
        Debug.Log("Button Destroyed");
    }
}
