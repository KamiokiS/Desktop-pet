using UnityEngine;

public class TakeByHand : MonoBehaviour
{
    private Vector2 offset;

    public bool RigidbodyObject;
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() - offset;
    }

    private void OnMouseDown()
    {
        offset = GetMouseWorldPosition() - (Vector2)transform.position;
        
        if(RigidbodyObject)
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnMouseUp()
    {
        if(RigidbodyObject)
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }


    public static Vector2 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return new Vector2(worldPosition.x, worldPosition.y);
    }
    
    
}
