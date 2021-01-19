using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LearningManager : MonoBehaviour
{
    public static LearningManager Instance { get; private set; }

    public GameObject learningCanvas;

    [SerializeField] private GameObject hint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindCanvas();
        InitialGraphics();
    }

    private void DisplayHint(string text)
    {
        hint.GetComponent<TextMeshProUGUI>().text = text;
    }

    private void FindCanvas()
    {
        learningCanvas = GameObject.Find("LearningCanvas");
        hint = GameObject.Find("HintText");
        
        
    }

    private void InitialGraphics()
    {
        learningCanvas.gameObject.SetActive(false);
    }

    public void OpenLearningPhase(string text)
    {
        learningCanvas.gameObject.SetActive(true);
        DisplayHint(text);
        GameManager.Instance.OnClickInventoryButton();

    }

    public void ClosedLearningCanvas()
    {
        InitialGraphics();
        GameManager.Instance.OpenInventory();
    }

}
