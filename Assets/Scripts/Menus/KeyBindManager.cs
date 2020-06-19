using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    #region Variables
    [SerializeField] //Used to format the dictionary so that Unity can properly read and display it and its contents
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); //Create a dictionary to store the names of our keys and thier associated KeyCode

    [System.Serializable] //Used to format the struct so that Unity can properly read and display it and its contents
    public struct KeyUISetup //Create a public struct containing:
    {
        public string keyName; //String used to store the name of the key (as seen on the label) in a way we can understand
        public Text keyDisplayText; //TMPro GUI element used to display the key we set
        public string defaultKey; //String used to store the name of the default key in a way we can understand
    }

    [Header("Base Keys")]
    public KeyUISetup[] baseSetup; //Array of the elements created above in KeyUISetup
    public GameObject currentKey; //Stores the key we are currently using and ppossibly changing

    [Header("Visual Debugging")]
    public Color32 changedKey = new Color32(39, 171, 249, 255); //Color we change the button to once we have changed the associated key
    public Color32 selectedKey = new Color32(239, 116, 36, 255); //Color we change the button to when its selected

    [Header("Debugging")]
    public static bool hasLoaded = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (keys.Count <= 0)
        {

            //Loop adds the keys to the dictionary (created above) with either save or default (depending on load)
            for (int i = 0; i < baseSetup.Length; i++) //For all the keys in the base setup array
            {
                if (hasLoaded)
                    return;

                //Add keys according to the saved string or default
                keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(baseSetup[i].keyName, baseSetup[i].defaultKey)));

                //Change the display to what the Bind is for each UI Text component
                baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
            }

            hasLoaded = true;
        }
    }

    #region Changing the Keybinds
    //Used to change the passed key object
    public void ChangeKey(GameObject clickedKey) //Parse a key GameObject
    {
        currentKey = clickedKey; //The current key we are accessing is the key that was clicked (passed into the function)
        if (clickedKey != null) //If we have clicked a key and its currently selected
        {
            currentKey.GetComponent<Image>().color = selectedKey; //Create a visual element that shows the user the button was succesfully pressed
        }
    }
    #endregion

    #region Saving Keybinds
    //Used when we want to save the keys and changes
    public void SaveKeys()
    {
        foreach (var key in keys) //For each variable (we call key) within the dictionary of keys
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString()); //Save the key's value as a string to its key
        }
        PlayerPrefs.Save(); //Save the PlayerPrefs once complete
    }
    #endregion

    #region Complete the Key Changing Process Using Events
    //Used beacuse it allows us to run events
    private void OnGUI()
    {
        string newKey = ""; //Create a currently unused string variable
        Event e = Event.current; //Create a reference to the current event

        if (currentKey == null) //Fixes issues for later on by exiting the function when we dont need to use it
            return; //Return (exit) out of the function from this point (not executing the following code)

        if (e.isKey) //If e (the current event) a keyboard event
        {
            newKey = e.keyCode.ToString(); //If so then our string newKey is equal to the keyCode of the key currently being pressed
        }

        //The following fixes an issue with setting the shift keys by hard coding it in
        if (Input.GetKey(KeyCode.LeftShift)) //If the Left Shift key is currently being pressed
        {
            newKey = "LeftShift"; //The new key is the Left Shift
        }
        if (Input.GetKey(KeyCode.RightShift))  //If the Right Shift key is currently being pressed
        {
            newKey = "RightShift"; //The new key is the Right Shift
        }

        if (newKey != "") //If a key has been set this frame (this OnGUI)
        {
            keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey); //Change out the key in the dictionary to the new one we just pressed
            currentKey.GetComponentInChildren<Text>().text = newKey; //Change the display text to match the new key
            currentKey.GetComponent<Image>().color = changedKey; //Change the color to show we have changed the key
            currentKey = null; //Reset the variable and wait until another has been pressed and the cycle repeats
        }
    }
    #endregion
}
