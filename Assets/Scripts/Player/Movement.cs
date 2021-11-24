using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Character")]
    public Vector3 moveDir;
    [SerializeField]
    private CharacterController _charC;

    [Header("Character Speeds")]
    public float speed;
    public float walk = 5, run = 10, crouch = 2.5f, jumpSpeed = 8, gravity = 20;
    //writing it like above means the header only goes above the first line, and prevents having to write new lines for each variable

    [Header("Input")]
    public Vector2 input;

    public GameObject statPage;

    void Start()
    {
        //_charC is set to the character controller on this Gameobject
        _charC = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (Input.GetKey(KeybindsManager.keys["Forward"]))
        {
            input.y = 1;
        }
        else if (Input.GetKey(KeybindsManager.keys["Backward"]))
        {
            input.y = -1;
        }
        else
        {
            input.y = 0;
        }

        if (Input.GetKey(KeybindsManager.keys["Left"]))
        {
            input.x = -1;
        }
        else if (Input.GetKey(KeybindsManager.keys["Right"]))
        {
            input.x = 1;
        }
        else
        {
            input.x = 0;
        }

        if (Input.GetKey(KeybindsManager.keys["Sprint"]))
        {
            speed = run;
        }
        else if (Input.GetKey(KeybindsManager.keys["Crouch"]))
        {
            speed = crouch;
        }
        else
        {
            speed = walk;
        }
        if (_charC.isGrounded) // if our character is grounded
        {
            //set moveDir to the inputs direction
            moveDir = new Vector3(input.x, 0, input.y);

            //moveDir's forward is changed from global z to the game objects local z - allows us to move where the player is facing
            moveDir = transform.TransformDirection(moveDir);

            //move dir is multiplied by the speed so we move at a decent pace
            moveDir *= speed;

            // if the input button for jump is pressed then
            if (Input.GetKey(KeybindsManager.keys["Jump"]))
            {
                //our moveDir.y is equal to our jump speed
                moveDir.y = jumpSpeed;
            }
        }
        //regardless of if we are grounded or not the players moveDir.y is always affected by gravity multiplied by time.deltaTime to normalise it
        moveDir.y -= gravity * Time.deltaTime;

        //we then tell teh character controller that it is moving in the direction *time.deltaTime
        _charC.Move(moveDir * Time.deltaTime);

        //just chucking this in here for the extra ui stuff that doesn't have to be functional
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (statPage.activeSelf)
            {
                statPage.SetActive(false);
            }
            else
            {
                statPage.SetActive(true);
            }
        }
    }
}
