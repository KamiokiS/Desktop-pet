using UnityEngine;

public class KeepObjectInScreen : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    void Start()
    {
        Camera cam = Camera.main;
        Vector3 lowerLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 upperRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        // Учитываем размеры спрайта
        float spriteWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float spriteHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        minX = lowerLeft.x + spriteWidth;
        maxX = upperRight.x - spriteWidth;
        minY = lowerLeft.y + spriteHeight;
        maxY = upperRight.y - spriteHeight;
    }

    private void OnMouseDrag()
    {
        Vector3 position = transform.position;

        // Ограничиваем координаты
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }
}
