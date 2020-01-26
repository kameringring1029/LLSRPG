using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMiniHeading : MonoBehaviour
{

    public void restart()
    {

        Application.LoadLevel("Heading"); // Reset
    }

    public void onClickTweet()
    {
        string record = GameObject.Find("Main Camera").GetComponent<ManagerMiniHeading>().record.ToString("F1");
        Debug.Log("tweeeeeet" + record);
        Application.ExternalCall("tweet", record);
    }
}
