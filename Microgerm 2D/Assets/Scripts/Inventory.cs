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
        foreach(GameObject slot in slots)
        {
            slot.GetComponent<Image>().sprite = itemImage;
        }
    }
}
