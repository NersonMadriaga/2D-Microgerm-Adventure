using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public bool[] isFull;

    public GameObject[] slots;

    public Sprite itemImage;

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == true)
            {
                slots[i].GetComponent<Image>().sprite = itemImage;
            }
        }
    }

    public void ClearInventory()
    {
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Image>().sprite = null;
        }
    }
}
