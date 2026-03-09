using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    const string SHOOT_STRING = "Shoot";
    float timeSinceLastShot = 0f;


    // [SerializeField] Transform weaponHolder;
    


    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentWeapon =  GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        HandleShoot();
    }

    // public void SwitchWeapon(WeaponSO weaponSO)
    // {
    //     if (currentWeapon)
    //     {
    //         Destroy(currentWeapon.gameObject);
    //     }

    //     Weapon newWeapon =  Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        
    //     // newWeapon.transform.localPosition = Vector3.zero;
    //     // newWeapon.transform.localRotation = Quaternion.identity;
    
        
    //     currentWeapon = newWeapon;
    //     this.weaponSO = weaponSO;
    // }

        public void SwitchWeapon(WeaponSO weaponSO)
    {
        GameObject oldWeapon = null;

        if (currentWeapon)
        {
            oldWeapon = currentWeapon.gameObject;
            currentWeapon = null; // prevent shooting the old weapon
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;

        if (oldWeapon != null)
        {
            Destroy(oldWeapon);
        }
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;
        if(currentWeapon == null) return;

        if(timeSinceLastShot >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }



    }
}
