using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PressHandler : MonoBehaviour
{
    public GameObject mainMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            mainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);

            //play animations
        }
    }
}
