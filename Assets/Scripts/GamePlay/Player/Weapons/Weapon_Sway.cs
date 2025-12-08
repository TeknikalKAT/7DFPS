using UnityEngine;

public class Weapon_Sway : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] float swayAmount;
    [SerializeField] float smoothAmount;
    [SerializeField] float maxAmount;

    [Header("Rotation")]
    [SerializeField] float rotationAmount = 4f;
    [SerializeField] float smoothRotation = 12f;
    [SerializeField] float maxRotationAmount = 5f;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    Vector3 initialPos;
    Quaternion initialRotation;

    InputController inputController;

    float inputX;
    float inputY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPos = transform.localPosition;
        initialRotation = transform.localRotation;
        inputController = GameObject.FindAnyObjectByType<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovementSway();
        TiltSway();
    }


    void GetInput()
    {
        inputX = -inputController.mouseX;
        inputY = -inputController.mouseY;
    }
    void MovementSway()
    {

        float moveX = Mathf.Clamp(inputX * swayAmount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(inputY * swayAmount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPos, smoothAmount * Time.deltaTime);
    }

    void TiltSway()
    {

        float tiltX = Mathf.Clamp(inputX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(inputY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltX : 0f, rotationY ? tiltY : 0f, rotationZ ? tiltY : 0f));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRotation, smoothRotation * Time.deltaTime);
    }
}
