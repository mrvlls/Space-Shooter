using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public int continuePrice;

    public RocketController rocketController;
    public GameObject menu;
    public TextMeshProUGUI priceText;
    public Animation notEnough;
    public LevelLoader levelLoader;
    [HideInInspector]
    public bool crashed;

    public static GameOver instance;

    private Animation anim;
    private int levelNum;

    void Start()
    {
        instance = this;
        anim = this.GetComponent<Animation>();
        priceText.text = continuePrice.ToString();
        crashed = false;
        levelNum = PlayerPrefs.GetInt("currentLevel");
    }

    // Когда игрок врезается в препятствие.
    public void Crashed()
    {
        crashed = true;
        rocketController.Crashed();
        // Проигрываем анимацию открытия окна GameOver.
        anim.Play("Game-Over-In");
        // Отключаем объект игрового меня со всеми кнопками.
        menu.SetActive(false);
    }

    // Если игрок выбирает продолжить иру (за монетки).
    public void Continue()
    {
        // Если у игрока достаточно монеток, чтобы продолжить.
        if(Wallet.GetAmount() >= continuePrice)
        {
            // Вычитаем сумму из кошелька игрока.
            Wallet.SetAmount(Wallet.GetAmount() - continuePrice);
            // Отмечаем что игра продолжается.
            Score.continueGame = true;
            // Загружаем сцену.
            levelLoader.LoadLevel(levelNum);
        }
        else
        {
            //Проигрываем анимацию "Недостаточно монеток".
            notEnough.Play("Not-Enough-In");
        }
    }
}
