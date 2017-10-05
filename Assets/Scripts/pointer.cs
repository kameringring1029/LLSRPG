using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour {

    GameObject Pointer;
    GameObject Camera;

    // Use this for initialization
    void Start () {
        Pointer=GameObject.Find("cursol");
        Camera = GameObject.Find("Main Camera");
    }
	
	public void onClickUp()
    {
        Pointer.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(1,0.5f,0);
        Camera.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(1, 0.5f, -10);

    }

    public void onClickDown()
    {
        Pointer.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(-1, -0.5f, 0);
        Camera.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(1, 0.5f, -10); ;
    }

    public void onClickLeft()
    {
        Pointer.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(-1, 0.5f, 0);
        Camera.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(1, 0.5f, -10); ;
    }

    public void onClickRignt()
    {
        Pointer.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(1, -0.5f, 0);
        Camera.GetComponent<Transform>().position = Pointer.GetComponent<Transform>().position + new Vector3(1, 0.5f, -10); ;
    }
}
