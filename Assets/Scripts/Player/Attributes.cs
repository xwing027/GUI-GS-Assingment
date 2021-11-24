using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    [Serializable]
    public struct Attribute //these are the parts each quality the player has will need
    {
        public string name;
        public float currentValue;
        public float maxValue;
        public float regenValue;
        public Image displayImage;
    }
    //all creatures start with 3 basic attributes: health, mana and stamina
    public Attribute[] attributes = new Attribute[3];
}
