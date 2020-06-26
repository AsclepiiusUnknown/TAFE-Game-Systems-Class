using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogueArray : MonoBehaviour
{
    public string characterName;

    //*MAKE THEM THE SAME LENGTH
    public string[] extraPosDlgText;
    public string[] posDlgText;
    public string[] neuDlgText;
    public string[] negDlgText;
    public string[] extraNegDlgText;
    public int indexOption;
    public int approval;

    void Start()
    {

    }
}
