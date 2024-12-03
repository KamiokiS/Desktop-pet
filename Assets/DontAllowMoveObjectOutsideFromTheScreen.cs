using System;
using UnityEngine;

public class DontAllowMoveObjectOutsideFromTheScreen : MonoBehaviour
{
    private Vector2 screenSize;

    private void Awake()
    {
        SL.Instance.RegisterService(this);
        
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)),
                           Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) *
                       0.5f; //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)),
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
    }


    public void CheckPos(Transform objTransform)
    {

        // Конвертируем мировые координаты объекта в экранные (Viewport)
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(objTransform.position);

        // Проверяем, вышел ли объект за пределы видимости экрана
        if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)
        {
            // Ограничиваем положение объекта, чтобы вернуть его в экранные границы
            viewportPos.x = Mathf.Clamp(viewportPos.x, 0f, 1f);
            viewportPos.y = Mathf.Clamp(viewportPos.y, 0f, 1f);

            // Преобразуем экранные координаты обратно в мировые
            objTransform.position = Camera.main.ViewportToWorldPoint(viewportPos);

            // Устанавливаем объект в новую позицию
            transform.position = objTransform.position;
        }
    }
}