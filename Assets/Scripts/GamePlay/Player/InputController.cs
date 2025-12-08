using UnityEngine;

public class InputController : MonoBehaviour
{
    public float mouseX;
    public float mouseY;
    public float horizontal;
    public float vertical;
    public bool jumping;
    public bool reload;
    public bool isFiring;
    public bool isAiming;
    public bool forwardSwitch;
    public bool backwardSwitch;

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumping = Input.GetKeyDown(KeyCode.Space);


        reload = Input.GetKeyDown(KeyCode.R);
        isFiring = Input.GetButton("Fire1");
        isAiming = Input.GetButton("Fire2");
        forwardSwitch = Input.GetKeyDown(KeyCode.E);
        backwardSwitch = Input.GetKeyDown(KeyCode.Q);
    }
}
