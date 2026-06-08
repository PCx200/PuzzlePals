using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool enableCursor;


    private void Start()
    {
        if (!enableCursor)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
