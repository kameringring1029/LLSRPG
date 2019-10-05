using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList : MonoBehaviour
{

    List<GameObject> BtnList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /* delegate用のやつ */
    public delegate void buttonexecWrapper(int x);
    public delegate void buttonStrExecWrapper(string x);




    // Mapリストを取得してリストUIに反映
    //
    static public void setItemButtonList(ItemBox itemlist , buttonStrExecWrapper func)
    {

        GameObject btnPref = Resources.Load<GameObject>("Prefab/ScrollViewButtonPrefab");

        //Content取得(ボタンを並べる場所)
        RectTransform content = GameObject.Find("Content").GetComponent<RectTransform>();

        //Contentの高さ決定
        //(ボタンの高さ+ボタン同士の間隔)*ボタン数
        float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * itemlist.items.Count);

        foreach(KeyValuePair<NogyoItem, int> item in itemlist.items)
        {
            //ボタン生成
            GameObject btn = (GameObject)Instantiate(btnPref);
            btn.name = item.Key.id + "_" + btn.name;

            //ボタンをContentの子に設定
            btn.transform.SetParent(content, false);

            //ボタンのテキスト変更
            btn.transform.GetComponentInChildren<Text>().text = item.Key.name + ":" + item.Value;
            btn.GetComponent<Image>().color = new Color(192 / 255f, 192 / 255f, 228 / 255f, 192 / 255f);

            //ボタンのクリックイベント登録
            btn.transform.GetComponent<Button>().onClick.AddListener(() => func(item.Key.id));

        }

    }



}
