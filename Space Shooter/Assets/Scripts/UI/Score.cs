using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static bool continueGame;
    private static int amount;
    private static TextMeshProUGUI scoreText;

    void Start()
    {
        // Если предыдущая игра не будет продолжена.
        if(!continueGame)
        {
            // Обнуляем очки.
            amount = 0;
        }
        else
        {
            // Оставляем кол-во очков и выключем continueGame bool.
            continueGame = false;
        }

        // Показываем очки.
        scoreText = this.GetComponent<TextMeshProUGUI>();
        DisplayAmount();
    }

    // Получаем кол-во очков.
    public static int GetAmount()
    {
        return amount;
    }

    // Устанавливаем кол-во очков.
    public static void SetAmount(int amountToSet)
    {
        // Устанавливаем новое кол-во очков.
        amount = amountToSet;
        // Показываем новое кол-во очков на экране.
        DisplayAmount();

    }

    // Выводим очки на экран.
    private static void DisplayAmount()
    {
        scoreText.text = amount.ToString();
    }
}
