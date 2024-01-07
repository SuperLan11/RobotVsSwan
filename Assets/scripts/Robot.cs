using System;
using System.Collections.Generic;
using UnityEngine;


public class Robot : MonoBehaviour, IHealth
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
    public Dictionary<WeaponType, int> inventory = new();
    [SerializeField] private GameObject sawPrefab;
    [SerializeField] private GameObject misslePrefab;
    public int eggs;
    void Start()
    {
        weaponMounts[WeaponSlot.RIGHT] = rightWeaponMount;
        weaponMounts[WeaponSlot.LEFT] = leftWeaponMount;
        SetWeapon(WeaponSlot.RIGHT, WeaponType.NONE);
        SetWeapon(WeaponSlot.LEFT, WeaponType.SWORD);
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            inventory[type] = 0;
        }

        maxHealth = health;
    }

    public static Robot instance;
    void Awake()
    {
        instance = this;
        maxHealth = health;
    }
    
    void Update()
    {
        bool isEditor = RoundManager.instance.roundState == RoundState.EDITOR;
        foreach (WeaponSlot slot in System.Enum.GetValues(typeof(WeaponSlot)))
        {
            if (!isEditor)
            {
                weapons[slot].PointAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else
            {
                weapons[slot].ResetDirection();
            }
        }

        if (!isEditor)
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
        GetComponent<Rigidbody2D>().velocity = movement * speed;
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
            case WeaponType.SAW:
                return Instantiate(sawPrefab);
            case WeaponType.MISSLE:
                return Instantiate(misslePrefab);
            default:
                throw new System.Exception("Invalid weapon type");
        }
    }

    public void SetWeapon(WeaponSlot slot, WeaponType type)
    {
        if (weapons.ContainsKey(slot))
        {
            inventory[weapons[slot].GetWeaponType()] += 1;
            Destroy(weaponMounts[slot].transform.GetChild(0).gameObject);
        }
        GameObject weaponObject = createWeapon(type);
        weapons[slot] = weaponObject.GetComponentInChildren<Weapon>();
        weaponObject.transform.parent = weaponMounts[slot].transform;
        weaponObject.transform.localPosition = Vector3.zero;
    }

    public int health;
    public int GetHealth()
    {
        return health;
    }

    private int maxHealth;
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BaseProjectile baseProjectile = other.gameObject.GetComponentInChildren<BaseProjectile>();
        if (baseProjectile != null)
        {
            TakeDamage(baseProjectile.GetDamage());
            Destroy(baseProjectile.gameObject);
        }
    }
}
