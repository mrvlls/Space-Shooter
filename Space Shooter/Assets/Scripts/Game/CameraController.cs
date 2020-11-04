using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Камера слдеит за игроком.
    void LateUpdate()
    {
        // Ограничиваем возможное движение камеры границами.
        if(target.position.x < 0 && transform.position.x > -0.25f || target.position.x > 0 && transform.position.x < 0.25f || target.position.x == 0)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
        }
    }
}
