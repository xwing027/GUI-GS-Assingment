using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public static Vector3 dropLoc;
    bool isPickingUp;

    private void Update() //change to fixedupdate for ranged pickup
    {
        dropLoc = transform.position + transform.forward * 2; //drop location 2m in front of character

        //create ray
        Ray interactRay;
        //assign the origin - this ray is shooting out from the main camera's screen point centre of screen
        interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        //create hit info
        RaycastHit hitInfo;

        if (Input.GetKeyDown(KeybindsManager.keys["Interact"]))
        {
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interactRay, out hitInfo, 10))
            {
                if (hitInfo.collider.tag == "Chest")
                {
                    Chest.chestScript.OpenChest();
                }
                
                if (hitInfo.collider.tag == "Shop")
                {
                    Shop.shopScript.OpenShop();
                }
            }  
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(interactRay, out hitInfo, 10))
            {
                if (hitInfo.collider.tag == "Item")
                {
                    hitInfo.collider.GetComponent<PickupItem>().PickedUp();
                }
            }
        }

        //overlap sphere is like a collider, but within the code (like a raycast instead)
        //centre of the sphere is player position. if item is 1m away it will pick it up
        //can only check for objects on a specific layer, pickup layer 6   
        /*Collider[] items = Physics.OverlapSphere(transform.position, 2f, 1 << 6); //1<<6 determines the layer (layer 6)

            Collider closestCollider;
            float closestDistance = 100;
            closestCollider = items[0]; 

            foreach (Collider collider in items) //for each item
            {
                if (Vector3.Distance(transform.position,collider.transform.position)<closestDistance)
                {
                    closestCollider = collider;
                    closestDistance = Vector3.Distance(transform.position, collider.transform.position);
                    isPickingUp = true;
                }
            }
            closestCollider.GetComponent<PickupItem>().PickedUp(); //run the pick up method*/
    }
}
