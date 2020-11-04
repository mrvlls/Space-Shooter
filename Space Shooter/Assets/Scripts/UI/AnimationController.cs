using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animation anim;

    void Start()
    {
        anim = this.GetComponent<Animation>();
    }
    
    // Анимация спокойствия.
    public void PlayIdle()
    {
        anim.Play(anim.name + "-Idle");
    }

    // Анимация открытия окна.
    public void OpenWindow()
    {
        anim.Play("Window-In");
    }

    // Анимация закрытия окна.
    public void CloseWindow()
    {
        anim.Play("Window-Out");        
    }
}
