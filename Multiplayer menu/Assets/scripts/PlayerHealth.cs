using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPun
{
    public Slider slider;

    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private TextMeshProUGUI healthBarDisplay;

    [SerializeField] private TextMeshProUGUI OwnScore = null;
    [SerializeField] private TextMeshProUGUI EnemyScore = null;
    public GameObject scoreboardScores;

    public int OwnScorePoints = 0;
    public int EnemyScorePoints = 0;

    public int health = 100;
    public int damage = 10;
    public GameObject player;
    public Transform playerPosition;

    void Start()
    {
        scoreboardScores = GameObject.Find("ScoreUI").transform.Find("PlayerScores").gameObject;
        OwnScore = scoreboardScores.transform.Find("ScoreOwn").GetComponent<TextMeshProUGUI>();
        EnemyScore = scoreboardScores.transform.Find("ScoreEnemy").GetComponent<TextMeshProUGUI>();
    }


    public void Update()
    {
        if(photonView.IsMine)
        {
            OwnScore.text = string.Empty + OwnScorePoints;
            EnemyScore.text = string.Empty + EnemyScorePoints;

            this.photonView.RPC("UpdateHealthBar", RpcTarget.All);

            if(health < 0)
            {
                EnemyScorePoints += 1;
                this.photonView.RPC("PlayerEliminated", RpcTarget.All);
            }
        }

        if(playerPosition.position.y > -10)
        {
            PlayerEliminated();
        }
    }
    
    [PunRPC]
    void UpdateHealthBar()
    {
        string healthBar = string.Empty + health;
        slider.value = health;
        healthBarDisplay.text = healthBar;
    }

    [PunRPC]
    void applyDamage()
    {
        health -= damage;
    }

    [PunRPC]
    IEnumerator PlayerEliminated()
    {
        player.SetActive (false);
        Vector2 respawnPoint = new Vector2(0, 2);
        playerPosition.position = respawnPoint;
        health = 100;
        yield return new WaitForSeconds(3f);
        player.SetActive (true);
    }

    public void AddPointToSelf()
    {
        OwnScorePoints++;
    }
}
