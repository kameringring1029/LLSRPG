using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPowerParticle : MonoBehaviour
{
    public delegate void onReach();
    onReach func;

    float speed = 250f;
    bool vanish_flag = false;


    // Start is called before the first frame update
    void Start()
    {
        // gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-200f, 10f, 0));
        vanish_flag = false;
    }

    public void initialize(onReach func)
    {
        this.func = func;
    }


    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody2D>().velocity *= 0.998f;
        //if(gameObject.GetComponent<Transform>().position.x < 0)
        //    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(5f, 0f, 0));


        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("beam").transform.position, step);
        if (transform.position == GameObject.Find("beam").transform.position)
        {
            StartCoroutine(vanish());

            if (!vanish_flag)
            {
                vanish_flag = true;
                func();
            }
        }
    }

    IEnumerator vanish()
    {
        float maxT = 1f;
        float deltaT = 0f;
        int step = 10;

        float speed_init = gameObject.GetComponent<ParticleSystem>().startSpeed;
        float gravity_init = gameObject.GetComponent<ParticleSystem>().gravityModifier;
        float color_a_init = gameObject.GetComponent<ParticleSystem>().startColor.a;
        float color_r = gameObject.GetComponent<ParticleSystem>().startColor.r;
        float color_b = gameObject.GetComponent<ParticleSystem>().startColor.b;
        float color_g = gameObject.GetComponent<ParticleSystem>().startColor.g;

        //while (gameObject.GetComponent<ParticleSystem>().startColor.a > 0.1f)
        while (deltaT < maxT)
        {
            float color_a = gameObject.GetComponent<ParticleSystem>().startColor.a;
            yield return new WaitForSeconds(maxT/step);
            deltaT += maxT/step;
            gameObject.GetComponent<ParticleSystem>().startColor = new Color(1f, 1f, 1f, color_a - color_a_init/step);
            gameObject.GetComponent<ParticleSystem>().gravityModifier -= gravity_init/step;
            gameObject.GetComponent<ParticleSystem>().startSpeed -= speed_init / step;
        }
        Destroy(gameObject);

    }
}

