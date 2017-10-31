using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using UnityEngine.EventSystems;
using System;

public class FieldBlock : MonoBehaviour
{

    public GameObject GroundedUnit { get; set; }
    public int[] position = new int[2];

    public Kusa blockInfo;

	// Use this for initialization
	void Start () {
        blockInfo = new Kusa();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    public void OnClick()
    {
        GameObject.Find("Main Camera").GetComponent<GameMgr>().pushBlock(position[0], position[1]);
    }
}
