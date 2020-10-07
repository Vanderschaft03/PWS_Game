using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;

public class PlayerShooting : MonoBehaviourPun
{
    public Transform firePoint;
    public int damage = 1; 
    public GameObject impactEffect;
    public LineRenderer lineRenderer;
    public int x;
    public int pointAmount = 1;
    public GameObject player;

    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {   
            this.photonView.RPC("ShootLine", RpcTarget.All);
            damagePlayer();
            Debug.Log("button");
        }
    }

    
    [PunRPC]
    IEnumerator ShootLine()
    {
        RaycastHit2D lineInfo = Physics2D.Raycast(firePoint.position, firePoint.right); 

        if (lineInfo)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, lineInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true; 

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false; 

        Debug.Log("activate");
    }

    
    public void damagePlayer()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right); 

        PhotonView pView = hitInfo.transform.GetComponent<PhotonView>();

        Debug.Log(pView);

        if(hitInfo.transform.CompareTag("Player"))
        {
            pView.RPC("applyDamage", RpcTarget.All);

            Debug.Log("hitdamage");

            if(hitInfo.transform.GetComponent<PlayerHealth>().health == -10)
            {
                Debug.Log("well0hp");
                player.GetComponent<PlayerHealth>().OwnScorePoints += 1;
            }

            //hitInfo.transform.gameObject.GetComponent<PlayerHealth>().health -= damage;

            
            //PhotonView pView = hitInfo.transform.GetComponent<PhotonView>();
            //Debug.Log(pView);

            //hitInfo.transform.gameObject.GetComponent<PlayerHealth>().health -= damage;
            
            //if(pView)
            //{
            //   pView.RPC("ApplyDamage", RpcTarget.All);
            //}
        }
    }
}
