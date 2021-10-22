using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunfuBtn : SingletonMonoBehaviour<KunfuBtn>
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickFire()
    {
        KunfuMgr.Instance.onFire();
    }
}
