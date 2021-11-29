/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    public static List<Item> inv = new List<Item>();
    public static bool showInv;
    public Item selectedItem;
    public static int money;
    public Vector2 scrollPos;
    public string sortType = "All";
    public string[] typeNames = new string[9] { "All","Armour","Weapon","Potion","Food","Ingredient","Craftable","Quest","Misc" };
    public Transform dropLocation;
    [System.Serializable]
    public struct EquippedItems
    {
        public string slotName;
        public Transform equipLocation;
        public GameObject equippedItem;
    };
    public EquippedItems[] equippedItemSlot;
    #endregion

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I)) //keycode for debugging
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
        }

        if (Input.GetKeyDown(IMGUIScript.inputKeys["Inventory"])&&!PauseMenu.isPaused) //if inventory button pressed and no pause menu
        {
            showInv = !showInv;
            if (showInv) //show pause menu
            {
                //curso can be seen
                Cursor.visible = true;
                //cursor not locked
                Cursor.lockState = CursorLockMode.None;
                //time paused
                Time.timeScale = 0f;
            }
            else //otherwise dont
            {
                //cursor cannot be seen
                Cursor.visible = false;
                //cursor is locked
                Cursor.lockState = CursorLockMode.Locked;
                //time is not paused
                Time.timeScale = 1f;
            }
        }
    }

    private void OnGUI()
    {
        if (showInv && !PauseMenu.isPaused) //if we are looking at the inventory
        {
            for (int i = 0; i < typeNames.Length; i++) //this sorts the inventory by type 
            {   //make a button for each sorting type (all, armour, weapon, etc)
                if (GUI.Button(new Rect(2.5f * IMGUIScript.scr.x + i * 1.5f * IMGUIScript.scr.x, 0, IMGUIScript.scr.x * 1.5f, 0.45f * IMGUIScript.scr.y), typeNames[i]))
                {
                    sortType = typeNames[i];
                }
            }

            DisplayInv();
            if (selectedItem != null) //if there is a selected item
            {
                UseItem();
            }
        } 
    }

    void DisplayInv()
    {
        if (!(sortType == "All" ||sortType =="")) //if we have a type selected
        {
            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
            int a = 0; //amount of this type
            int s = 0; //new slot position of the item
            
            for (int i = 0; i < inv.Count; i++) //find all items of type in our inv
            {
                if (inv[i].ItemType == type) //if current element matches type
                {
                    a++; //add amount to this type
                }
            }
            if (a <=15) //display our type that has been filtered if under 15 (aka not long enough to need a scroll view)
            {
                for (int i = 0; i < inv.Count; i++) //for all the items in the inventory
                {
                    if (inv[i].ItemType == type) //if the item's type matches the sorted type
                    {   //make a button showing this item
                        if (GUI.Button(new Rect(IMGUIScript.scr.x * 12.5f, IMGUIScript.scr.y * 2.75f + (s * 0.45f * IMGUIScript.scr.x), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.45f), inv[s].Name))
                        {
                            selectedItem = inv[s]; //if you select the item set the slot to the inventory slot
                        }
                        s++; //increase the slot
                    }
                }
            }
            else //otherwise
            {   //make a scrollview 
                scrollPos = GUI.BeginScrollView(new Rect(11.5f * IMGUIScript.scr.x, IMGUIScript.scr.y * 2.75f, 3.75f * IMGUIScript.scr.x, 6.75f * IMGUIScript.scr.y), scrollPos, new Rect(0, 0, 0, a * 0.45f * IMGUIScript.scr.y), false, true);
                for (int i = 0; i < inv.Count; i++) //for the inventory 
                {
                    if (inv[i].ItemType == type) //if the item type matches the sort
                    {
                        if (GUI.Button(new Rect(IMGUIScript.scr.x * 12.5f, s * 0.45f * IMGUIScript.scr.x, IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.45f), inv[i].Name))
                        {
                            selectedItem = inv[i]; //if you select the item set the slot to the inventory slot
                        }
                        s++; //increase the slot
                    }
                }
                GUI.EndScrollView();
            }
        }
        else //all items are shown
        {
            if (inv.Count <= 15) //if we have enough items to fit on the screen
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x*12.5f, IMGUIScript.scr.y*2.75f+(i*0.45f*IMGUIScript.scr.x), IMGUIScript.scr.x*3, IMGUIScript.scr.y*0.45f),inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
            }
            else //if we do not (scroll needed)
            {
                scrollPos = GUI.BeginScrollView(new Rect(11.5f*IMGUIScript.scr.x, IMGUIScript.scr.y * 2.75f, 3.75f * IMGUIScript.scr.x, 6.75f * IMGUIScript.scr.y),scrollPos,new Rect(0,0,0,inv.Count*0.45f*IMGUIScript.scr.y),false,true);
                for (int i = 0; i < inv.Count; i++)
                {   //for everything in the inventory, show everything (no sort type needed)
                    if (GUI.Button(new Rect(IMGUIScript.scr.x, i * 0.45f * IMGUIScript.scr.x, IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.45f), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                GUI.EndScrollView();
            }
        }
    }

    void UseItem()
    {
        //name - move these so theyre next to the list
        GUI.Box(new Rect(4*IMGUIScript.scr.x,IMGUIScript.scr.y*0.25f, IMGUIScript.scr.x*3, IMGUIScript.scr.y*0.45f),selectedItem.Name);
        //icon
        GUI.Box(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 0.5f, IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 3), selectedItem.IconName);
        //desc, amount, value
        GUI.Box(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.25f, IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 1f), selectedItem.Description+"\nAmount: "+selectedItem.Amount+"\n$"+selectedItem.Value);
        //switch via type
        switch (selectedItem.ItemType)
        {
            case ItemTypes.Armour:
                if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f),"Equip"))
                {
                    //wear the thing
                }
                break;
            case ItemTypes.Weapon:
                if (equippedItemSlot[1].equippedItem == null||selectedItem.Name!=equippedItemSlot[1].equippedItem.name)
                {
                    if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.75f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Equipped"))
                    {
                        //wear the thing
                        if (equippedItemSlot[1].equippedItem!=null)
                        {
                            Destroy(equippedItemSlot[1].equippedItem);
                        }
                        equippedItemSlot[1].equippedItem = Instantiate(selectedItem.MeshName, equippedItemSlot[1].equipLocation);
                        equippedItemSlot[1].equippedItem.name = selectedItem.Name;
                        equippedItemSlot[1].equippedItem.GetComponent<ItemHandler>().enabled = false;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Unequipped"))
                    {
                        Destroy(equippedItemSlot[1].equippedItem);
                        equippedItemSlot[1].equippedItem = null;
                    }
                }
                break;
            case ItemTypes.Potion:
                if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Drink"))
                {
                    //wear the thing
                }
                break;
            case ItemTypes.Money:
                break;
            case ItemTypes.Quest:
                break;
            case ItemTypes.Food:
                if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 4.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Eat"))
                {
                    //wear the thing
                }
                break;
            case ItemTypes.Ingredient:
                if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Use"))
                {
                    //wear the thing
                }
                break;
            case ItemTypes.Craftable:
                if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 3.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Craft"))
                {
                    //wear the thing
                }
                break;
            case ItemTypes.Misc:
                break;
            default:
                break;
        }
        //discard button 
        if (GUI.Button(new Rect(5.5f * IMGUIScript.scr.x, IMGUIScript.scr.y * 4.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.45f), "Discard"))
        {
            //check if the item is equipped
            for (int i = 0; i < equippedItemSlot.Length; i++)
            {
                if (equippedItemSlot[i].equippedItem!= null && selectedItem.Name==equippedItemSlot[i].equippedItem.name)
                {
                    //if so destroy from scene
                    Destroy(equippedItemSlot[i].equippedItem);
                    equippedItemSlot[i].equippedItem = null;
                }
            }

            //spwn at drop location
            GameObject itemToDrop = Instantiate(selectedItem.MeshName, dropLocation.position, Quaternion.identity);
            //apply gravity and make sure its named correctly
            itemToDrop.name = selectedItem.Name;
            itemToDrop.AddComponent<Rigidbody>().useGravity = true;
            //if the maount >1 if so reduce from list
            if (selectedItem.Amount>1)
            {
                selectedItem.Amount--;
            }
            //else remove from list
            else
            {
                inv.Remove(selectedItem);
                selectedItem = null;
                return;
            }
        }
    }
}

*/
