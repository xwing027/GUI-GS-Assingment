using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour
{
    public ItemData[] chestItems = new ItemData[10]; //this is the actual inventory here. can increase to make inv bigger
    public Button[] chestButtons;
    public static Chest chestScript;
    public Sprite emptyCSlot;
    public GameObject chestScreen;
    public static bool isChestActive;
    int itemIndex;

    //description stuff
    public GameObject[] chestDescScreen;
    public Text[] chestDescName;
    public Text[] chestItemDesc;
    public Image[] chestDescIcon;
    public Text[] descIndexText;
    int descIndex;

    private void Start()
    {
        //basically there should only be one inv manager
        if (chestScript == null)
        {
            //if there isnt one, make this the manager
            chestScript = this;
        }
        else
        {
            //otherwise, destroy it
            Destroy(this);
        }
        for (int i = 0; i < chestButtons.Length; i++)
        {
            UpdateChestSlot(i);
            UpdateChestDesc(i);
        }
    }

    public void CloseChest()
    {
        chestScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isChestActive = false;
        InventoryManager.isInvActive= false;
    }

    public void OpenChest()
    {
        chestScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        isChestActive = true;
    }

    #region Visuals
    public void UpdateChestSlot(int buttonIndex) //buttonIndex here is the button number we're updating
    {
        chestButtons[buttonIndex].GetComponent<Image>().sprite = chestItems[buttonIndex].icon; //get the matching icon and display

        if (chestItems[buttonIndex].isStackable)
        {
            chestButtons[buttonIndex].GetComponentInChildren<Text>().text = chestItems[buttonIndex].count + ""; //get the matching name and display
        }
        else
        {
            chestButtons[buttonIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }
        if (chestItems[buttonIndex].icon == null)
        {
            chestButtons[buttonIndex].GetComponent<Image>().sprite = emptyCSlot;
        }

        if (chestItems[buttonIndex].itemName == null || chestItems[buttonIndex].itemName == "")
        {
            chestButtons[buttonIndex].GetComponent<Button>().interactable = false;
        }
        else
        {
            //update functionality
            chestButtons[buttonIndex].GetComponent<Button>().interactable = true;
        }

        chestItems[buttonIndex].slot = buttonIndex; //set the item slot to the button index pos
    }

    public void ClearChestSlot(int index)
    {
        //remove item data
        chestItems[index] = new ItemData();

        //remove icon
        chestButtons[index].GetComponent<Image>().sprite = emptyCSlot;

        //remove text
        chestButtons[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        chestButtons[index].GetComponent<Button>().interactable = false;
    }

    public void UpdateChestDesc(int index)
    {
        chestDescIcon[index].GetComponent<Image>().sprite = chestItems[index].icon; //update desc icon
        chestDescName[index].GetComponent<Text>().text = chestItems[index].itemName; //update desc name
        chestItemDesc[index].GetComponent<Text>().text = chestItems[index].description; //update desc
        descIndexText[index].GetComponent<Text>().text = chestItems[index].slot.ToString();
    }

    public void ShowChestDesc()
    {
        if (!InventoryManager.isInvActive)
        {
            for (int i = 0; i < chestButtons.Length; i++)
            {
                if (chestButtons[itemIndex].name == descIndexText[i].GetComponent<Text>().text)
                {
                    Debug.Log("desc index "+chestButtons[itemIndex].name == chestItems[itemIndex].slot.ToString());
                    Debug.Log("showing panel...");
                    //show that panel
                    chestDescScreen[i].SetActive(true);
                }
                else //hide all others
                {
                    Debug.Log("hiding desc");
                    chestDescScreen[i].SetActive(false);
                }
            }
        }
    }

    public void HideChestDescription()
    {
        for (int i = 0; i < chestButtons.Length; i++)
        {
            if (chestDescScreen[i].activeSelf)
            {
                chestDescScreen[i].SetActive(false);
            }
        }
    }
    #endregion

    #region Putting in inventory
    public int FindInvSlotStack(int item) //for stackable
    {
        Debug.Log("looking for slot");
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (chestItems[itemIndex].itemName == InventoryManager.invMan.items[i].itemName && InventoryManager.invMan.items[i].count <= 10 - chestItems[itemIndex].count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindInvSlot(int item) //for non stack
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (InventoryManager.invMan.items[i].itemName == "" || InventoryManager.invMan.items[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void OnChestClick()
    {
        if (InventoryManager.isInvActive)
        {
            int slot;
            if (chestItems[itemIndex].isStackable)
            {
                Debug.Log("stackable item contacted. Searching for available slot CHEST");
                slot = FindInvSlotStack(itemIndex);
                Debug.Log("stack slot: " + slot);
                if (slot >= 0)
                {
                    InventoryManager.invMan.items[slot].count += chestItems[itemIndex].count;
                    InventoryManager.invMan.UpdateSlot(slot);
                    InventoryManager.invMan.UpdateDescription(slot);

                    ClearChestSlot(itemIndex);
                    return;
                }
                else
                {
                    Debug.Log("no stack bye");
                }
            }
            slot = FindInvSlot(itemIndex);
            Debug.Log(slot);
            if (slot >= 0)
            {
                InventoryManager.invMan.items[slot] = chestItems[itemIndex];
                InventoryManager.invMan.UpdateSlot(slot);
                InventoryManager.invMan.UpdateDescription(slot);

                ClearChestSlot(itemIndex);
            }
        }
    }
    #endregion

    public void GetButtonIndex()
    {
        #region Getting item info
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        bool buttonNameCheck = clickedButton.name == "0" || clickedButton.name == "1" || clickedButton.name == "2" || clickedButton.name == "3" || clickedButton.name == "4" || clickedButton.name == "5" || clickedButton.name == "6" || clickedButton.name == "7" || clickedButton.name == "8" || clickedButton.name == "9";

        if (buttonNameCheck)
        {
            itemIndex = Int32.Parse(clickedButton.name);
            Debug.Log("this button index is: " + itemIndex);
        }
        else
        {
            Debug.Log("doesn't match");
        }
        #endregion
    }
}
