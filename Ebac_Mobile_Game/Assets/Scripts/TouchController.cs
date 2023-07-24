using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 initialTouchPosition;
    private bool isMoving = false;
    public float moveSpeed = 10f; // Define a velocidade de movimento do personagem.
    public float minX; // Defina o valor mínimo da posição X permitida para o personagem.
    public float maxX; // Defina o valor máximo da posição X permitida para o personagem.

    public void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialTouchPosition = touch.position;
                isMoving = true;
            }
            else if (touch.phase == TouchPhase.Moved && isMoving)
            {
                Vector2 touchDelta = touch.deltaPosition;
                float targetX = transform.position.x + touchDelta.x * moveSpeed * Time.deltaTime;
                float clampedX = Mathf.Clamp(targetX, minX, maxX);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isMoving = false;
            }
        }
    }
}
