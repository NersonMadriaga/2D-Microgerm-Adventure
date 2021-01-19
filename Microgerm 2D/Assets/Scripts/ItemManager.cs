using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
   public static ItemManager instance { get; private set; }

    private ItemListSO itemList;
    private ItemSO item;
    private int index;

    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    private void Awake()
    {
        instance = this;
        index = 0;
    }

    private void Start()
    {
        itemList = Resources.Load<ItemListSO>(typeof(ItemListSO).Name);
    }

    public ItemSO GetItemDrop()
    {

        if (index < itemList.list.Count)
        {
            item = itemList.list[index];
        }
        else
        {
            Debug.Log("No more Item in itemlist");
        }

        return item;
    }



}
