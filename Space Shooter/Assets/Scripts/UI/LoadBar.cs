using UnityEngine;

public class LoadBar : MonoBehaviour
{
    [Range(0,1)]
    public float progress;

    private static Quaternion lastRotation;

    void Start()
    {
        // Начинаем с такого же вращения как и в предыдущей сцене. 
        transform.rotation = lastRotation;
    }

    void Update()
    {
        // Вращаем круг.
        transform.Rotate(0, 0, progress, Space.Self); 
    }

    // Сохраняем вращение перед следующей сценой.
    public void saveRotation()
    {
        lastRotation = transform.rotation;
    }

}
