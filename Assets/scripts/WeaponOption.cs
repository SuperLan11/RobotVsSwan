using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        startImageScale = weaponImage.gameObject.transform.localScale.x;
        imageScale = startImageScale;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        hoverSprite = Instantiate(weaponImage.gameObject, GetComponentInParent<Canvas>().transform);
        weaponImage.enabled = false;
        Robot.instance.ShowArmSlots();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        hoverSprite.transform.position = eventData.position;
    }
}
