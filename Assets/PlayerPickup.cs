using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public static Vector3 dropLoc;

    private void Update() //change to fixedupdate for ranged pickup
    {
        dropLoc = transform.position + transform.forward * 2; //drop location 2m in front of character

        if (Input.GetKeyDown(KeybindsManager.keys["Interact"]))
        {
            Collider[] items = Physics.OverlapSphere(transform.position, 2f, 1 << 6);

            Collider closestCollider;
            float closestDistance = 100;
            closestCollider = items[0];

            foreach (Collider collider in items) //for each item
            {
                if (Vector3.Distance(transform.position,collider.transform.position)<closestDistance)
                {
                    closestCollider = collider;
                    closestDistance = Vector3.Distance(transform.position, collider.transform.position);
                    
                }
            }
            closestCollider.GetComponent<PickupItem>().PickedUp(); //run the pick up method
        }
        
        //overlap sphere is like a collider, but within the code (like a raycast instead)
        //centre of the sphere is player position. if item is 1m away it will pick it up
        //can only check for objects on a specific layer, pickup layer 6   
    }
}
