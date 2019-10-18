using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 作物の情報を表示するパネル用のやつ
 * 
 */

public class ProduceView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 情報更新 */
    public void renew(GameObject selectedblock, Produce prod)
    {
        renewSprite(selectedblock);
        renewText(selectedblock, prod);
    }

    void renewSprite(GameObject selectedblock)
    {
        // 作物Spriteの変更
        Sprite spr = selectedblock.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
        if (spr != null)
        {
            GameObject.Find("ProduceView").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.Find("ProduceView").GetComponent<Image>().sprite = spr;
        }
        else
        {
            GameObject.Find("ProduceView").GetComponent<Image>().color = new Color(1, 1, 1, 0); // 透明にする
        }


        // 花壇Spriteの変更
        GameObject.Find("KadanView").GetComponent<Image>().sprite
            = selectedblock.GetComponent<SpriteRenderer>().sprite;
    }

    void renewText(GameObject selectedblock, Produce prod)
    {
        if(prod != null)
        {
            NogyoItem item = NogyoItemDB.getinstance().getItemFromPType(prod.group, prod.type);

            GameObject.Find("ProduceViewText").GetComponent<TextMeshProUGUI>().text
                = item.shapingExplain();
        }
        else
        {
            GameObject.Find("ProduceViewText").GetComponent<TextMeshProUGUI>().text = "空き地だよ";
        }

    }
}
