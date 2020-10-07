using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

namespace MultiplayerMovement
{
    public class PlayerFlip : MonoBehaviourPun
    {
        public bool facingRight;
        public Transform player;
        public Transform gun;

        void Update()
        {
            if(photonView.IsMine)
            {
                UpdateFlip();
            }
        }

        private void UpdateFlip()
        {
            Vector2 pointGun = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.position;
            float gunAngle = Mathf.Atan2(pointGun.y, pointGun.x) * Mathf.Rad2Deg;
            gun.rotation = Quaternion.Euler(0f, 0f, gunAngle);

            Vector2 pointDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
            float angle = Mathf.Atan2(pointDir.y, pointDir.x) * Mathf.Rad2Deg;

            if((angle > 90f || angle < -90f) && facingRight)
            {
               player.Rotate (0f, 180f, 0f);
               facingRight = !facingRight;
            }
            else if(((angle > 0f && angle < 90f) || (angle < 0f && angle > -90f)) && !facingRight)
            {
                player.Rotate (0f, 180f, 0f);
                facingRight = !facingRight;
            }
        }
    }
}