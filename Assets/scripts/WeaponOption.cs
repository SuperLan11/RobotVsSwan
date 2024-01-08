using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,
    IEndDragHandler, IDragHandler
{
    public Image weaponImage;
    private float startImageScale, imageScale;
    private float bigImageScale = 1f;
    private bool hovering = false;
    public WeaponType weaponType;
    public GameObject costBackground;
    public int cost;
    public TextMeshProUGUI costText;

    void Start()
    {
        startImageScale = weaponImage.gameObject.transform.localScale.x;
        imageScale = startImageScale;
        costText.text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float target;
        if (hovering)
        {
            target = bigImageScale;
        }
        else
        {
            target = startImageScale;
        }

        imageScale = Mathf.Lerp(imageScale, target, Time.deltaTime);
        weaponImage.transform.localScale = new Vector3(imageScale, imageScale, imageScale);
        costBackground.SetActive(getInventory() == 0);
        GetComponent<CanvasGroup>().alpha = canPlace() ? 1 : 0.5f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    private GameObject hoverSprite;

    private bool canPlace()
    {
        return Robot.instance.eggs >= cost || getInventory() > 0;
    }

    private int getInventory()
    {
        return Robot.instance.inventory[weaponType];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canPlace())
        {
            return;
        }
        hoverSprite = Instantiate(weaponImage.gameObject, GetComponentInParent<Canvas>().transform);
        weaponImage.enabled = false;
        Robot.instance.ShowArmSlots();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canPlace())
        {
            return;
        }
        Destroy(hoverSprite);
        weaponImage.enabled = true;
        Robot.instance.HideArmSlots();
        Vector2 point = Camera.main.ScreenToWorldPoint(eventData.position);
        //Debug.Log(point);
        Collider2D collider = Physics2D.OverlapPoint(point, layerMask: LayerMask.GetMask("UI"));
        if (collider != null && collider.gameObject.GetComponent<WeaponMount>() != null)
        {
            WeaponMount mount = collider.gameObject.GetComponent<WeaponMount>();
            Robot.instance.SetWeapon(mount.slot, weaponType);
            AudioManager.instance.Play("robot_attach");
            if (getInventory() == 0)
            {
                Robot.instance.eggs -= cost;
            }
            else
            {
                Robot.instance.inventory[weaponType]--;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canPlace())
        {
            return;
        }
        hoverSprite.transform.position = eventData.position;
    }
}
