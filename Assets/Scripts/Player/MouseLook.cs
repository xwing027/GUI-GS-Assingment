using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region RotationalAxis
    //create a public enum called RotationalAxis
    public enum RotationalAxis
    {
        MouseX, MouseY
    }
    #endregion

    #region Variables
    [Header("Rotation")] //reference to the rotational axis and set a default
    public RotationalAxis axis = RotationalAxis.MouseX; //setting the value is not necessary, but good practice

    [Header("Sensitivity")] //this is just the sensitivity for x and y, with a default set
    public Vector2 sensitivity = new Vector2(30, 30);

    [Header("Y Rotation Clamp")] //set the max and min y rotation
    public Vector2 rotationRangeY = new Vector2(-60, 60);
    float _rotationY;
    #endregion

    void Start()
    {
        //Lock cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        //Hide cursor from view
        Cursor.visible = false;

        //if our game object has a rigidboy attached to it
        if (GetComponent<Rigidbody>())
        {
            // set the rigidbodys freezerotation to true(this allows the camera made with charcon to work with a rigidbody)
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        //if our game object has a camera attached to it
        if (GetComponent<Camera>())
        {
            //set our rotation for a MouseY axis
            axis = RotationalAxis.MouseY;
        }
    }

    void Update()
    {
        if (!(PauseManager.isPaused)) //add in or if inventory is not showing
        {

            #region Mouse X
            //if we are rotating on the X
            if (axis == RotationalAxis.MouseX)
            {
                //transform the rotation on our game objects Y by our Mouse inpute mouse X times X sensitivity
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity.x, 0);
            }
            #endregion

            #region Mouse Y
            //else we are only rotating on the Y
            else
            {
                // our rotation is plus equals our mouse input for mouse Y times Y sensitivity
                _rotationY += Input.GetAxis("Mouse Y") * sensitivity.y;
                //the rotation Y is clamped using mathf and we are clamping the y rotation to the y min and y max
                _rotationY = Mathf.Clamp(_rotationY, rotationRangeY.x, rotationRangeY.y);
                //transforms our local euler angle to the next vector3 rotation -rotationy on the x axis
                transform.localEulerAngles = new Vector3(-_rotationY, 0, 0);
            }
            #endregion
        }
    }
}
