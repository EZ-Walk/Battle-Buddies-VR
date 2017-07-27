﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour {

    [Header("Enemies Killed")]
    public GameObject enemiesKilledCounter;
    public GameObject rangerUIEnemiesKilledCounter;

    [Header("Wave Counter")]
    public GameObject waveCounter;
    public GameObject rangerUIWaveCounter;

    [Header("Structure Health")]
    public GameObject end;
    public GameObject enemy;
    public GameObject healthCounter;
    public GameObject rangerUIHealthCounter;
    public GameObject info;

    [Header("Shop")]
    public GameObject Gold;
    public GameObject rangerUIGold;
    public GameObject ElectricGel;
    public GameObject Metal;

    private int score;
    private int currentWave;
    private int health = 100;
    public static int gold = 100;
    public static int electricGel = 100;
    public static int metal = 100;

    [Header("Ready Up System")]
    private bool currentlyReady = false;
    public static bool overseerReady = false;
    public static bool rangerReady = false;
    public GameObject overseerReadyIndicator;
    //false is not ready, true is ready
    public GameObject rangerUIOverseerReadyIndicator;
    public GameObject rangerReadyIndicator;
    public GameObject rangerUIRangerReadyIndicator;

    [Header("Audio Sources")]
    public AudioSource readyUpSE;
    public AudioSource waveStart;
    public AudioSource enemyDestroyed;
    public AudioSource turretPlaced;
    public AudioSource turretDestroyed;
    public AudioSource arrowFired;
    public AudioSource enemyReachesEndSE;
    public AudioSource gameOver;
    public AudioSource unreadySE;

    [Header("Movement Mode")]
    public bool isWalkingPrefered;


    private void Start()
    {
        clearInfo();
        unreadyOverseer();
        unreadyRanger();
        VRTK.Examples.Archery.BowAim.powerMultiplier = 30;
    }

    private void Update()
    {
        enemiesKilledCounter.GetComponent<TextMeshProUGUI>().text = score + "";
        rangerUIEnemiesKilledCounter.GetComponent<TextMeshProUGUI>().text = score + "";

        waveCounter.GetComponent<TextMeshProUGUI>().text = currentWave + "";
        rangerUIWaveCounter.GetComponent<TextMeshProUGUI>().text = currentWave + "";

        healthCounter.GetComponent<TextMeshProUGUI>().text = health + "";
        rangerUIHealthCounter.GetComponent<TextMeshProUGUI>().text = health + "";

        Gold.GetComponent<TextMeshProUGUI>().text = gold + "";
        rangerUIGold.GetComponent<TextMeshProUGUI>().text = gold + "";

        ElectricGel.GetComponent<TextMeshProUGUI>().text = electricGel + "";
        Metal.GetComponent<TextMeshProUGUI>().text = metal + "";

        overseerReadyIndicator.GetComponent<TextMeshProUGUI>().text = overseerReady + "";
        rangerUIOverseerReadyIndicator.GetComponent<TextMeshProUGUI>().text = overseerReady + "";

        rangerReadyIndicator.GetComponent<TextMeshProUGUI>().text = rangerReady + "";
        rangerUIRangerReadyIndicator.GetComponent<TextMeshProUGUI>().text = rangerReady + "";
    }


    public void increaseEnemiesKilled ()
    {
        score++;
    }

    public void increaseWaveCounter ()
    {
        currentWave++;
    }

    public void enemyReachesEnd ()
    {
        health = health - 5;
        enemyReachesEndSE.Play();

        if (health <= 0)
        {
            gameOver.Play();
        }
    }

    public void builtTurret ()
    {
        gold = gold - shop.costOfCurrentTurret;
        Debug.Log(shop.costOfCurrentTurret);
        metal -= shop.metalCostOfCurrentTurret;
        electricGel -= shop.electricGelCostOfCurrentTurret;
    }

    public void notEnoughGold()
    {
        info.GetComponent<TextMeshProUGUI>().text = "You don't have the required resources!";
    }

    public IEnumerator clearInfo()
    {
        yield return new WaitForSeconds(5f);
        info.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void toggleReadyOverseer ()
    {
        if (currentlyReady)
        {
            unreadyOverseer();
            currentlyReady = false;
        }
        else if (!currentlyReady)
        {
            readyOverseer();
            currentlyReady = true;
        }
    }

    void readyOverseer()
    {
        overseerReady = true;
        readyUpSE.Play();
    }

    void unreadyOverseer()
    {
        overseerReady = false;
        unreadySE.Play();
    }

    public void readyRanger()
    {
        rangerReady = true;
        readyUpSE.Play();
    }

    public void unreadyRanger ()
    {
        rangerReady = false;
        unreadySE.Play();
    }

    public void tryToStartWithoutReady ()
    {
        info.GetComponent<TextMeshProUGUI>().text = "Both players need to ready up before you can start the wave!";
        clearInfo();
    }

    public void buyBowV2()
    {
        if (gold >= 200)
        {
            gold -= 200;
            VRTK.Examples.Archery.BowAim.powerMultiplier = 40;
        }
        else
        {
            info.GetComponent<TextMeshProUGUI>().text = "you don't have enough gold!";
            clearInfo();
        }
    }

    public void buyBowV3()
    {
        if (gold >= 500)
        {
            gold -= 500;
            VRTK.Examples.Archery.BowAim.powerMultiplier = 50;
            Debug.Log("power mult is " + VRTK.Examples.Archery.BowAim.powerMultiplier);
        }
        else
        {
            info.GetComponent<TextMeshProUGUI>().text = "you don't have enough gold!";
            clearInfo();
        }
    }
}
