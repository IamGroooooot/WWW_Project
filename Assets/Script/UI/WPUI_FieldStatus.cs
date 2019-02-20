using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUI_FieldStatus : MonoBehaviour {

    Image image_Worker;
    Image image_Fertilizer;

    private void Awake()
    {
        image_Worker = transform.Find("Image_Worker").GetComponent<Image>();
        image_Fertilizer = transform.Find("Image_Fertilizer").GetComponent<Image>();
    }

    public void SetSprite_Worker(Sprite sprite)
    {
        if (image_Worker == null) return;
        image_Worker.color = new Color(1, 1, 1, 1);
        image_Worker.sprite = sprite;
    }

    public void SetSprite_Fertilizer(Sprite sprite)
    {
        if (image_Fertilizer == null) return;
        image_Fertilizer.color = new Color(1, 1, 1, 1);
        image_Fertilizer.sprite = sprite;
    }

    public void ClearSprite()
    {
        if (image_Worker != null)
        {
            image_Worker.color = new Color(1, 1, 1, 0);
            image_Worker.sprite = null;
        }
        if (image_Fertilizer != null)
        {
            image_Fertilizer.color = new Color(1, 1, 1, 0);
            image_Fertilizer.sprite = null;
        }
    }

}
