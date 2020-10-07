using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;

public class RoundTimer : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI countDown = null;
    [SerializeField] private TextMeshProUGUI winAnnouncement = null;
    public GameObject player;

    private GameObject winAnnouncementColl;
    private GameObject countDownColl;
    public int CountDownTime;
    public bool endRound = false;

    void Start()
    {
        countDownColl = GameObject.Find("Countdown").transform.Find("CountdownTimer").gameObject;
        countDown = countDownColl.transform.Find("CountdownText").GetComponent<TextMeshProUGUI>();

        winAnnouncementColl = GameObject.Find("PlayerWinAnnouncement").transform.Find("WinAnnouncement").gameObject;
        winAnnouncement = winAnnouncementColl.transform.Find("WinAnnouncementText").GetComponent<TextMeshProUGUI>();

        winAnnouncement.text = string.Empty;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while(CountDownTime > 0)
        {
            countDown.text = CountDownTime.ToString();

            yield return new WaitForSeconds(1f);

            CountDownTime--;
        }

        endRound = true;

        EndRoundSequence();
    }

    void Update()
    {
        int ownScore = player.GetComponent<PlayerHealth>().OwnScorePoints;
        int enemyScore = player.GetComponent<PlayerHealth>().EnemyScorePoints;
    }

    void EndRoundSequence()
    {
        if(endRound)
        {
            Debug.Log("timer end");

            if(player.GetComponent<PlayerHealth>().OwnScorePoints > player.GetComponent<PlayerHealth>().EnemyScorePoints)
            {
                winAnnouncement.text = player.GetComponent<PlayerNametag>().PlayerNameLeft.text + " WINS WITH " + player.GetComponent<PlayerHealth>().OwnScorePoints + " points";
                Debug.Log("win");
            }
            else if(player.GetComponent<PlayerHealth>().OwnScorePoints < player.GetComponent<PlayerHealth>().EnemyScorePoints)
            {
                winAnnouncement.text = player.GetComponent<PlayerNametag>().PlayerNameRight.text + " WINS WITH " + player.GetComponent<PlayerHealth>().EnemyScorePoints + " points";
                Debug.Log("loss");
            }
        }
    }
}
