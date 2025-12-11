using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FPSController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("--Rotation Properties--")]
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float maxX = 60f, minX = -30f;     //clamped rotation
    [SerializeField] Transform cam;

    bool isGrounded;

    InputController inputController;
    float xRotation = 0;
    CharacterController charController;
    Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputController = GameObject.FindAnyObjectByType<InputController>();
        charController = GetComponent<CharacterController>();
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
           
        Rotation();
        Movement();
    }

    void Rotation()
    {
        float mouseX = inputController.mouseX * mouseSensitivity * Time.deltaTime;
        float mouseY = inputController.mouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }

    void Movement()
    {
        Vector3 moveDirection = transform.right * inputController.horizontal + transform.forward * inputController.vertical;
        charController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if(inputController.jumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
}
