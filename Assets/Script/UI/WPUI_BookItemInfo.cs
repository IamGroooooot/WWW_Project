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

    private WPImageText[] imageText_Info = new WPImageText[3];

    private Image image_Item;

    private void Awake()
    {
        transform.Find("Button_Back").GetComponent<Button>().onClick.AddListener(OnClick_Back);
        image_Item = transform.Find("Image_Item").GetComponent<Image>();
        imageText_Name = transform.Find("ImageText_Name").GetComponent<WPImageText>();
        for(int i = 0; i < imageText_Info.Length; ++i)
        {
            Transform item = transform.Find("ImageText_Info_" + i.ToString());
            if (item == null) continue;
            imageText_Info[i] = item.GetComponent<WPImageText>();
        }
    }

    private void OnClick_Back()
    {
        SetActive(false);
    }

    public WPImageText GetImageText_Info(int index)
    {
        if (index < 0 || index > imageText_Info.Length) return null;
        if (imageText_Info[index] == null) return null;
        return imageText_Info[index];
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
