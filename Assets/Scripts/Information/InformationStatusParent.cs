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
        public int[] agility = new int[2];
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
                this.agility[i] = status.agility();
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
                "<color=yellow>  HP </color> " + hp[1] + " / " + hp[0] + "\n" +
                "<color=yellow>移動</color> " + movable[1] + "  <color=yellow>射程</color> " + reach[1] + "\n" +
                "<color=yellow>力</color>   " + attack_phy[1] + "   <color=yellow>防</color>   " + guard_phy[1] + "\n" +
                "<color=yellow>魔力</color> " + attack_magic[1] + "  <color=yellow>魔防</color> " + guard_magic[1] + "\n" +
                "<color=yellow>速さ</color> " + agility[1] + "  <color=yellow>運</color> " + luck[1] + "";

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
        public virtual int agility() { return 0; }
        public virtual int luck() { return 0; }
        public virtual MOVETYPE movetype() { return MOVETYPE.WALK; }

        public virtual string description() { return ""; }
        public virtual string status_description() { return ""; }

    }




}
