using System;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static float delayToSpawnFood = 2f;
    public static float delayToDestroyFood = 5f;
    public static float lives = 100f;

    public TextMeshProUGUI timeText;
    public Slider lifeSlider;

    private float timeElapsed = 0f;
    private string formattedTime;

    public RandomFood randomFood;

    public Player player;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        formattedTime = TimeSpan.FromSeconds(timeElapsed).ToString(@"mm\:ss");
        timeText.text = formattedTime;
        lifeSlider.value = lives;
        TimeEvents();
        lives -= Time.deltaTime * 2;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        randomFood.StartFoodGeneration(); // MOD: agora guarda refer�ncia e pode ser parado

    }

    public void TimeEvents()
    {
        if (timeElapsed >= 90f)
        {
            player.moveSpeed = 11f;
            delayToSpawnFood = 0.5f;
        } else if (timeElapsed >= 60f)
        {
            player.moveSpeed = 9f;
            delayToSpawnFood = 1f;
        } else if (timeElapsed >= 30f)
        {
            player.moveSpeed = 7f;
            delayToSpawnFood = 1.5f;
        }
    }
}
