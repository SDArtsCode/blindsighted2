using UnityEngine;

public class MouseVision : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;


    float rotateX = 0f;
    float rotateY = 0f;

    public Transform camPivot;

    private void Awake()
    {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!UIController.playerLocked)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotateY += mouseX;
            rotateX -= mouseY;
            rotateX = Mathf.Clamp(rotateX, -90f, 90f);


            camPivot.transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, rotateY, 0f);
        }
    }

    void OnPlayerDeath()
    {
        mouseSensitivity /= 8f;
    }
}
