using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FailedQuiz : MonoBehaviour
{
    [SerializeField] private GameObject retakeButton;

    private void Start()
    {
        retakeButton.SetActive(true);
    }
    public void HideRetakeQuizButton()
    {
        retakeButton.SetActive(false);
    }
}
