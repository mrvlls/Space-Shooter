using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Sprite soundOn, soundOff;
    public Image soundImage;
    public GameObject settingsMenu;

    void Start()
    {
        // Устанавливаем настройки в начале игры.
        SetSounds();
    }

    //Смена настроек звука.
    public void ChangeSounds()
    {
        // Получаем состояние звука.
        bool active = GetSetting("Sounds");
        
        // Если звук включен
        if(active)
        {
            // Отключаем зву в игре.
            soundImage.sprite = soundOff;
            AudioListener.volume = 0.0f;
            ChangeSetting("Sounds", 0);
        }
        else
        {
            // Включаем звук в игре.
            soundImage.sprite = soundOn;
            AudioListener.volume = 1.0f;
            ChangeSetting("Sounds", 1);
        }
    }

    // Обнуляем прогресс в игре.
    public void Reset()
    {
        // Удаляем все PlayerPrefs в игре.
        PlayerPrefs.DeleteAll();
    }

    // Получаем значения настроек.
    public static bool GetSetting(string name)
    {
        return PlayerPrefs.GetInt(name, 1) == 1 ? true : false;
    }

    // Изменяем настройки.
    private void ChangeSetting(string name, int state)
    {
        PlayerPrefs.SetInt(name, state);
    }

    // Устанавливаем настройки звука в начале игры.
    private void SetSounds()
    {
        // Плучаем состояние звука.
        bool active = GetSetting("Sounds");
        
        // Если звук включен.
        if(active)
        {
            // Включаем звук.
            soundImage.sprite = soundOn;
            AudioListener.volume = 1.0f;
        }
        else
        {
            // Выключаем звук.
            soundImage.sprite = soundOff;
            AudioListener.volume = 0.0f;
        }
    }

    public void SettingsMenuOpen() //Открываем окно настроек.
    {
        settingsMenu.SetActive(true);
    }

    public void SettingsMenuClose()  // Закрываем окно настроек.
    {
        settingsMenu.SetActive(false);
    }


}
