using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float  moveSpeed        = 5f;
    public float  mouseSensitivity = 100f;
    public bool   canMove          = true;

    private CharacterController controller;
    private float  xRotation = 0f;
    private Transform camTransform;
    private Vector3 velocity;

    void Start()
    {
        controller   = GetComponent<CharacterController>();
        camTransform = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!canMove) return;

        // Mouse look
        float mX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation - mY, -80f, 80f);
        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mX);

        // WASD movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y -= 9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    public void SetMovement(bool state)
    {
        canMove          = state;
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible   = !state;
    }
}