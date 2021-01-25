using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickUp : MonoBehaviour
{
    private Inventory inventory;

    public Item item;

    private bool isAlreadyPick;

    private void Start()
    {
        isAlreadyPick = false;
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAlreadyPick == false)
        {
            GameManager.Instance.OpenInventory();
            isAlreadyPick = true;
            Debug.Log(item.ItemGameObject.prefab.name +" pick up");
            for(int i = 0; i<inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    // Add item to the inventory
                    inventory.isFull[i] = true;
                    Instantiate(item.ItemGameObject.prefab, inventory.slots[i].transform, false);
                    inventory.ItemCount += 1;
                    inventory.UpdateInventory();
                    inventory.slots[i].transform.parent.GetComponent<Button>().onClick.Invoke();

                    Debug.Log("itemCount" + inventory.ItemCount);
                    Destroy(gameObject);
                    break;
                    
                }
            }

            
        }
    }
}
