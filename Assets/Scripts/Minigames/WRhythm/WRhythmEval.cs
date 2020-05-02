using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WRhythmEval : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float a = gameObject.GetComponent<Image>().color.a;
        if (a <= 0) Destroy(gameObject);
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, a - 0.02f);
    }
}
