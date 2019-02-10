using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPToast : MonoBehaviour {

    Text text_Toast;
    Image image_Toast;

    private void Init()
    {
        text_Toast = transform.Find("Text_Toast").GetComponent<Text>();
        image_Toast = transform.Find("Image_Toast").GetComponent<Image>();
    }

    public void MakeToast(string content, float time)
    {
        Init();
        StartCoroutine(ToastRoutine(content, time));
    }

    private void SetAlpha(float alpha)
    {
        if (text_Toast == null || image_Toast == null) return;
        text_Toast.color = new Color(text_Toast.color.r, text_Toast.color.g, text_Toast.color.b, alpha);
        image_Toast.color = new Color(image_Toast.color.r, image_Toast.color.g, image_Toast.color.b, alpha);
    }

    private IEnumerator ToastRoutine(string content, float time)
    {
        transform.localPosition = new Vector2(10000, 0);
        text_Toast.text = content;
        float alpha = 0;
        while (alpha < 1)
        {
            SetAlpha(alpha);
            yield return null;
            if(alpha <= 0)
            {
                image_Toast.rectTransform.sizeDelta = new Vector2(
                    text_Toast.rectTransform.sizeDelta.x + 100,
                    100);
                transform.localPosition = Vector2.zero;
            }
            alpha += 2 * Time.deltaTime;
        }
        yield return new WaitForSeconds(time);
        alpha = 1;
        while (alpha > 0)
        {
            SetAlpha(alpha);
            yield return null;
            alpha -= 2 * Time.deltaTime;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
