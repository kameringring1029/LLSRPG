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

        gaugeUpdate();

        if (Input.GetMouseButtonDown(0)) // クリック！！！
        {
            Debug.Log("push!");

            float gaugeval = gauge.GetComponent<Slider>().value;
            beamsize += gaugeval;
          //  changeBeamSize();

            GameObject particle = Instantiate(Resources.Load<GameObject>("Minigame/beam/PowerParticle"));
            particle.GetComponent<BeamPowerParticle>().initialize(changeBeamSize);

            chargetime += 1;
        }
    }

    /* ゲージの制御 */
    void gaugeUpdate()
    {
        float gaugeval = gauge.GetComponent<Slider>().value;

        if (gaugeval >= 1 && gauge_vector == 1)
        {
            gauge_vector = -1;
        }
        else if (gaugeval <= 0 && gauge_vector == -1)
        {
            gauge_vector = 1;
        }
        gauge.GetComponent<Slider>().value += gauge_diff * gauge_vector;

    }

    /* ビームをでっかく */
    void changeBeamSize()
    {
        StartCoroutine(changeBeamSizeProc());
    }
    IEnumerator changeBeamSizeProc()
    {
        float x_bef = GameObject.Find("beam").GetComponent<Transform>().localScale.x;
        float y_bef = GameObject.Find("beam").GetComponent<Transform>().localScale.y;

        for(int step = 7; step >= 1; step--)
        {
            yield return new WaitForSeconds(0.1f);

            GameObject.Find("beam").GetComponent<Transform>().localScale
             = new Vector3(x_bef + ((float)beamsize - x_bef)/step, y_bef + ((float)beamsize - y_bef) / step, 1);

        }

        // 3回目で発射
        if (chargetime == 3) beamFire();
    }

    /* ビーム発射 */
    void beamFire()
    {
        Debug.Log("onFire");
        inEffect = true;

        GameObject.Find("lazerbeamGK_sprite").GetComponent<Animator>().SetBool("onFire", true);

        GameObject.Find("beam").GetComponent<ParticleSystem>().startLifetime = 10;
        Instantiate(Resources.Load<GameObject>("Minigame/beam/Button_Restart"), GameObject.Find("Canvas_FG").transform);

        StartCoroutine(showResult());
    }
    IEnumerator showResult()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Transform>().position = new Vector3(-10,0,-10);
        //gameObject.GetComponent<Camera>().orthographicSize = 10f;

        if (GameObject.Find("target").GetComponent<Renderer>().isVisible)
            Debug.Log("result");
    }
}
