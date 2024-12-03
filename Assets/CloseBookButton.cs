using System;
using UnityEngine;

public class CloseBookButton : MonoBehaviour
{
    public GameObject CloseBookIcon;
    public GameObject OpenBook;

    private Vector3 lastPos;

    private void OnMouseUp()
    {
        RaycastOnMouseDown();
    }

    private void RaycastOnMouseDown()
    {
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider == null) 
            return;
        
        if (hit.collider.transform.localPosition == transform.localPosition)
        {
            CloseBook();
        }
    }

    private void CloseBook()
    {
        MoveCloseBookIconToOpenBookTransform();
        CloseBookIcon.SetActive(true);
        OpenBook.SetActive(false);
    }
    
    

    public void MoveCloseBookIconToOpenBookTransform()
    {
        CloseBookIcon.transform.position = OpenBook.transform.position;
        // SL.Instance.GetService<DontAllowMoveObjectOutsideFromTheScreen>().CheckPos(CloseBookIcon.transform);
    }
}
