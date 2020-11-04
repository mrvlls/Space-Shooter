using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;
    private bool crashed;
    private AudioSource audioSource;

    void Start()
    {
        // Рандомно устанавливаем скорость вращения.
        speed = Random.Range(-1.0f, 1.0f);
        audioSource = this.GetComponent<AudioSource>();
        crashed = false;
    }

    void Update()
    {
        // Вращаем астероид.
        transform.Rotate(0, 0, speed, Space.Self); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Проверяем не столкнулся ли он до этого.
        if(!crashed)
        {
            // Проверяем столкнулся ли объект с одним из коллайдеров игрока.
            if(col.name == "Front" || col.name == "Back")
            {
                // Проигрываем звук столкновения.
                audioSource.Play();
                // Обнуляем скорость игрока.
                speed = 0;
                // Открываем окно Game Over.
                GameOver.instance.Crashed();
                // Устанавливаем индикатор столкновения, чтобы функция не сработала еще раз.
                crashed = true;
            }

            if (col.name == "Bullet(Clone)")
            {
                //ParticleSystem explosion = Instantiate(smallExplosion, transform.position, transform.rotation);
                //explosionSound = smallExplosion.GetComponent<AudioSource>();
                //explosion.Play();
                //explosionSound.Play();
                //Destroy(explosion, explosion.duration);
                Destroy(gameObject);
                Destroy(col.gameObject);
            }
        }
    }

}
