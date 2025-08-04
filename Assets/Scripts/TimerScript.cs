using System.Diagnostics;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    public Stopwatch playerTimer;
    public Stopwatch enemyTimer;
    public TextMeshProUGUI timerTextPlayer;
    public TextMeshProUGUI timerTextEnemy;
    public bool isEnemyPlaying;
    public bool isPlayerPlaying;

    public void Start()
    {
        playerTimer = new Stopwatch();
        enemyTimer = new Stopwatch();
    }

    public void GoPlayerTimer()
    {
        playerTimer.Restart();
    }

    public void GoEnemyTimer()
    {
        enemyTimer.Restart();
    }



    public void StopPlayerTimer()
    {
        playerTimer.Stop();
    }

    public void StopEnemyTimer()
    {
        enemyTimer.Stop();
    }

    public void Update()
    {
        if (isEnemyPlaying || isPlayerPlaying)
        {
            updateText();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            if (isPlayerPlaying)
            {
                isPlayerPlaying = false;
                StopPlayerTimer();
            }
            else
            {
                isPlayerPlaying = true;
                GoPlayerTimer();
            }
        }
        if (other.CompareTag("EnemyCar"))
        {
            if (isEnemyPlaying)
            {
                isEnemyPlaying = false;
                StopEnemyTimer();
            }
            else
            {
                isEnemyPlaying = true;
                GoEnemyTimer();
            }
        }
    }
    void updateText()
    {
        timerTextEnemy.text = enemyTimer.Elapsed.Minutes + ":" + enemyTimer.Elapsed.Seconds + ":" + enemyTimer.Elapsed.Milliseconds;
        timerTextPlayer.text = playerTimer.Elapsed.Minutes + ":" + playerTimer.Elapsed.Seconds + ":" + playerTimer.Elapsed.Milliseconds;
    }
}
