using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

[RequireComponent(typeof(Rigidbody))]

public class FieldBlock : MonoBehaviour {

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


    public void onClick()
    {
       // blockInfo.groundtype = Unit.GROUNDTYPE.SEA;
        GameObject.Find("Main Camera").GetComponent<GameMgr>().pushBlock(position[0], position[1]);
    }


    public string outputInfo()
    {
        string outinfo =
            position[0] + "-" + position[1] +"\n" +
            "ブロック(" + blockInfo.type + ")" + "\n" +
            "特殊効果；" + blockInfo.groundtype + "\n";

        Debug.Log(outinfo);

        return outinfo;
    }
}
