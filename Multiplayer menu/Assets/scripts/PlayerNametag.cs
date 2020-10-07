using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;


public class PlayerNametag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI PlayerNameLeft = null;
    [SerializeField] public TextMeshProUGUI PlayerNameRight = null;
    private GameObject scoreboardNames;
    public bool playerNameLeftIsTaken = false;

    public void Start()
    {   
        scoreboardNames = GameObject.Find("ScoreUI").transform.Find("PlayerNames").gameObject;
        PlayerNameLeft = scoreboardNames.transform.Find("PlayerNameLeft").GetComponent<TextMeshProUGUI>();
        PlayerNameRight = scoreboardNames.transform.Find("PlayerNameRight").GetComponent<TextMeshProUGUI>();
        string thisPLayerName = photonView.Owner.NickName;

        SetName();

        if (photonView.IsMine)
        {
            if (!playerNameLeftIsTaken)
            {
                this.photonView.RPC("SetPlayerLeftName", RpcTarget.All, new object[]{thisPLayerName});
            }
            if (photonView.IsMine)
            {
                this.photonView.RPC("SetPlayerRightName", RpcTarget.All, new object[]{thisPLayerName});
            }
        }
    }

    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;
    }

    [PunRPC]
    void SetPlayerLeftName(string PlayerNameFirst)
    {
        PlayerNameLeft.text = PlayerNameFirst;
        playerNameLeftIsTaken = true;
    }

    [PunRPC]
    void SetPlayerRightName(string PlayerNameSecond)
    {
        PlayerNameRight.text = PlayerNameSecond;
    }
}
