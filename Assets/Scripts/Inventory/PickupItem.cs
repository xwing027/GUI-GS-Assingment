using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PickupItem : MonoBehaviour
{
    public ItemData data = new ItemData();

    public virtual void PickedUp() //this will run when close to an object
    {
        int slot; //determines which slot in the array/inventory it can go into, as only 10 are available
        if (data.isStackable) //if the item can be stacked
        {
            Debug.Log("stackable item contacted. Searching for available slot");
            
            //takes the pickup item and searches the inv for an existing slot that can it can go into, and also has room
            slot = InventoryManager.invMan.FindAvailableStackSlot(this);
            
            if (slot>= 0) //if there is a place in the inventory for the item to go
            {
                //take the slot's data and find the item count in the place, then add our count to it
                InventoryManager.invMan.items[slot].count += data.count;
                InventoryManager.invMan.UpdateSlot(slot); //update the slot display

                Debug.Log("An item has been added to a stack.");
                Destroy(gameObject);
                return;
            }
            else
            {
                Debug.Log("No slot with enough space was found"); //later add in inventory full warning
            }
        }

        Debug.Log("Searching for an empty slot");
        slot = InventoryManager.invMan.FindEmptySlot(this);
        
        if (slot >= 0) //if there is a slot found
        {
            InventoryManager.invMan.items[slot] = data; //take data, no need to add to the count
            InventoryManager.invMan.UpdateSlot(slot); //update slot display

            Debug.Log("an item has been picked up");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No valid slot was found");
        }
    }


}
