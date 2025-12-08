using UnityEngine;

public abstract class Weapon_Base : MonoBehaviour
{
    public bool isActive;
    public int powerLevel;            //works based on XP
    public bool laser = false;
    [SerializeField] GameObject weaponMesh;

    [Header("Bullet Properties")]
    [SerializeField] protected float maxBullets = 15;
    [SerializeField] protected float reloadSpeed = 2;

    [Header("Aiming Properties")]
    [SerializeField] float aimSpeed = 8f;
    [SerializeField] float camAimFOV;
    [SerializeField] float camAimSpeed = 6f;
    [SerializeField] Vector3 meshAimingPos;
    Vector3 meshOriginalPosition;

    float camOriginalFOV;
    Camera cam;
    protected float currentBullets;
    protected float _reloadSpeed;

    protected InputController inputController;
    protected bool isReloading = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        cam = Camera.main;
        camOriginalFOV = cam.fieldOfView;
        inputController = GameObject.FindAnyObjectByType<InputController>();
        _reloadSpeed = reloadSpeed;
        currentBullets = maxBullets;
        meshOriginalPosition = weaponMesh.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            weaponMesh.SetActive(true);
            if(currentBullets < maxBullets && inputController.reload || currentBullets <= 0)
            {
                isReloading = true;
            }
            if (isReloading)
                Reloading();


            AimDownSights();
        }
        else
        {
            weaponMesh.SetActive(false);
        }
    }

    void AimDownSights()
    {
        if (inputController.isAiming && !isReloading)
        {
            weaponMesh.transform.localPosition = Vector3.Lerp(weaponMesh.transform.localPosition, meshAimingPos, Time.deltaTime * aimSpeed);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camAimFOV, Time.deltaTime * camAimSpeed);
        }
        else
        {
            weaponMesh.transform.localPosition = Vector3.Lerp(weaponMesh.transform.localPosition, meshOriginalPosition, Time.deltaTime * aimSpeed);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camOriginalFOV, Time.deltaTime * camAimSpeed);

        }
    }

    public abstract void Active();

    void Reloading()
    {
        _reloadSpeed -= Time.deltaTime;
        if(_reloadSpeed <= 0)
        {
            currentBullets = maxBullets;
            _reloadSpeed = reloadSpeed;
            isReloading = false;
        }
    }

    public void AutomaticReload()           //usually done when we reach a new power/XP level
    {
        currentBullets = maxBullets;
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public float CurrentBullets()
    {
        return currentBullets;
    }

    public float MaxBullets()
    {
        return maxBullets;
    }
}
