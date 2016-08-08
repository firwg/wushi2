using UnityEngine;
using System.Collections;
/// <summary>
/// 攻击力增加50%
/// </summary>
public class BuffIncreaseATK : Buff
{
    public BuffIncreaseATK(Agent onwer, Agent spawner, int turncount) : base(E_BuffEffectType.E_Buff_IncreaseATK, onwer, spawner,turncount) { }



    public override void Active()
    {
        Onwer.BlackBoard.Test_Temp_ATK += Onwer.BlackBoard.Test_Basic_ATK * 0.5f;
    }

    public override void Deactivate()
    {
        Onwer.BlackBoard.Test_Temp_ATK -= Onwer.BlackBoard.Test_Basic_ATK * 0.5f;
    }


}

