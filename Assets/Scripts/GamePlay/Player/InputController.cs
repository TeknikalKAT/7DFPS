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
    public bool togglePause;
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        forwardSwitch = Input.GetKeyDown(KeyCode.E) || scroll < 0f;
        backwardSwitch = Input.GetKeyDown(KeyCode.Q) || scroll > 0f;

        togglePause = Input.GetKeyDown(KeyCode.Escape);
    }
}
