using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    private InputManager inputManager;
    private Vector2 rotation;
    bool allowRotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform target;
    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.Press.Enable();
        inputManager.Axis.Enable();
        inputManager.Press.performed += _ => { StartCoroutine(Rotate()); };
        inputManager.Press.canceled += _ => { allowRotation = false; };
        inputManager.Axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }
    private void OnEnable()
    {
        
    }
    private IEnumerator Rotate()
    {
        allowRotation = true;
        while (allowRotation)
        {
            rotation *= rotationSpeed;
            transform.RotateAround(target.transform.position, Vector3.up, rotation.x);
            yield return null;
        }

    }
}
