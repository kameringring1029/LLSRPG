using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeamMgr : MonoBehaviour
{
    
    GameObject gauge;
    int gauge_vector;
    float gauge_diff;

    float beamsize;

    int chargetime;

    bool inEffect;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start beam");
        gauge = GameObject.Find("Gauge");
        gauge.GetComponent<Slider>().value = 0;
        gauge_vector = 1;
        gauge_diff = 0.05f;

        beamsize = 0.1f;
        changeBeamSize();

        chargetime = 0;
        inEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inEffect) return;

        float gaugeval = gauge.GetComponent<Slider>().value;

        if (gaugeval >= 1 && gauge_vector == 1)
        {
            gauge_vector = -1;
        }
        else if(gaugeval <= 0 && gauge_vector == -1)
        {
            gauge_vector = 1;
        }
        gauge.GetComponent<Slider>().value += gauge_diff * gauge_vector;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("push!");
            beamsize += gaugeval;
            changeBeamSize();

            chargetime += 1;
            if (chargetime > 2) beamFire();
        }
    }

    void changeBeamSize()
    {
        GameObject.Find("beam").GetComponent<Transform>().localScale
         = new Vector3((float)beamsize, (float)beamsize, 1);
    }

    void beamFire()
    {
        inEffect = true;

        GameObject.Find("Guilty").GetComponent<Animator>().SetBool("onFire", true);

        GameObject.Find("beam").GetComponent<ParticleSystem>().startLifetime = 10;
        Instantiate(Resources.Load<GameObject>("Minigame/beam/Button_Restart"), GameObject.Find("Canvas").transform);
    }


}
