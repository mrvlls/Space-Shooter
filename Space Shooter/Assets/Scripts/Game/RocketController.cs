using System.Collections;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Range(0, 9.5f)]
    public float speed = 8.0f;
    [Range(0, 2.0f)]
    public float changeLanesSpeed = 2.0f;

    public Transform parts;

    public GameObject crashedParticles;

    public GameObject bullet;
    
    private int lane = 0;
    private bool changingLanes;
    private float duration;
    private Vector3 startPos, endPos;
    private bool paused;

    private Animation anim;
    private AudioSource audioSource;

    void Start()
    {
        anim = this.GetComponent<Animation>();
        audioSource = this.GetComponent<AudioSource>();
        // Обнуляем позицию ракеты.
        UpdatePosition();
        // Устанавливаем скорость препятствий.
        UpdateObstaclesSpeed(speed);
    }

    void Update()
    {
        // Если игра не на паузе.
        if(!paused)
        {
            // Если игрок не на endPos.
            if(transform.position != endPos)
            {
                // Тогда игрок меняет полосы.
                changingLanes = true;
            }
            else
            {
                // Или игрок уже поменял полосу.
                changingLanes = false;
            }

            // Если игрок нажимает A или Левую стрелку.
            if ((Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow))) 
            {
                // Двигаем игрока влево.
                MoveLeft();
            }

            // Если игрок нажимает D или Правую стрелку.
            if ((Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))) 
            {
                // Двигаем игрока вправо.
                MoveRight();
            }

            // Если игрок нажимает левую кнопку мыши или пробел.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            {
                Shoot(); //Стрельба
            }

            // Если игрок меняет полосу.
            if (changingLanes)
            {
                // Скорость игрока больше 0.
                if(changeLanesSpeed != 0)
                {
                    // Сколько осталось до смены полосы.
                    duration += Time.deltaTime/((2-changeLanesSpeed)/10);
                    // Двигаем игрока на endPos.
                    transform.position = Vector3.Lerp(startPos, endPos, duration);
                }
            }
        }
    }

    // Игрок нажал клавишу влево.
    public void MoveLeft()
    {
        // Если игрок не на первой полос.
        if(lane > -2)
        {
            // Проигрываем анимацию движения влево.
            anim.Play("Move-Left");
            // Проигрываем звук передвижения.
            audioSource.Play();
            // Меняем полосу и обновляем позицию.
            lane--;
            UpdatePosition();             
        }       
    }

    // Игрок нажал клавишу вправо.
    public void MoveRight()
    {
        // Если игрок не на последней полосе.
        if(lane < 2)
        {
            // Проигрываем анимацию движения вправо.
            anim.Play("Move-Right");
            // Проигрываем звук передвижения.
            audioSource.Play();
            // Меняем полосу и обновляем позицию.
            lane++;
            UpdatePosition();   
        }     
    }

    // Обновляем позицию игрока.
    private void UpdatePosition()
    {
        // Начинаем сначала.
        duration = 0;
        // Устанавливаем начальную позицию.
        startPos = transform.position;
        // Устанавливаем конечную позицию.
        endPos = new Vector3(lane, transform.position.y, transform.position.z);
    }

    // Обноволяем скорость астероидов.
    private void UpdateObstaclesSpeed(float obstaclesSpeed)
    {
        ObstaclesLine.speed = obstaclesSpeed;
    }

    // Пауза контроллера игрока.
    public void Pause()
    {
        paused = true;
        // Скорость препятствий 0.
        UpdateObstaclesSpeed(0);
    }

    // Включаем контроллер игрока.
    public void Resume()
    {
        paused = false;
        // Обновляем скорость препятствий.
        UpdateObstaclesSpeed(speed);
    }

    // Если игрок столкнулся с препятсвием.
    public void Crashed()
    {
        // Ставим паузу.
        Pause();
        // Включаем систему частиц
        crashedParticles.SetActive(true);
    }

    //Стрельба игрока
    void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, new Vector2(bullet.transform.position.x, bullet.transform.position.y), transform.rotation);
        bulletClone.SetActive(true);
        bulletClone.GetComponent<Bullet>().KillOldBullet();
        bulletClone.GetComponent<Rigidbody2D>().AddForce(transform.up * 350);
    }
}
