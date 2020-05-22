using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Game Systems/RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Physics")]
        public float gravity = 20f;
        public CharacterController controller;
        [Header("Movement Variables")]
        public float speed = 5f;
        public float jumpSpeed = 8f;
        public Vector3 moveDirection;

        void Start()
        {
        //Grabs the Character controller attached to this object
            controller = GetComponent<CharacterController>();
        }
        void Update()
        {
            if(controller.isGrounded)
            {
                moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
                moveDirection *= speed;
                if(Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
