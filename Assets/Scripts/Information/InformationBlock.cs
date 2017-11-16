using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{

    public class BlockInfo
    {
        public virtual string type() { return ""; }
        public virtual string effect() { return "なし"; }
        public virtual Unit.GROUNDTYPE groundtype() { return Unit.GROUNDTYPE.NORMAL; }
    }



    public class Kusa : BlockInfo
    {
        public override string type() { return "草"; }
        public override Unit.GROUNDTYPE groundtype() { return Unit.GROUNDTYPE.NORMAL; }

    }

    public class Sea : BlockInfo
    {
        public override string type() { return "海"; }
        public override Unit.GROUNDTYPE groundtype() { return Unit.GROUNDTYPE.SEA; }

    }

    public class High : BlockInfo
    {
        public override string type() { return "障害物"; }
        public override Unit.GROUNDTYPE groundtype() { return Unit.GROUNDTYPE.HIGH; }

    }

    public class Unmovable : BlockInfo
    {
        public override string type() { return "進入禁止"; }
        public override Unit.GROUNDTYPE groundtype() { return Unit.GROUNDTYPE.UNMOVABLE; }

    }
}
