using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

namespace MultiplayerMenu
{
    public class PlayerNameInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button connectButton = null;

        private const string PlayerPrefsNameKey = "Playername";

        private void Start()
        {
            SetupInputfield();
            connectButton.interactable = false;
        }

        private void SetupInputfield()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
            {
                return;
            }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;
        }

        public void Update()
        {
            string name = nameInputField.text;

            if(string.IsNullOrEmpty(name))
            {
                connectButton.interactable = false;
            }
            else
            {
                connectButton.interactable = true;
            }
        }

        public void SavePlayerName()
        {
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
        }
    }
}
