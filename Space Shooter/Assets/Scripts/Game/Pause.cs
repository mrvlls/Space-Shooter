using UnityEngine;

public class Pause : MonoBehaviour
{
    public RocketController rocketController;


    private AnimationController animationController;

    void Start()
    {
        animationController = this.GetComponent<AnimationController>();
    }

    // Когда игрок нажал паузу.
    public void PauseGame()
    {
        // Останавливаем контроллер игрока.
        rocketController.Pause();
        // Открываем окно паузы.
        animationController.OpenWindow();
    }

    // Когда игрок закрывает окно паузы.
    public void ResumeGame()
    {
        // Включаем контроллер игрока.
        rocketController.Resume();
        // Закрываем окно паузы.
        animationController.CloseWindow();
    }

}
