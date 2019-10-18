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

    
    /*説明分と画像を更新*/
    public void renewExplain(string itemid)
    {
        Debug.Log("selectitem" + itemid);
        GameObject.Find("ItemExplainView").GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Nogyo/item/" + NogyoItemDB.getinstance().db[itemid].id);
        GameObject.Find("ItemExplainText").GetComponent<TextMeshProUGUI>().text = NogyoItemDB.getinstance().db[itemid].shapingExplain();
    }

    public void cancel()
    {
        Destroy(gameObject);
    }
}
