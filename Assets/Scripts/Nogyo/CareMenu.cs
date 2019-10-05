using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareMenu : MonoBehaviour
{
    public string selected { get; private set; }

    public void Activate(NogyoMgr.CAREMENU care, ItemBox itembox)
    {
        selected = "";

        // てすとボタンリスト制作
        ButtonList.buttonStrExecWrapper carefunc = selectItem;
        List<string> strlist = new List<string>();
        strlist.Add("Seed_GMary"); strlist.Add("Seed_WClover");
        ButtonList.setButtonList(strlist, carefunc);


    }

    public void selectItem(string itemstr)
    {
        selected = itemstr;
    }
    public void cancel()
    {
        selected = "END";
    }
}
