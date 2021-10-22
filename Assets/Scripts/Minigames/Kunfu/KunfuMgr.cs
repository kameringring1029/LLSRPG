using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunfuMgr : SingletonMonoBehaviour<KunfuMgr>
{
    private bool isFiring;

    public GameObject player;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onFire()
    {
        Debug.Log("fire!");

        switch (isFiring)
        {
            case true:
                isFiring = false;
                break;

            case false:
                isFiring = true;
                break;
        }

        player.GetComponent<KunfuPlayer>().actionFire(isFiring);
    }
}
