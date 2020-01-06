using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beambeam : MonoBehaviour
{

    bool trg;

    // Start is called before the first frame update
    void Start()
    {
        trg = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {
        Debug.Log("trigger");
        trg = true;
    }
}
