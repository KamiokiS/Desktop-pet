using UnityEngine;

public class OpenBookButton : MonoBehaviour
{
    public GameObject CloseBookIcon;
    public GameObject OpenBook;

    private Vector3 lastPos;

    private void OnMouseUp()
    {
        if (lastPos == transform.position)
            OpenBookMethod();
    }

    private void OnMouseDown()
    {
        lastPos = transform.position;
    }


    private void OpenBookMethod()
    {
        MoveOpenBookToCloseBookIconTransform();
        CloseBookIcon.SetActive(false);
        OpenBook.SetActive(true);
    }


    public void MoveOpenBookToCloseBookIconTransform()
    {
        OpenBook.transform.position = CloseBookIcon.transform.position;
    }
}