using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryManager : MonoBehaviour
{
    #region variables
    //inventory variables
    public ItemData[] items = new ItemData[10]; //this is the actual inventory here. can increase to make inv bigger
    public Button[] invButtons;
    public static InventoryManager invMan;
    public Sprite emptySlot;
    public GameObject invScreen;
    public static bool isInvActive;
    public int playerMoney;
    public Text moneyDisplay;
    public Text[] itemMoneyDisplay;
    PlayerHandler playerH;

    //consumables stuff
    public ItemData[] consumables = new ItemData[4];
    public Image[] conImages;
    public Sprite emptyConSlot;
    public bool isThereCon;
    public int stat;
    public int buttonIndex;

    //extra screen info stuff (use/discard and information)
    public GameObject[] extraScreen;
    public Text[] descName;
    public Text[] itemDescription;
    public Image[] descIcon;
    public Button[] discard;
    public Button[] use;
    public Text[] descIndexText;
    int descIndex;
    #endregion

    private void Start()
    {
        //basically there should only be one inv manager
        if (invMan == null)
        {
            //if there isnt one, make this the manager
            invMan = this;
        }
        else
        {
            //otherwise, destroy it
            Destroy(this);
        }

        playerH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeybindsManager.keys["Inventory"])&&!PauseManager.isPaused)
        {
            isInvActive = !isInvActive;
            invScreen.SetActive(isInvActive);
            if (isInvActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
            else
            {
                if (Chest.isChestActive)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0;
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                }
                
            }
        }

        moneyDisplay.text = "$ " + playerMoney.ToString(); //update player money 
    }

    #region pickupitem functions
    public int FindAvailableStackSlot(PickupItem item) //btw int means it will return an int at the end of the function, void means nothing
    {
        for (int i = 0; i < items.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (item.data.itemName == items[i].itemName && items[i].count <= 10 -item.data.count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindEmptySlot(PickupItem item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (items[i].itemName == "" || items[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void UpdateSlot(int buttonIndex) //buttonIndex here is the button number we're updating
    {
        invButtons[buttonIndex].GetComponent<Image>().sprite = items[buttonIndex].icon; //get the matching icon and display

        if (items[buttonIndex].isStackable)
        {
            invButtons[buttonIndex].GetComponentInChildren<Text>().text = items[buttonIndex].count + ""; //get the matching name and display
        }
        else
        {
            invButtons[buttonIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }

        //update functionality
        invButtons[buttonIndex].GetComponent<Button>().interactable = true;

        if (Shop.isShopActive)
        {
            itemMoneyDisplay[buttonIndex].GetComponent<Text>().text = "$ " + items[buttonIndex].value;
        }

        items[buttonIndex].slot = buttonIndex; //set the item slot to the button index pos


    }

    public void ClearSlot(int index)
    {
        //remove item data
        items[index] = new ItemData();

        //remove icon
        invButtons[index].GetComponent<Image>().sprite = emptySlot;

        //remove text
        invButtons[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        invButtons[index].GetComponent<Button>().interactable = false;

        itemMoneyDisplay[buttonIndex].GetComponent<Text>().text = "";
    }

    public int FindAvailableConSlot(PickupItem item)
    {
        for (int i = 0; i < consumables.Length; i++) //loop through the consumables
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (item.data.itemName == consumables[i].itemName && consumables[i].count <= 10 - item.data.count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindEmptyConSlot(PickupItem item)
    {
        for (int i = 0; i < consumables.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (consumables[i].itemName == "" || consumables[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void UpdateConSlot(int conIndex)
    {
        isThereCon = true;
        //update icon and text
        conImages[conIndex].GetComponent<Image>().sprite = consumables[conIndex].icon; //get the matching icon and display

        if (consumables[conIndex].isStackable)
        {
            conImages[conIndex].GetComponentInChildren<Text>().text = consumables[conIndex].count + ""; //get the matching number and display
        }
        else
        {
            conImages[conIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }
        consumables[conIndex].slot = conIndex; //set the item slot to the button index pos 
    }

    public void ClearConSlot(int index)
    {
        isThereCon = false;
        //remove item data
        consumables[index] = new ItemData();

        //remove icon
        conImages[index].GetComponent<Image>().sprite = emptyConSlot;

        //remove text
        conImages[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        //invButtons[index].GetComponent<Button>().interactable = false;
    }
    #endregion

    #region equipping item functions
    public int FindEquipSlot(int item)
    {
        for (int i = 0; i < Equipping.equipScript.body.Length; i++)
        {
            if (Equipping.equipScript.body[i].itemName == "" || Equipping.equipScript.body[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void EquipArmour()
    {
        int slot;
        slot = FindEquipSlot(buttonIndex);
        if (slot >= 0)
        {
            Equipping.equipScript.body[slot] = items[buttonIndex];
            Equipping.equipScript.UpdateEquipSlot(slot);

            ClearSlot(buttonIndex);
        }

        extraScreen[buttonIndex].SetActive(false);
    }

    public void EquipWeapon()
    {
        int slot;
        slot = FindWSlot(buttonIndex);
        if (slot >= 0)
        {
            Equipping.equipScript.hands[slot] = items[buttonIndex];
            Equipping.equipScript.UpdateWSlot(slot);

            ClearSlot(buttonIndex);
        }

        extraScreen[buttonIndex].SetActive(false);
    }

    public int FindWSlot(int index)
    {
        for (int i = 0; i < Equipping.equipScript.hands.Length; i++)
        {
            if (Equipping.equipScript.hands[i].itemName == "" || Equipping.equipScript.hands[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }
    #endregion

    #region shop functions
    public int FindShopSlotStack(int item) //btw int means it will return an int at the end of the function, void means nothing
    {
        for (int i = 0; i < Shop.shopScript.shopItems.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (items[buttonIndex].itemName == Shop.shopScript.shopItems[i].itemName && Shop.shopScript.shopItems[i].count <= 10 - items[buttonIndex].count)
            {
                return (i);
            }
        }
        return -1;
    }

    public int FindShopSlot(int item)
    {
        for (int i = 0; i < Shop.shopScript.shopItems.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (Shop.shopScript.shopItems[i].itemName == "" || Shop.shopScript.shopItems[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void Sell()
    {
        if (Shop.isShopActive)
        {
            int slot;
            if (items[buttonIndex].isStackable)
            {
                slot = FindShopSlotStack(buttonIndex);
                if (slot >= 0)
                {
                    Shop.shopScript.shopItems[slot].count += items[buttonIndex].count;
                    Shop.shopScript.UpdateShopSlot(slot);

                    UpdateMoneySell(buttonIndex);
                    ClearSlot(buttonIndex);
                    return;
                }
                else
                {
                    Debug.Log("no stack bye");
                }
            }
            slot = FindShopSlot(buttonIndex);
            Debug.Log(slot);
            if (slot >= 0)
            {
                Shop.shopScript.shopItems[slot] = items[buttonIndex];
                Shop.shopScript.UpdateShopSlot(slot);

                UpdateMoneySell(buttonIndex);
                ClearSlot(buttonIndex);
                return;
            }
        }
    }

    void UpdateMoneySell(int item)
    {
        playerMoney += items[item].value;
    }
    #endregion

    #region Chest functions
    public int FindChestSlotStack(int item)
    {
        Debug.Log("looking for slot");
        for (int i = 0; i < Chest.chestScript.chestItems.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (items[buttonIndex].itemName == Chest.chestScript.chestItems[i].itemName && Chest.chestScript.chestItems[i].count <= 10 - items[buttonIndex].count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindChestSlot(int item)
    {
        for (int i = 0; i < Chest.chestScript.chestItems.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (Chest.chestScript.chestItems[i].itemName == "" || Chest.chestScript.chestItems[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void PutInChest()
    {
        if (Chest.isChestActive)
        {
            int slot;
            if (items[buttonIndex].isStackable)
            {
                Debug.Log("stackable item contacted. Searching for available slot CHEST");
                slot = FindChestSlotStack(buttonIndex);
                Debug.Log("stack slot: " + slot);
                if (slot >= 0)
                {
                    Chest.chestScript.chestItems[slot].count += items[buttonIndex].count;
                    Chest.chestScript.UpdateChestSlot(slot);
                    Chest.chestScript.UpdateChestDesc(slot);

                    ClearSlot(buttonIndex);
                    return;
                }
                else
                {
                    Debug.Log("no stack bye");
                }
            }

            slot = FindChestSlot(buttonIndex);
            Debug.Log(slot);
            if (slot >= 0)
            {
                Chest.chestScript.chestItems[slot] = items[buttonIndex];
                Chest.chestScript.UpdateChestSlot(slot);
                Chest.chestScript.UpdateChestDesc(slot);

                ClearSlot(buttonIndex);
            }
        }
    }
    #endregion

    #region description functions
    public void UpdateDescription(int descIndex)
    {
        descIcon[descIndex].GetComponent<Image>().sprite = items[descIndex].icon; //update desc icon
        descName[descIndex].GetComponent<Text>().text = items[descIndex].itemName; //update desc name
        itemDescription[descIndex].GetComponent<Text>().text = items[descIndex].description; //update desc
        
        //get the index of the item in the inventory and put it on a text element
        descIndex = Array.IndexOf(items, items[descIndex]);
        descIndexText[descIndex].GetComponent<Text>().text = descIndex.ToString();

        //set the discard button function cuz idk how to get it to work with an on click
        discard[descIndex].GetComponent<ClickableObject>().leftClick = items[descIndex].Drop;

        if (items[descIndex].itemType == "Armour")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Equip";
            //change function to armour equip
            use[descIndex].GetComponent<Button>().onClick.AddListener(EquipArmour);
        }
        if (items[descIndex].itemType == "Food")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Eat";
            //change function to eat food
            use[descIndex].GetComponent<Button>().onClick.AddListener(EatFood);
        }
        if (items[descIndex].itemType == "Weapon")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Equip";
            //change function to weapon equip
            use[descIndex].GetComponent<Button>().onClick.AddListener(EquipWeapon);
        }
    }

    public void EatFood()
    {
        Debug.Log("mmm ooey gooey yummy");

        //hide screen after use
        playerH.attributes[0].currentValue += items[buttonIndex].itemBonus;
        ClearSlot(buttonIndex);
        HideDescription();
    }

    public void ShowDescription()
    {
        //on click...
        //get the itemName attached to the button clicked
        //if the itemName matches the descName on the desc panel
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject.name == descIndexText[i].GetComponent<Text>().text &&!Chest.isChestActive&&!Shop.isShopActive)
            {
                //show that panel
                extraScreen[i].SetActive(true);
            }
            else //hide all others
            {
                extraScreen[i].SetActive(false);
            }
        }
    }

    public void HideDescription()
    {
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (extraScreen[i].activeSelf)
            {
                extraScreen[i].SetActive(false);
            }
        }
    }
    #endregion

    #region sorting buttons
    public void SortFood()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Food") //if its not food, hide it
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icon
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear; //hide text

                //stop functionality
                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "Food") //if it is food, show it (in case it was already hidden)
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<Button>().interactable = true;
            }
        } 
    }

    public void SortArmour()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Armour")
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icon
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                //stop functionality
                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "Armour")
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void SortWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Weapon")
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icon
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                //stop functionality
                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "Weapon")
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ShowAll()
    {
        for (int i = 0; i < items.Length; i++)
        {
            //get icons back
            invButtons[i].GetComponent<Image>().color = Color.white;
            invButtons[i].GetComponentInChildren<Text>().color = Color.white;

            //bring back functionality
            invButtons[i].GetComponent<Button>().interactable = true;
        }
    }
    #endregion

    public void ButtonClick()
    {
        //this is used to get the selected button so we can grab the item info that's attached to it
        //get the button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        //check if the button name is an index number
        bool buttonNameCheck = clickedButton.name == "0" || clickedButton.name == "1" || clickedButton.name == "2" || clickedButton.name == "3" || clickedButton.name == "4" || clickedButton.name == "5" || clickedButton.name == "6" || clickedButton.name == "7" || clickedButton.name == "8" || clickedButton.name == "9";
        if (buttonNameCheck) //if yes
        {
            buttonIndex = Int32.Parse(clickedButton.name); //set the button name, aka the index, to an int to store the index
        }
    }
}
