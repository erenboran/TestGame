using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public event Action OnBuy;

    public Image currentDayBar,wallHealthBar;

    [SerializeField]
    WaveSpawner waveSpawner;

    [SerializeField]

    Text currentDayText, nextDayText, nextDay2Text;

    public int money;

    public int day;

    public Player player;

    public Text fenceCountText, diamondText,moneyText, deadEnemyText, deadEnemyTextGlobal;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        deadEnemyText.text = waveSpawner.totaldeadEnemyCount.ToString();
        deadEnemyTextGlobal.text = waveSpawner.totaldeadEnemyCount.ToString();
        currentDayBar.fillAmount = (float)waveSpawner.deadEnemyCount/ waveSpawner.dailyEnemyCount;
        currentDayText.text = GameManager.instance.day.ToString();
        nextDayText.text = (GameManager.instance.day + 1).ToString();
        nextDay2Text.text = (GameManager.instance.day + 2).ToString();
    //    fenceCountText.text=ResourceManager.instance.fenceCount.ToString();
    //    diamondText.text = ResourceManager.instance.diamondCount.ToString();
     //   wallHealthBar.fillAmount = Wall.instance.currentWallHealth / Wall.instance.maxWallHealth;
        moneyText.text = money.ToString();
    }

}
