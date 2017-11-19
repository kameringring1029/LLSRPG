using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeMgr : MonoBehaviour {

    public GameObject selectUnitPanel;
    public GameObject musePanel;
    public GameObject aqoursPanel;


    public GameObject[] unitButtons = new GameObject[18];
    public GameObject[] unitButtonsArea = new GameObject[18];
    public GameObject[] selectedUnitArea = new GameObject[3];
    List<int> selectedUnits = new List<int>();


    private void Start()
    {
        selectUnitPanel.SetActive(true);

    }

    //--- 指定したユニットを選択中に ---//
    public void selectUnit(int unitid)
    {
        selectedUnits.Add(unitid);
        unitButtons[unitid-1].GetComponent<RectTransform>().position = selectedUnitArea[selectedUnits.Count-1].GetComponent<RectTransform>().position;
    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        selectedUnits.Remove(unitid);
        unitButtons[unitid-1].GetComponent<RectTransform>().position =unitButtonsArea[unitid - 1].GetComponent<RectTransform>().position;
        for(int i =0; i<selectedUnits.Count; i++)
        {
            unitButtons[selectedUnits[i]-1].GetComponent<RectTransform>().position = selectedUnitArea[i].GetComponent<RectTransform>().position;
        }

    }


    //--- ユニット選択タブの切り替え ---//
    public void displayMuse()
    {
        // musepanelを親の中で最前面に
        musePanel.transform.SetAsLastSibling();
    }
    public void displayAqours()
    {
        // aqourspanelを親の中で最前面に
        aqoursPanel.transform.SetAsLastSibling();
    }


    //--- SRPGスタート ---//
    // ユニット選択画面を消去、選択されたユニットをGameMgrに渡す
    public void startGame()
    {
        selectUnitPanel.SetActive(false);
        gameObject.GetComponent<GameMgr>().init(selectedUnits.ToArray());
    }

}
