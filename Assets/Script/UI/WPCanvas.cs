using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPCanvas : MonoBehaviour {

    private void Awake()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
