using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGateTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameObject.Find("Player").GetComponent<Inventory>().ItemCount < QuizManager.Instance.GetLimit())
            {
                GameManager.Instance.GetAllScrolls();
            }
            else
            {
                GameManager.Instance.OpenConfirmation();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.HideAllScrolls();

        }
    }
}
