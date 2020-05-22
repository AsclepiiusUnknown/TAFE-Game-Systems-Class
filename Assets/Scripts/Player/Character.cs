using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Stats
{
    #region Variables
    [Header("Character Data")]
    public new string name;
    [Header("Movement Variables")]
    public float speed = 5f;
    public float crouch = 2.5f, sprint = 10f, jumpSpeed = 8f;
    #endregion

    #region Behaviour
    public virtual void Movement()
    {
        //Default movement written here
        Debug.Log("Parent Movement");
    }
    #endregion
    public virtual void Update()
    {
        Movement();
    }
}
