using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] Image healthBar;

    [SerializeField]
    JoystickPlayerExample joyStick;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject camera;

    public GameObject GameOverUI, InGameCanvas;

    public bool hasPlayedDeathSound = false;
    public float health;
    public AudioSource DeadSound;

    private void Update()
    {
        healthBar.fillAmount = health / 10.0f;

        if (health <= 0)
        {
            OlumSesiniCal();
            InGameCanvas.SetActive(false);
            GameOverUI.SetActive(true);
            StopGame(); // Zaman ölçeðini durdurmak için StopGame fonksiyonunu çaðýr

            //Time.timeScale = 0;
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }

    public void OlumSesiniCal()
    {
        if (!hasPlayedDeathSound)
        {
            DeadSound.Play();
            hasPlayedDeathSound = true;
        }
    }

    public void ContuineButton()
    {
        health += 10;
        Debug.Log("old");
        GameOverUI.SetActive(false);
        Time.timeScale = 1;
        InGameCanvas.SetActive(true);
        hasPlayedDeathSound = false;
    }
}