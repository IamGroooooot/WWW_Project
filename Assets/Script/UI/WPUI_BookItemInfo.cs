using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUI_BookItemInfo : MonoBehaviour
{
    private WPImageText imageText_Name;
    public WPImageText Name
    {
        get
        {
            if(imageText_Name == null)
            {
                imageText_Name = transform.Find("ImageText_Name").GetComponent<WPImageText>();
            }
            return imageText_Name;
        }
    }

    private Image image_Item;

    private void Awake()
    {
        transform.Find("Button_Back").GetComponent<Button>().onClick.AddListener(OnClick_Back);
        image_Item = transform.Find("Image_Item").GetComponent<Image>();
        imageText_Name = transform.Find("ImageText_Name").GetComponent<WPImageText>();
    }

    private void OnClick_Back()
    {
        SetActive(false);
    }

    public void SetSprite_Item(Sprite content)
    {
        if (image_Item == null) return;
        image_Item.sprite = content;
    }

    public void SetActive(bool param)
    {
        gameObject.SetActive(param);
    }

}
