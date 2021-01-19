using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New List", menuName = "ScriptableObjects/Item List")]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> list;
}
