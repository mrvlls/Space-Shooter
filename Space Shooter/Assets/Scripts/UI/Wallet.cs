using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    private static int amount;
    private static TextMeshProUGUI walletText;

    void Start()
    {
        amount = PlayerPrefs.GetInt("WalletAmount", 0);
        walletText = this.GetComponent<TextMeshProUGUI>();
        DisplayAmount();
    }

    // Получаем кол-во монет в кошельке.
    public static int GetAmount()
    {
        return amount;
    }

    // Устанавливаем кол-во монет.
    public static void SetAmount(int amountToSet)
    {
        amount = amountToSet;
        DisplayAmount();
        PlayerPrefs.SetInt("WalletAmount", amount);
    }

    // Показываем кол-во монет на экране.
    private static void DisplayAmount()
    {
        walletText.text = amount.ToString();
    }
}
