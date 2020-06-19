using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Stats
{
    #region Variables
    [Header("Character Data")]
    public new string name;
    [Header("Movement Variables")]
    [HideInInspector]
    public float speed = 5f;
    public float crouch = 2.5f, sprint = 10f, jumpSpeed = 8f;
    #endregion
}
