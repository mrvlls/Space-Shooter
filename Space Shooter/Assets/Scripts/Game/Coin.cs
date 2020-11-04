using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animation anim;
    private AudioSource audioSource;
    private bool taken;

    void Start()
    {
        anim = this.GetComponent<Animation>();
        audioSource = this.GetComponent<AudioSource>();
        taken = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Проверяем собрана и монетка. (Потому что игрок может снова столкнуться с монеткой пока проигрывается анимация сбора монетки)
        if(!taken)
        {
            // Проверяем столкновение монетки с одним из коллайдеров игрока.
            if(col.name == "Front" || col.name == "Back")
            {
                // Увеличиваем кол-воо монет в кошельке.
                Wallet.SetAmount(Wallet.GetAmount() + 1);
                // Проигрываем анимацию сбора монетки.
                anim.Play("Coin-Destroy-Down");
                // Проигрываем звук сбора монетки.
                audioSource.Play();
                // Отмечаем монетку собранной.
                taken = true;
            }
        }
    }
}
