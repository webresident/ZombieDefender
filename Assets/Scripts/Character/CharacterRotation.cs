using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, horizontalInput);
    }
}
