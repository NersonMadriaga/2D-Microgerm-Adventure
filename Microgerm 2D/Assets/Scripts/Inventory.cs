using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory : MonoBehaviour
{
    public bool[] isFull;

    public GameObject[] slots;

    public Sprite itemImage;

    private int itemCount;

    [SerializeField] private GameObject inventoryCounter;

    public int ItemCount
    {
        get
        {
            return itemCount;
        }
        set
        {
           itemCount = value;
        }
    }

    private void Start()
    {
        itemCount = 0;

        inventoryCounter.SetActive(false);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == true)
            {
                slots[i].GetComponent<Image>().sprite = itemImage;
                
            }
        }

        Debug.Log("item count " + itemCount);

        if(itemCount > -1)
        {
            inventoryCounter.SetActive(true);
            inventoryCounter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemCount.ToString();
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
