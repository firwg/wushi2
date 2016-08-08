using UnityEngine;
using System.Collections;




/// <summary>
/// 增加30%攻击速度
/// </summary>
public class BuffIncreaseATKSPD : Buff
{
    public BuffIncreaseATKSPD(Agent onwer, Agent spawner, int turncount) : base(E_BuffEffectType.E_Buff_IncreaseATKSPD, onwer, spawner, turncount) { }



    public override void Active()
    {
        Onwer.BlackBoard.Test_Temp_ATKSPD += Onwer.BlackBoard.Test_Basic_ATKSPD * 0.3f;
    }

    public override void Deactivate()
    {
        Onwer.BlackBoard.Test_Temp_ATKSPD -= Onwer.BlackBoard.Test_Basic_ATKSPD * 0.3f;
    }
}
