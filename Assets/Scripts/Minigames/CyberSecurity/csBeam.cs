using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBeam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(destroyBeam());
    }

    void destroyBeam()
    {
        //yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
