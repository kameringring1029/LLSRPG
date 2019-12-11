using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CommCtrl : MonoBehaviour
{

    bool inComm;
    ItemMenu.STATUS status;
    int messageid;

    // Start is called before the first frame update
    void Start()
    {
        inComm = true;
    }

    public void init(ItemMenu.STATUS status)
    {
        this.status = status;
        StartCoroutine(initproc());

    }
    IEnumerator initproc()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "<color=red>ルル</color>:\n" + getRandomMessage();
        yield return new WaitForSeconds(1f);

        gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("inComm", true);

        while (inComm)
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "<color=red>ルル</color>:\n"+getRandomMessage();

            yield return new WaitForSeconds(5f);
        }
    }

    public void destroy()
    {
        StartCoroutine(destroyproc());
    }
    IEnumerator destroyproc()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "<color=red>ルル</color>:\n" + "まかせて!";

        gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("inComm", false);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    string getRandomMessage()
    {
        string message = "まかせて!";

        // random
        int rand;
        do
        {
            rand = Random.Range(1, 8);
        } while (rand == messageid);
        messageid = rand;



        // メッセージ決定
        switch (messageid)
        {
            case 1:
                return "おなかすいたーー!";
            case 2:
                return "あっ、ねこ";
            case 3:
                return "スズちゃんのオデンたべたいーーー";
            case 4:
                return "たまにはアルちゃんに運動させなきゃ";
            default:
                // デフォメッセージ
                switch (status)
                {
                    case ItemMenu.STATUS.inSelectShip:
                        return "どれを出荷する?";
                    case ItemMenu.STATUS.inShop:
                        return "何を買ってくる?";
                    default:
                        return "";

                }

        }

        return message;
    }



}
