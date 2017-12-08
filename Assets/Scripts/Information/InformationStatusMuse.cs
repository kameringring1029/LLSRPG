
using General;

/*
 * μ`sユニットのステータス定義
 * 継承元はInformationStatusParent.cs
 */

namespace Information
{

    public class Kotori_KY : statusTable
    {
        public override string graphic_id() { return "Kotori-KY"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "コトリ"; }
        public override string job() { return "賢者"; }
        public override string subname() { return "トワイライトタイガー"; }
        public override int level() { return 28; }
        public override int movable() { return 3; }
        public override int reach() { return 2; }
        public override int hp() { return 18; }
        public override int attack_phy() { return 5; }
        public override int guard_phy() { return 8; }
        public override int attack_magic() { return 24; }
        public override int guard_magic() { return 28; }
        public override int agility() { return 18; }
        public override int luck() { return 15; }

    }



    public class Eli_DS : statusTable
    {
        public override string graphic_id() { return "Eli-DS"; }
        public override string job_id() { return "Pirates"; }
        public override string name() { return "エリ"; }
        public override string job() { return "海賊"; }
        public override string subname() { return "Dancing Stars on Me"; }
        public override int level() { return 33; }
        public override int movable() { return 5; }
        public override int reach() { return 2; }
        public override int hp() { return 28; }
        public override int attack_phy() { return 25; }
        public override int guard_phy() { return 18; }
        public override int attack_magic() { return 14; }
        public override int guard_magic() { return 8; }
        public override int agility() { return 14; }
        public override int luck() { return 14; }


        public override MOVETYPE movetype() { return MOVETYPE.SWIM; }
    }


    public class Umi_DG : statusTable
    {
        public override string graphic_id() { return "Umi-DG"; }
        public override string job_id() { return "Archer"; }
        public override string name() { return "ウミ"; }
        public override string job() { return "アーチャー"; }
        public override string subname() { return "道着"; }
        public override int level() { return 37; }
        public override int movable() { return 5; }
        public override int reach() { return 2; }
        public override int hp() { return 33; }
        public override int attack_phy() { return 22; }
        public override int guard_phy() { return 21; }
        public override int attack_magic() { return 9; }
        public override int guard_magic() { return 15; }
        public override int agility() { return 28; }
        public override int luck() { return 8; }

        
    }


    public class Rin_HN : statusTable
    {
        public override string graphic_id() { return "Rin-HN"; }
        public override string job_id() { return "Pirates"; }
        public override string name() { return "リン"; }
        public override string job() { return "ニンジャ"; }
        public override string subname() { return "星空忍法"; }
        public override int level() { return 23; }
        public override int movable() { return 5; }
        public override int reach() { return 2; }
        public override int hp() { return 22; }
        public override int attack_phy() { return 15; }
        public override int guard_phy() { return 13; }
        public override int attack_magic() { return 4; }
        public override int guard_magic() { return 11; }
        public override int agility() { return 30; }
        public override int luck() { return 25; }

    }

    public class Rin_LB : statusTable
    {
        public override string graphic_id() { return "Rin-LB"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "リン"; }
        public override string job() { return "？"; }
        public override string subname() { return "Love wing bell"; }
        public override int level() { return 1; }
        public override int movable() { return 4; }
        public override int reach() { return 5; }
        public override int hp() { return 4; }
        public override int attack_phy() { return 0; }
        public override int guard_phy() { return 3; }
        public override int attack_magic() { return 26; }
        public override int guard_magic() { return 8; }
        public override int agility() { return 9; }
        public override int luck() { return 50; }

    }

    public class Hanayo_LB : statusTable
    {
        public override string graphic_id() { return "Hanayo-LB"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "ハナヨ"; }
        public override string job() { return "？"; }
        public override string subname() { return "Love wing bell"; }
        public override int level() { return 1; }
        public override int movable() { return 4; }
        public override int reach() { return 1; }
        public override int hp() { return 4; }
        public override int attack_phy() { return 0; }
        public override int guard_phy() { return 3; }
        public override int attack_magic() { return 26; }
        public override int guard_magic() { return 8; }
        public override int agility() { return 9; }
        public override int luck() { return 50; }

    }


}

