using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    private int nowCursorPosition = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveCursor(int vector){

        nowCursorPosition += vector;
        if (nowCursorPosition < 0) nowCursorPosition = 1;
        if (nowCursorPosition > 1) nowCursorPosition = 0;

        if (nowCursorPosition == 1)
        {
            gameObject.GetComponent<Text>().text =
                "　こうげき\n" +
                "⇒たいき";
        }
        else if(nowCursorPosition == 0)
        {
            gameObject.GetComponent<Text>().text =
                "⇒こうげき\n" +
                "　たいき";
        }
    }

    public int getSelectedAction()
    {
        return nowCursorPosition;
    }
}
