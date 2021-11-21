using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRhythmThrower : MonoBehaviour
{
    public int animhash;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animhash = 0;
        animator = GetComponent<Animator>();
        //GetComponent<Animator>().Play("thrower_wait", 0, Time.time);

        //    StartCoroutine(resetAnim());

    }


    // Update is called once per frame
    void Update()
    {
       //StartCoroutine(syncWaitAnimator());

    }


    IEnumerator resetAnim()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("trg_ready");
        GetComponent<Animator>().SetTrigger("trg_throw");
    }


    
    IEnumerator syncWaitAnimator()
    {
        yield return null;

        if(animhash != GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("thrower_wait")))
            {                
                GetComponent<Animator>().ForceStateNormalizedTime(1f/Time.time);
                Debug.Log("thrower:sync");
            }
            animhash = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash;

        }

    }
    

}
