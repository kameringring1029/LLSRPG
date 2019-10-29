using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemMenu : MonoBehaviour
{
    ItemBox itembox;

    public void Activate(ItemBox itembox)
    {
        this.itembox = itembox;
        ButtonList.buttonStrExecWrapper itemfunc = selectItem;
        ButtonList.setItemButtonList(itembox, itemfunc, GameObject.Find("ItemListContent").GetComponent<RectTransform>());
    }

    // itemボタンが押下されたときに呼ばれることになる関数
    public void selectItem(string itemid)
    {
        renewExplain(itemid);
    }


    // アイテム種別タブが押下されたときに呼ばれることになる関数
    public void onClickTab(int group)
    {
        ItemBox filterbox = itembox.filterByItemgroupForTab((NogyoItem.NogyoItemGroup)Enum.ToObject(typeof(NogyoItem.NogyoItemGroup), group));

        ButtonList.destroyAllButtons();
        ButtonList.buttonStrExecWrapper itemfunc = selectItem;
        ButtonList.setItemButtonList(filterbox, itemfunc, GameObject.Find("ItemListContent").GetComponent<RectTransform>());


    }

    // アイテム種別タブが押下されたときに呼ばれることになる関数
    public void onFiltering(Dropdown dropdown)
    {
        int group = dropdown.value;

        ItemBox filterbox = itembox.filterByItemgroupForTab((NogyoItem.NogyoItemGroup)Enum.ToObject(typeof(NogyoItem.NogyoItemGroup), group));

        ButtonList.destroyAllButtons();
        ButtonList.buttonStrExecWrapper itemfunc = selectItem;
        ButtonList.setItemButtonList(filterbox, itemfunc, GameObject.Find("ItemListContent").GetComponent<RectTransform>());

    }


        /* 説明分と画像を更新 */
        public void renewExplain(string itemid)
    {
        Debug.Log("selectitem" + itemid);
        GameObject.Find("ItemExplainView").GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Nogyo/item/" + NogyoItemDB.getinstance().db[itemid].id);

      //  GameObject.Find("ItemExplainText").GetComponent<TextMeshProUGUI>().text = NogyoItemDB.getinstance().db[itemid].shapingExplain();
        GameObject.Find("ItemExplainTitle").GetComponent<TextMeshProUGUI>().text = NogyoItemDB.getinstance().db[itemid].name;
        GameObject.Find("ItemExplainText").GetComponent<TextMeshProUGUI>().text = NogyoItemDB.getinstance().db[itemid].explain;
        GameObject.Find("ItemExplainNum").GetComponent<TextMeshProUGUI>().text = itembox.items[NogyoItemDB.getinstance().db[itemid]].ToString();

        renewStatus(itemid);
    }
    

    /* ステータス表示の更新 */
    public void renewStatus(string itemid)
    {
        GameObject panel = GameObject.Find("ItemExlpainStatusPanel");

        for(int i=0; i<6; i++)
        {
            int value = NogyoItemDB.getinstance().db[itemid].status.getValueByNum((NogyoItemStatus.STSVALUE)Enum.ToObject(typeof(NogyoItemStatus.STSVALUE), i));
            panel.transform.GetChild(i).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Nogyo/testtube/testtube_" + i)[value];
        }

    }

    public void cancel()
    {
        Destroy(gameObject);
    }
}
