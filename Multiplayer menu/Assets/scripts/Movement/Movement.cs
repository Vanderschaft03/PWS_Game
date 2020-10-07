using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

namespace MultiplayerMovement
{
    public class Movement : MonoBehaviourPun
    {
        public Rigidbody2D rb;
        public Transform groundCheck;
        
        private bool isGrounded;
        public float checkRadius;
        public int multiJump = 0;

        public float movementSpeed;
        public float movementX = 0f;
        public int jumpForce = 350;

        void Update()
        {
            if(photonView.IsMine)
            {
                TakeInput();
            }
        }

        private void TakeInput()
        {   
            movementX = Input.GetAxisRaw("Horizontal") * movementSpeed; 
            Vector2 movement = new Vector2(movementX * 2, rb.velocity.y); 
            rb.velocity = movement;

            if(Input.GetButtonDown("Jump") && multiJump < 5)
            {
                Vector2 movementAfterJump = new Vector2(movementX * 2, 0); 
                rb.velocity = movementAfterJump;
                rb.AddForce(new Vector2(0, jumpForce));
                multiJump++;
            }

            if(isGrounded)
            {
                multiJump = 0;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius);
        }
    }
}