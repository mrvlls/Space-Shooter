using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleLineSpawner : MonoBehaviour
{
    [Range(0, 4)]
    public int minObstacles, maxObstacles;
    [Range(0.2f, 0.8f)]
    public float spawnPlace;
    [Range(0, 1.5f)]
    public float randomizeObstaclesOffest;
    [Range(0, 1.0f)]
    public float coinSpawnRate;

    public static ObstacleLineSpawner instance;

    private int lineIndex;

    private int levelNum;

    void Start()
    {
        instance = this;
        // Спауним линию когда игра началась.
        SpawnLine();
        levelNum = PlayerPrefs.GetInt("currentLevel");
    }

    // Используется ля спавна линии.
    public void SpawnLine()
    {
        while (lineIndex < PlayerPrefs.GetInt($"level{levelNum}lines", 1))
        {
            // Создаем объект новой линии.
            GameObject line = new GameObject("Line-" + lineIndex);
            // Делаем линию "ребенком" объекта.
            line.transform.parent = transform;
            // Добавляем ObstaclesLine компонент к объекту.
            line.AddComponent<ObstaclesLine>();
            // Начинаем считать когда игрок достиг первой линии.
            if (lineIndex > 1)
            {
                // Увеличиваем очки.
                IncreaseScore();
            }
            // Увеличиваем индексацию линий.
            lineIndex++;


            if (lineIndex > 1 && Score.GetAmount() == (PlayerPrefs.GetInt($"level{levelNum}lines", 1) - 2))
            {
                if (PlayerPrefs.GetInt("maxLevel", 1) < (levelNum + 1))
                    PlayerPrefs.SetInt("maxLevel", levelNum + 1); // Устанавливаем максимальный открытый уровень.
                PlayerPrefs.SetInt("currentLevel", levelNum + 1);
                Invoke("LoadMenu", 4);
                break;
            }
            break;
            
        }


    }

    // Увеличиваем очки на одно.
    private void IncreaseScore()
    {
        Score.SetAmount(Score.GetAmount() + 1);
    }

    // Загружаем меню.
    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
