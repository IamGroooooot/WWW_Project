using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUI_FieldStatus : MonoBehaviour {

    private WPImageText imageText_Worker;
    private WPImageText imageText_Fertilizer;
    public WPImageText Worker
    {
        get
        {
            if(imageText_Worker == null)
            {
                imageText_Worker = transform.Find("ImageText_Worker").GetComponent<WPImageText>();
            }
            return imageText_Worker;
        }
    }

    public WPImageText Fertilizer
    {
        get
        {
            if(imageText_Fertilizer == null)
            {
                imageText_Fertilizer = transform.Find("ImageText_Fertilizer").GetComponent<WPImageText>();
            }
            return imageText_Fertilizer;
        }
    }

    private void Awake()
    {
        imageText_Worker = transform.Find("ImageText_Worker").GetComponent<WPImageText>();
        imageText_Fertilizer = transform.Find("ImageText_Fertilizer").GetComponent<WPImageText>();
    }

    public void SetActive(bool param)
    {
        gameObject.SetActive(param);
    }

}
