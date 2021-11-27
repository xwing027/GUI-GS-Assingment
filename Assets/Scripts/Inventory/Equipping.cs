using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipping : PickupItem
{
    //public ItemData data; //not needed if inheriting from pickupdata
    public static Equipping equipScript;

    private void Awake()
    {
        equipScript = this;
    }

    public void Use()
    {
        if(data.itemType == "Armour")
        {
            int bodySlot;
            if (data.count == 1) //if there is only one of the item, run
            {
                bodySlot = InventoryManager.invMan.FindEquipSlot(this); //the item goes into the body slot
                if (bodySlot == 0) //if the body slot is empty
                {
                    Debug.Log("Found empty slot");
                    //InventoryManager.invMan.body[bodySlot] = data; //take data
                    InventoryManager.invMan.UpdateArmourSlot(bodySlot); //show in armour icon
                    InventoryManager.invMan.ClearSlot(bodySlot);//clear from inventory
                }
            }
            else //otherwise exit as you can't stack weapons ig
            {
                return;
            }
        }
    }
}
