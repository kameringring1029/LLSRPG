using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;


/*
 * ユニットステータス参照用のユーティリティ
 */

namespace Information
{

    public class UnitStatusUtil
    {
        //-- ユニットIDからステータスを呼び出す --
        public static statusTable search(int unitid)
        {

            switch (unitid)
            {
                case 2:
                    return new Eli_DS();
                case 4:
                    return new Umi_DG();
                case 5:
                    return new Rin_HN();
                case 8:
                    return new Hanayo_LB();

                case 11:
                    return new Chika_GF();
                case 12:
                    return new Riko_GF();
                case 15:
                    return new You_GF();
                case 16:
                    return new Yohane_JA();
                case 13:
                    return new Kanan_TT();

            }

            return new Chika_GF();
        }



        //-- ユニットIDからステータスの説明文を生成 --//
        public static string outputUnitInfo(int unitid)
        {
            statusTable unit = search(unitid);

            string hp = string.Format("<color=yellow>HP</color> {0,3}", unit.hp());
            string movable = string.Format("<color=yellow>MOV</color>{0,3}", unit.movable());
            string reach = string.Format("<color=yellow>RNG</color>{0,3}", unit.reach());
            string attack_phy = string.Format("<color=yellow>ATK</color>{0,3}", unit.attack_phy());
            string guard_phy = string.Format("<color=yellow>DEF</color>{0,3}", unit.guard_phy());
            string attack_magic = string.Format("<color=yellow>MAT</color>{0,3}", unit.attack_magic());
            string guard_magic = string.Format("<color=yellow>MDF</color>{0,3}", unit.guard_magic());
            string agility = string.Format("<color=yellow>AGL</color>{0,3}", unit.agility());
            string luck = string.Format("<color=yellow>LCK</color>{0,3}", unit.luck());

            string outinfo =
                "<b>" + unit.name() + "</b>" + " <size=20>(" + unit.subname() + ")" + "\n" + 
                " 【" + unit.job() + "  Lv: " + unit.level() + "】\n\n" +
                " 「" + unit.description() + "」</size>\n\n " +
                hp + "   " + movable + "   " + reach + "\n " +
               attack_phy + "   " + guard_phy + "   " + agility + "\n " +
               attack_magic + "   " + guard_magic + "   " + luck + "\n\n" +
            " <sprite=\"Aqours\" index=0> " + " <sprite=\"CYR\" index=0>";

            return outinfo;
        }


        public static GameObject searchJobPrefab(string jobid)
        {
            return Resources.Load<GameObject>("Prefab/Units/"+jobid);
        }

    }
}
