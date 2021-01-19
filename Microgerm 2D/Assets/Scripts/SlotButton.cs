using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotButton : MonoBehaviour
{
    public GameObject slot;

    private GameObject item;

    public void OnClick()
    {
        if(slot.transform.childCount != 0)
        {
            item = slot.transform.GetChild(0).gameObject;
            Debug.Log(item.GetComponent<Item>().ItemGameObject.hint);
            LearningManager.Instance.OpenLearningPhase(item.GetComponent<Item>().ItemGameObject.hint);
        }
        else
        {
            Debug.Log("No item");
        }
        
    }
}
