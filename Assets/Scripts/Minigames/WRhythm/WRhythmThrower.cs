using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRhythmThrower : MonoBehaviour
{
    int animhash;

    // Start is called before the first frame update
    void Start()
    {
        animhash = 0;

    //    StartCoroutine(resetAnim());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(syncWaitAnimator());

    }


    IEnumerator resetAnim()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("trg_ready");
        GetComponent<Animator>().SetTrigger("trg_throw");
    }


    /**/
    IEnumerator syncWaitAnimator()
    {
        yield return null;

        if(animhash != GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("thrower_wait")))
            {
                Debug.Log("thrower:sync");
                GetComponent<Animator>().ForceStateNormalizedTime(1f/Time.time);
            }
            animhash = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash;

        }

    }
}
