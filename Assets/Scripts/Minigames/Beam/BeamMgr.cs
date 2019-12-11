using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamMgr : MonoBehaviour
{
    
    GameObject gauge;
    int gauge_vector;
    float gauge_diff;

    // Start is called before the first frame update
    void Start()
    {
        gauge = GameObject.Find("Gauge");
        gauge.GetComponent<Slider>().value = 0;
        gauge_vector = 1;
        gauge_diff = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gauge.GetComponent<Slider>().value >= 1 && gauge_vector == 1)
        {
            gauge_vector = -1;
        }
        else if(gauge.GetComponent<Slider>().value <= 0 && gauge_vector == -1)
        {
            gauge_vector = 1;
        }
        gauge.GetComponent<Slider>().value += gauge_diff * gauge_vector;
    }
}
