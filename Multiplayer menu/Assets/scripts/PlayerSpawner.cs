using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;

namespace Multiplayer
{
    public class PlayerSpawner : MonoBehaviourPun
    {
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private CinemachineVirtualCamera PlayerCamera = null;

        private void Start()
        {
            var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            PlayerCamera.Follow = player.transform;
        }
    }
}