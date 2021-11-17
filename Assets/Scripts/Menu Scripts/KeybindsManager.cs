using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsManager : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    
    [System.Serializable]
    public struct KeyUISetup
    {
        public string keyName;
        public Text keyDisplayText;
        public string defaultKey;
    }

    public KeyUISetup[] baseSetup; //arrary of the info stored in keyuisetup struct
    public GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        //check if dictionary has less or = than 0 in size
        if (keys.Count <=0)
        {
            //if not
            for (int i = 0; i < baseSetup.Length; i++) //for every key in the base set up
            {
                //add a key according to saved string or the default
                keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(baseSetup[i].keyName, baseSetup[i].defaultKey)));

                //display this:
            }
        }
        for (int i = 0; i < baseSetup.Length; i++) //for every key in the base set up
        {
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveKeys()
    {
        //save each key as they are changed
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    public void ChangeKey(GameObject clickedKey) //?
    {
        currentKey = clickedKey;
    }

    public void OnGUI()
    {
        string newKey = "";
        Event e = Event.current;
        if (currentKey != null)
        {
            if (e.isKey)
            {
                newKey = e.keyCode.ToString();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (newKey != "") //if the key isnt empty
            {
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                currentKey.GetComponentInChildren<Text>().text = newKey; //get the key stored and use it
                
                PlayerPrefs.GetString(keys.Values.ToString());
            }
        }
    }


}
