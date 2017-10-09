using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

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
}
