using UnityEngine;
using System.Collections;

public class BuffIncreaseDEF : Buff {

    public BuffIncreaseDEF(Agent onwer, Agent spawner, int turncount) : base(E_BuffEffectType.E_Buff_IncreaseDEF, onwer, spawner, turncount) { }



    public override void Active()
    {
        Onwer.BlackBoard.Test_Temp_DEF += Onwer.BlackBoard.Test_Basic_DEF * 0.5f;
    }

    public override void Deactivate()
    {
        Onwer.BlackBoard.Test_Temp_DEF -= Onwer.BlackBoard.Test_Basic_DEF * 0.5f;
    }
}
