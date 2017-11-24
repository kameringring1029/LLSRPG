using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;


/*
 * ユニットのステータス定義のスーパークラス
 */

namespace Information
{

    public class UnitStatus {

        public string graphic_id = "";

        public string name = "";
        public string job = "";
        public string subname = "";

        // ステータスは{基のステ, 修正後のステ}
        public int[] level = new int[2];
        public int[] reach = new int[2];
        public int[] hp = new int[2];
        public int[] attack_phy = new int[2];
        public int[] guard_phy = new int[2];
        public int[] attack_magic = new int[2];
        public int[] guard_magic = new int[2];
        public int[] luck = new int[2];


        public int[] movable = new int[2];
        public MOVETYPE movetype;


        public UnitStatus(statusTable status)
        {
            this.graphic_id = status.graphic_id();

            this.name = status.name();
            this.job = status.job();
            this.subname = status.subname();

            // ステータスは{基のステ, 修正後のステ}
            for (int i = 0; i < 2; i++)
            {
                this.level[i] = status.level();
                this.reach[i] = status.reach();
                this.hp[i] = status.hp();
                this.attack_phy[i] = status.attack_phy();
                this.guard_phy[i] = status.guard_phy();
                this.attack_magic[i] = status.attack_magic();
                this.guard_magic[i] = status.guard_magic();
                this.luck[i] = status.luck();

                this.movable[i] = status.movable();
            }

            movetype = status.movetype();
        }

        public string outputInfo()
        {
            string outinfo =
                name + "\n" +
                " (" + subname + ")" + "\n" +
                job + "  Lv: " + level[1] + "\n" +
                "  HP: " + hp[1] + " / " + hp[0] + "\n\n" +
                "移動：" + movable[1] + "  射程：" + reach[1] + "\n" +
                "力：" + attack_phy[1] + "   防：" + guard_phy[1] + "\n" +
                "魔力：" + attack_magic[1] + "  魔防：" + guard_magic[1] + "\n" +
                "運：" + luck[1] + "";

            return outinfo;
        }
    }


    public class statusTable {
        public virtual string graphic_id() { return ""; }
        public virtual string job_id() { return ""; }
        public virtual string name() { return ""; }
        public virtual string job() { return ""; }
        public virtual string subname() { return ""; }
        public virtual int level() { return 0; }
        public virtual int movable() { return 0; }
        public virtual int reach() { return 0; }
        public virtual int hp() { return 0; }
        public virtual int attack_phy() { return 0; }
        public virtual int guard_phy() { return 0; }
        public virtual int attack_magic() { return 0; }
        public virtual int guard_magic() { return 0; }
        public virtual int luck() { return 0; }
        public virtual MOVETYPE movetype() { return MOVETYPE.WALK; }
    }




}
