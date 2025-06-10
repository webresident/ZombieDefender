using Cinemachine;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float moveRotationSpeed = 5.0f;

    [Header("Camera Settings")]
    [SerializeField] private float xCameraValue = 0f;
    [SerializeField] private float yCameraValue = 0f;
    [SerializeField] private float cameraReturnSpeed = 3.0f;

    [SerializeField] private CinemachineFreeLook cinemachineCamera;

    private bool isRightMouseHeld = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMouseInput();
        HandleCameraAndCharacterRotation();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseHeld = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightMouseHeld = false;
        }
    }

    private void HandleCameraAndCharacterRotation()
    {
        if (isRightMouseHeld)
        {
            // При зажатой ПКМ — персонаж следует за вращением камеры
            RotateCharacterToCamera();
        }
        else
        {
            // Камера плавно возвращается в дефолтное положение
            SetCameraDefaultPosition();

            // Если персонаж движется вперёд — поворачиваем по камере
            if (Input.GetAxis("Vertical") > 0.1f)
            {
                RotateCharacterToCameraSmooth();
            }
        }
    }

    private void RotateCharacterToCamera()
    {
        Vector3 cameraForward = cinemachineCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RotateCharacterToCameraSmooth()
    {
        Vector3 cameraForward = cinemachineCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveRotationSpeed * Time.deltaTime);
    }

    private void SetCameraDefaultPosition()
    {
        cinemachineCamera.m_XAxis.Value = Mathf.Lerp(cinemachineCamera.m_XAxis.Value, xCameraValue, cameraReturnSpeed * Time.deltaTime);
        cinemachineCamera.m_YAxis.Value = Mathf.Lerp(cinemachineCamera.m_YAxis.Value, yCameraValue, cameraReturnSpeed * Time.deltaTime);
    }
}
