using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }
    
    [SerializeField] private bool enableCursor;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } // Singleton

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

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
