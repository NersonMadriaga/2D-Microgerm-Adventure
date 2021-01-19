using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO itemGameObject;

    public ItemSO ItemGameObject
    {
        get
        {
            return itemGameObject;
        }
    }

    private void Start()
    {
    }
}
