using UnityEngine;
using System.Collections;

public class BuffImmunity : Buff {

    public BuffImmunity(Agent onwer, Agent spawner, int turncount) : base(E_BuffEffectType.E_Buff_Immunity, onwer, spawner, turncount) { }



    public override void Active()
    {
        
    }

    public override void Deactivate()
    {
        
    }
}
