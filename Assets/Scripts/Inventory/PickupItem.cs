using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PickupItem : MonoBehaviour
{
    public ItemData data = new ItemData();

    public virtual void PickedUp() //this runs after having picked up an item, and now it needs to go somewhere
    {
        int slot; //determines which slot in the array/inventory it can go into, as only 20 are available
        int conSlot;
        Debug.Log(data.itemName);

        if (data.isStackable) //if the item can be stacked
        {           
            if(data.itemType !="Potion")
            {
                Debug.Log("stackable item contacted. Searching for available slot");

                //takes the pickup item and searches the inv for an existing slot that can it can go into, and also has room
                slot = InventoryManager.invMan.FindAvailableStackSlot(this);

                if (slot >= 0) //if there is a place in the inventory for the item to go
                {
                    //take the slot's data and find the item count in the place, then add our count to it
                    InventoryManager.invMan.items[slot].count += data.count;
                    InventoryManager.invMan.UpdateSlot(slot); //update the slot display
                    InventoryManager.invMan.UpdateDescription(slot);

                    Debug.Log("An item has been added to a stack.");
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    Debug.Log("No slot with enough space was found"); //later add in inventory full warning
                }
            }
            else
            {
                conSlot = InventoryManager.invMan.FindAvailableConSlot(this);
                if (conSlot >= 0)
                {
                    InventoryManager.invMan.consumables[conSlot].count += data.count;
                    //update con slot
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("no con space found");
                }
            }
        }

        if (data.itemType !="Potion")
        {
            Debug.Log("Searching for an empty slot");
            slot = InventoryManager.invMan.FindEmptySlot(this);

            if (slot >= 0) //if there is a slot found
            {
                InventoryManager.invMan.items[slot] = data; //take data, no need to add to the count
                InventoryManager.invMan.UpdateSlot(slot); //update slot display
                InventoryManager.invMan.UpdateDescription(slot);

                Debug.Log("an item has been picked up");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No valid slot was found");
            }
        }
        else
        {
            conSlot = InventoryManager.invMan.FindEmptyConSlot(this);
            if (conSlot >= 0) //if there is a slot found
            {
                InventoryManager.invMan.consumables[conSlot] = data; //take data, no need to add to the count
                InventoryManager.invMan.UpdateConSlot(conSlot); //update slot display

                Debug.Log("an item has been picked up");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No valid slot was found");
            }
        }
        
    }
}
