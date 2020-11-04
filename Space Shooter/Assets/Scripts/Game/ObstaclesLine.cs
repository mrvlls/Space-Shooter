using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesLine : MonoBehaviour
{
    public static float speed;
    public int lineCount;

    private List<GameObject> obstacles;
    private GameObject coin;

    private float duration;
    private Vector3 startPos, endPos;
    private bool newLineSpawned;

    void Awake()
    {
        // Устанавливаем позицию новой линии на верх экрана перед всем остальным.
        transform.position = new Vector3(transform.position.x, 7, transform.position.z);



    }

    void Start()
    {
        // Загружаем астероиды из ресурсов.
        LoadObstacles();
        // Загружаем монетку из ресурсов.
        LoadCoin();
        // Спацним астероиды и монетку.
        SpawnLineOfObstacles();
        // Получаем начальную позицию линии.
        startPos = transform.position;
        // Усанавливаем конечную позицию линии.
        endPos = new Vector3(transform.position.x, -7, transform.position.z);
    }

    void Update()
    {

        // Если линия еще не достигла конечной позии.
        if (transform.position != endPos)
        {
            // Если скорость игрока больше чем 0.
            if (speed != 0)
            {
                // Сколько линия будет двигаться к низу экрана.
                duration += Time.deltaTime / (10 - speed);
                // Двигаем линию к низу экрана.
                transform.position = Vector3.Lerp(startPos, endPos, duration);

                // Сколько линии осталось двигаться чтобы спавнить новую линию.
                if (!newLineSpawned && duration > ObstacleLineSpawner.instance.spawnPlace)
                {

                    // Спавним новую линию.
                    ObstacleLineSpawner.instance.SpawnLine();
                    newLineSpawned = true;

                }
            }
        }
        else
        {
            // Уничтожаем лини когда она достигла конечной позиции.
            Destroy(gameObject);
        }

    }

    // Загружаем астероиды из ресурсов.
    private void LoadObstacles()
    {
        obstacles = new List<GameObject>();
        Object[] objects = Resources.LoadAll("Obstacles") as Object[];
        foreach (Object item in objects)
        {
            obstacles.Add(item as GameObject);
        }
    }

    // Загружаем монетку из ресурсов.
    private void LoadCoin()
    {
        coin = Resources.Load("Coin") as GameObject;
    }

    // Спавним астероид в одну из пяти полос.
    private void SpawnObstacle(int lane)
    {
        int randomObstacleIndex = Random.Range(0, obstacles.Count);
        float randomObstacleOffest = Random.Range(0, ObstacleLineSpawner.instance.randomizeObstaclesOffest);

        Instantiate(obstacles[randomObstacleIndex], new Vector3(lane, 7 + randomObstacleOffest, 0), Quaternion.identity, transform);
    }

    // Спавним монетку в одну из пяти полос.
    private void SpawnCoin(int lane)
    {
        float randomCoinOffest = Random.Range(0, ObstacleLineSpawner.instance.randomizeObstaclesOffest);

        Instantiate(coin, new Vector3(lane, 7 + randomCoinOffest, 0), Quaternion.identity, transform);
    }

    // Спавним объекты в линию.
    private void SpawnLineOfObstacles()
    {
        int minObstacles = ObstacleLineSpawner.instance.minObstacles;
        int maxObstacles = ObstacleLineSpawner.instance.maxObstacles;

        // Получаем рандомное количество препятствий для спавна.
        int obstaclesAmount = Random.Range(minObstacles, maxObstacles);
        // Получаем доступные полосы.
        List<int> availableLanes = new List<int>() {-2, -1, 0, 1, 2};
        for(int i = 0; i < obstaclesAmount; i++)
        {
            // Рандомный индекс полосы.
            int randomLaneIndex = Random.Range(0, availableLanes.Count);
            // Спавним объект.
            SpawnObstacle(availableLanes[randomLaneIndex]);
            // Убираем полосу, в которую спавнили объект, из доступных полос.
            availableLanes.RemoveAt(randomLaneIndex);
        }

        // Проверяем нужно ли спавнить монетку.
        if(Random.value < ObstacleLineSpawner.instance.coinSpawnRate)
        {
            //Случайный доступный для спавна индекс полосы.
            int randomLaneIndex = Random.Range(0, availableLanes.Count);
            // Спавним монетку на доступный полосу.
            SpawnCoin(availableLanes[randomLaneIndex]);
        }
    }
}
