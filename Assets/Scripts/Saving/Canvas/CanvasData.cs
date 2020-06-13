using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CanvasData
{
    #region Variables
    public int skinIndex, eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex; //Index values for the customisation
    public string playerName; //The name of the player
    public string playerClass; //The class the player chose
    public string playerAbility; //The ability of thier chosen class
    #endregion

    public CanvasData(CanvasSet canvas)
    {
        //Set the variables to the corresponding ones wihtin the passed parameter
        skinIndex = canvas.skinIndex; //Skin Index
        eyesIndex = canvas.eyesIndex; //Eyes Index
        mouthIndex = canvas.mouthIndex; //Mouth Index
        hairIndex = canvas.hairIndex; //Hair Index
        armourIndex = canvas.armourIndex; //Armour Index
        clothesIndex = canvas.clothesIndex; //Clothes Index

        playerName = canvas.characterName; //Name
        playerClass = canvas.canvasCharClass.ToString(); //Class
        playerAbility = canvas.characterAbility.ToString(); //Ability
    }
}
