using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public bool openDoors = true;

    public LoadBar loadBar;

    private Animation anim;

    public int levelNum;

    public Button[] levelButtons;
    public Image[] levelImages;

    void Start()
    {
        anim = this.GetComponent<Animation>();

        // Проверяем нужно ли открывать двери и играть анимацию.
        if (openDoors)
        {
            anim.Play("OpenDoors");
        }

        
    }

    private void Awake() // Каждую загрузку сцены проверяем максимально открытый уровень (активируем его и все уровни до него).
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("maxLevel", 1); i++) 
            {
                levelButtons[i].enabled = true;
                levelImages[i].color = Color.white;
            }
        }
    }



    // Загружаем следующий уровеньы.
    public void LoadLevel(int sceneIndex)
    {
      // Играем анимацию закрытия дверей.
      anim.Play("CloseDoors");
        // Загружаем сцену.
      StartCoroutine(LoadLevelAsync(sceneIndex));
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
      // Задержка для анимации закрытия дверей.
      yield return new WaitForSeconds(0.5f);

      // Загружаем сцену.
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

      // Пока загрузка не завершилась.
      while(!operation.isDone)
      {
        // Получаем процесс загрузки.
        float progress = Mathf.Clamp01(operation.progress / 0.9f);
        // Загружаем в LoadBar.
        loadBar.progress = 1 - progress;
        yield return null;
        // Сохраняем вращение LoadBar для следующей сцены.
        loadBar.saveRotation();
      }
    }

    public void SetLines(int levelNum) // Генерируем постоянное количество линий для уровня
    {
        if (PlayerPrefs.GetInt($"level{levelNum}lines", 1) == 1)
        {
            PlayerPrefs.SetInt($"level{levelNum}lines", Random.Range(15, 26));
        }
        this.levelNum = levelNum;
        PlayerPrefs.SetInt("currentLevel", levelNum);
        Debug.Log(PlayerPrefs.GetInt($"level{levelNum}lines", 1));
    }

}
