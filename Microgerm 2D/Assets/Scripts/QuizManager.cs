using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class QuizManager : MonoBehaviour
{
    public static QuizManager instance { get; private set; }

    [SerializeField] private Inventory inventory;

    [SerializeField] private int questionLimit1, questionLimit2, questionLimit3;

    [SerializeField] private Transform btnMultiple, btnTrueFalse;

    [SerializeField] private GameObject trueFalsePanel, multipleChoicePanel;

    [SerializeField] private TextMeshProUGUI multipleChoiceQuestion, trueFalseQuestion;

    private List<ItemSO> _item;

    private string question, correctAnswer;
    private string[] choices;
    private bool isBoolean, isTrue;
    private int index, questionLevel;

    private ItemSO currentItem;
    private Transform[] buttonChoices;
    private Transform[] buttonTrueFalse;

    private void Awake()
    {
        instance = this;
        index = 0;

        _item = new List<ItemSO>();

        buttonChoices = new Transform[4];
        buttonTrueFalse = new Transform[2];
    }

    private void Start()
    {
        FindGameObjects();
        DefaultGraphics();
    }

    private void GetData()
    {
        question = currentItem.question;
        correctAnswer = currentItem.correctAnswer;
        choices = currentItem.ShuffleChoices();
        isTrue = currentItem.isTrue;
        isBoolean = currentItem.isBoolean;
    }

    public void DisplayInterface()
    {
        GetItem();
        GetData();
        GetPanel();
        DisplayQuestion();
        DisplayButton();
    }

    private void DisplayQuestion()
    {
        if (isBoolean)
        {
            trueFalseQuestion.text = question;
        } else
        {
            multipleChoiceQuestion.text = question;
        }
    }

    public void OnAnswered()
    {
        if (isBoolean)
        {
            foreach (Transform btn in buttonTrueFalse)
            {
                btn.GetComponent<QuizButton>().DisableButton();
            }
        } else
        {
            foreach (Transform btn in buttonChoices)
            {
                btn.GetComponent<QuizButton>().DisableButton();
            }
        }


    }

    public void RevealCorrectAnswer()
    {
        if (isBoolean)
        {
            foreach (Transform btn in buttonTrueFalse)
            {
                btn.GetComponent<QuizButton>().RevealCorrectAnswer();
            }
        }

        if (isBoolean == false)
        {
            foreach (Transform btn in buttonChoices)
            {
                btn.GetComponent<QuizButton>().RevealCorrectAnswer();
            }
        }

    }

    public void NextQuestion()
    {
        Debug.Log("Next Question");

        if (index < GetLimit())
        {
            index++;
            Debug.Log(index + "Quiz Index");
            ClearButtons();
            DisplayInterface();

        } else
        {
            ScoreManager.instance.CheckScore();
            GameManager.Instance.ClosedQuestionAnswerCanvas();
        }
    }

    private int GetLimit()
    {
        int limit = 0;

        switch (GameManager.Instance.CurrentLevel)
        {
            default:
            case 1:
                limit = questionLimit1;
                break;
            case 2:
                limit = questionLimit2;
                break;
            case 3:
                limit = questionLimit3;
                break;
        }

        return limit;
    }
    private void ClearButtons()
    {

        if (isBoolean)
        {
            if (buttonTrueFalse.Length > 0)
            {
                foreach (Transform btn in buttonTrueFalse)
                {
                    Destroy(btn.gameObject);
                }
            }
        }

        if (isBoolean == false)
        {
            if (buttonChoices.Length > 0)
            {
                foreach (Transform btn in buttonChoices)
                {
                    Destroy(btn.gameObject);
                }
            }
        }




    }
    private void DisplayButton()
    {

        if (isBoolean)
        {
            Debug.Log("Button Not Display");
            int btnIndex = -1;
            for (int i = 0; i < 2; i++)
            {
                Transform button = Instantiate(btnTrueFalse, trueFalsePanel.transform);
                buttonTrueFalse[i] = button;

                float offsetAmount = 240f;
                button.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * btnIndex, -150f);
                if (btnIndex == -1)
                {
                    button.GetChild(0).GetComponent<TextMeshProUGUI>().text = "True";
                    button.GetComponent<QuizButton>().IsTrueText = "True";
                } else
                {
                    button.GetChild(0).GetComponent<TextMeshProUGUI>().text = "False";
                    button.GetComponent<QuizButton>().IsTrueText = "False";
                }

                button.GetComponent<QuizButton>().IsTrue = isTrue;
                button.GetComponent<QuizButton>().IsBoolean = isBoolean;

                btnIndex *= -1;


            }
        }

        if (isBoolean == false)
        {

            int btnIndex = 0;
            for (int i = 0; i < choices.Length; i++)
            {
                Transform button = Instantiate(btnMultiple, multipleChoicePanel.transform);
                buttonChoices[i] = button;
                float offsetAmount = -75f;
                button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetAmount * btnIndex);
                btnIndex++;

                button.GetChild(0).GetComponent<TextMeshProUGUI>().text = choices[i];
                button.GetComponent<QuizButton>().Choice = choices[i];
                button.GetComponent<QuizButton>().CorrectAnswer = correctAnswer;

            }


        }
    }

    private void GetPanel()
    {
        if (isBoolean)
        {
            trueFalsePanel.SetActive(true);
            multipleChoicePanel.SetActive(false);
        } else
        {
            multipleChoicePanel.SetActive(true);
            trueFalsePanel.SetActive(false);
        }
    }

    private void FilterQuestionByLevel(int level)
    {
        
        ItemListSO itemList = Resources.Load<ItemListSO>(typeof(ItemListSO).Name);
        
       foreach (ItemSO item in itemList.list)
        {
            if(item.level == level)
            {
                _item.Add(item);
            }
        }
    }
    
    private void GetItem()
    {
        /*
        if(inventory.slots[index].transform.childCount > 0)
        {
          if(inventory.slots[index].transform.GetChild(0).GetComponent<Item>().ItemGameObject.level != GameManager.Instance.CurrentLevel)
            {
                NextQuestion();
            } else
            {
                currentItem = inventory.slots[index].transform.GetChild(0).GetComponent<Item>().ItemGameObject;
            }
           
        } else
        {
            currentItem = Resources.Load<ItemListSO>(typeof(ItemListSO).Name).list[index];
        }
        */

        /*
        if(Resources.Load<ItemListSO>(typeof(ItemListSO).Name).list[index].level != GameManager.Instance.CurrentLevel)
        {
            index++;
        }else
        {
            currentItem = Resources.Load<ItemListSO>(typeof(ItemListSO).Name).list[index];
        }
        */
        FilterQuestionByLevel(GameManager.Instance.CurrentLevel);

        currentItem = _item[index];
        Debug.Log("CurrentItem " + currentItem);
    }
    private void DefaultGraphics()
    {
        trueFalsePanel.SetActive(false);
        multipleChoicePanel.SetActive(false);
    }

    private void FindGameObjects()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        //trueFalsePanel = GameObject.Find("TrueFalsePanel");
        //multipleChoicePanel = GameObject.Find("MultipleChoicePanel");

       
    }

    IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(1);

        foreach (Transform btn in buttonChoices)
        {
            btn.GetComponent<QuizButton>().EnabledButton();
        }
    }
}
