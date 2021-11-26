using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName; //item's name 
    public bool isStackable; //can you stack it
    public int count; //how many
    public Sprite icon; //item icon
    public GameObject droppedObj; //the prefab the spawns when we drop it
    public int value; //cost for buying and selling
    public int slot; //inventory slot?

    public void Use()
    {
        Debug.Log("We have used " + itemName);
    }

    public void Drop()
    {
        Debug.Log("We have dropped " + itemName);

        //dropped item means that if the in world item has more items (if stackable) than the prefab
        //the amount the in world version has will override the prefab amount 
        //e.g. if prefab is 1 but in world is stack of 5, it will be 5 upon pickup, not revert back to 1
        GameObject droppedItem= GameObject.Instantiate(Resources.Load("PickupItems/" + itemName) as GameObject,PlayerPickup.dropLoc,Quaternion.identity);
        droppedItem.GetComponent<PickupItem>().data = this;
        InventoryManager.invMan.ClearSlot(slot);
    }
}
