using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Manager : MonoBehaviour
{

    [SerializeField] Weapon_Base[] weapons;
    [SerializeField] float switchTime = 2f;
    [Header("--UI ELEMENTS--")]
    [SerializeField] Text bulletText;
    [SerializeField] Slider laserSlider;

    public bool canShoot;

    //we'll add a reload icon here, which we'll turn on when reloading, and turn off when not reloading

    Weapon_Base currentWeapon;
    Animator anim;

    InputController inputController;
    int currentWeaponIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canShoot = true;
        currentWeaponIndex = 0;
        currentWeapon = weapons[currentWeaponIndex];
        anim = GetComponent<Animator>();
        inputController = GameObject.FindAnyObjectByType<InputController>();
        SwitchToActiveWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //can happen when current weapon is not reloading, so we'll check this
        if(!currentWeapon.IsReloading() && canShoot)
            WeaponSwitch();

        if (canShoot)
            currentWeapon.Active();

        if(!currentWeapon.laser)
        {
            bulletText.gameObject.SetActive(true);
            laserSlider.gameObject.SetActive(false);
            bulletText.text = currentWeapon.CurrentBullets().ToString() + " / " + currentWeapon.MaxBullets();
        }
        else
        {
            bulletText.gameObject.SetActive(false);
            laserSlider.gameObject.SetActive(true);
            laserSlider.maxValue = currentWeapon.MaxBullets();
            laserSlider.value = currentWeapon.CurrentBullets();
        }
    }



    void SwitchToActiveWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (currentWeaponIndex == i)
            {
                currentWeapon = weapons[i];
                currentWeapon.isActive = true;
            }
            else
                weapons[i].isActive = false;
        }
    }

    void WeaponSwitch()
    {
        if (inputController.forwardSwitch)
            ForwardSwitch();
        if (inputController.backwardSwitch)
            BackwardSwitch();
    }
    public void ForwardSwitch()
    {
        canShoot = false;
        if ((currentWeaponIndex + 1) != weapons.Length)
            currentWeaponIndex += 1;
        else
            currentWeaponIndex = 0;
        StartCoroutine(SwitchTime());   
    }
    public void BackwardSwitch()
    {
        canShoot = false;
        if ((currentWeaponIndex - 1) != -1)
            currentWeaponIndex -= 1;
        else
            currentWeaponIndex = weapons.Length - 1;
        StartCoroutine(SwitchTime());
    }

    IEnumerator SwitchTime()
    {
        anim.CrossFadeInFixedTime("WeaponHolder_Switch", 0.01f);
        yield return new WaitForSeconds(switchTime);
        SwitchToActiveWeapon();
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
