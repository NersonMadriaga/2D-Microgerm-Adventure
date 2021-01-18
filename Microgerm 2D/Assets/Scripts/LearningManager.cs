using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LearningManager : MonoBehaviour
{
    public static LearningManager Instance { get; private set; }

    public GameObject learningOneCanvas, learningTwoCanvas, learningThreeCanvas, inventoryCanvas;

    [SerializeField] private TextMeshProUGUI hint;

    public ItemSO item;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindCanvas();
        InitialGraphics();
    }

    private void FindCanvas()
    {
        learningOneCanvas = GameObject.Find("LearningOneCanvas");
        learningTwoCanvas = GameObject.Find("LearningTwoCanvas");
        learningThreeCanvas = GameObject.Find("LearningThreeCanvas");
        inventoryCanvas = GameObject.Find("InventoryCanvas");
    }

    private void InitialGraphics()
    {
        learningOneCanvas.gameObject.SetActive(false);
        learningTwoCanvas.SetActive(false);
        learningThreeCanvas.SetActive(false);
        //inventoryCanvas.SetActive(false);
    }

    public void OpenLearningPhase()
    {
        switch (GameManager.Instance.CurrentLevel)
        {
            default:
            case 1:
                OpenLearningOneCanvas();
                break;
            case 2:
                OpenLearningTwoCanvas();
                break;
            case 3:
                OpenLearningThreeCanvas();
                break;
            
        }

        inventoryCanvas.SetActive(false);
    }

    public void OpenLearningOneCanvas()
    {
        GameManager.Instance.ClosedGameCanvas();
        learningOneCanvas.SetActive(true);
    }

    public void ClosedLearning()
    {
        InitialGraphics();
        GameManager.Instance.OpenInventory();
    }

    public void OpenLearningTwoCanvas()
    {
        GameManager.Instance.ClosedGameCanvas();
        learningTwoCanvas.SetActive(true);
    }

    public void OpenLearningThreeCanvas()
    {
        GameManager.Instance.ClosedGameCanvas();
    }

}
