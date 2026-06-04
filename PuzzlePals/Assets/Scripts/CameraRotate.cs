using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
//This script makes the camera rotate around the player

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
        inputManager.Axis.Enable();
        StartCoroutine(Rotate());
        inputManager.Axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }
    private void OnEnable()
    {
        
    }
    private IEnumerator Rotate()
    {
        while (true)
        {
            rotation *= rotationSpeed;
            transform.RotateAround(target.transform.position, Vector3.up, rotation.x);
            //to be added the clamp for rotating up and down
            //transform.RotateAround(target.transform.position, -Vector3.right, rotation.y);
            yield return null;
        }

    }
}
