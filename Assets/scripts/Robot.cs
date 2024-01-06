using System;
using System.Collections.Generic;
using UnityEngine;


public class Robot : MonoBehaviour
{
    [SerializeField] private GameObject nullWeaponPrefab;
    private Dictionary<WeaponSlot, Weapon> weapons = new();
    private Dictionary<WeaponSlot, GameObject> weaponMounts = new();
    [SerializeField] private GameObject rightWeaponMount;
    [SerializeField] private GameObject leftWeaponMount;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private GameObject gunPrefab;
    private WeaponType currentWeaponLeft = WeaponType.SWORD;
    private WeaponType currentWeaponRight = WeaponType.NONE;
    void Start()
    {
        weaponMounts[WeaponSlot.RIGHT] = rightWeaponMount;
        weaponMounts[WeaponSlot.LEFT] = leftWeaponMount;
        SetWeapon(WeaponSlot.RIGHT, WeaponType.NONE);
        SetWeapon(WeaponSlot.LEFT, WeaponType.SWORD);
    }

    public static Robot instance;
    void Awake()
    {
        instance = this;
    }

    public bool editorMode = false;

    void Update()
    {
        foreach (WeaponSlot slot in System.Enum.GetValues(typeof(WeaponSlot)))
        {
            if (!editorMode)
            {
                weapons[slot].PointAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else
            {
                weapons[slot].ResetDirection();
            }
        }

        if (!editorMode)
        {
            Move();
            processWeaponChanges();
            processShooting();
        }
    }

    public void ShowArmSlots()
    {
        foreach (GameObject mount in weaponMounts.Values)
        {
            mount.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    public void HideArmSlots()
    {
        foreach (GameObject mount in weaponMounts.Values)
        {
            mount.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void processShooting()
    {
        if (Input.GetMouseButton(0))
        {
            weapons[WeaponSlot.LEFT].Shoot();
        }
        if (Input.GetMouseButton(1))
        {
            weapons[WeaponSlot.RIGHT].Shoot();
        }
    }

    void processWeaponChanges()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponLeft = (WeaponType)(((int)currentWeaponLeft + 1) % 3);
            SetWeapon(WeaponSlot.LEFT, currentWeaponLeft);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeaponRight = (WeaponType)(((int)currentWeaponRight + 1) % 3);
            SetWeapon(WeaponSlot.RIGHT, currentWeaponRight);
        }
    }

    public float speed;
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        //Debug.Log(movement);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private GameObject createWeapon(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.SWORD:
                return Instantiate(swordPrefab);
            case WeaponType.GUN:
                return Instantiate(gunPrefab);
            case WeaponType.NONE:
                return Instantiate(nullWeaponPrefab);
            default:
                throw new System.Exception("Invalid weapon type");
        }
    }

    public void SetWeapon(WeaponSlot slot, WeaponType type)
    {
        if (weapons.ContainsKey(slot))
        {
            Destroy(weaponMounts[slot].transform.GetChild(0).gameObject);
        }
        GameObject weaponObject = createWeapon(type);
        weapons[slot] = weaponObject.GetComponentInChildren<Weapon>();
        weaponObject.transform.parent = weaponMounts[slot].transform;
        weaponObject.transform.localPosition = Vector3.zero;
    }
}