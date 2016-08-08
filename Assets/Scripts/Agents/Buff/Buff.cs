using UnityEngine;
using System.Collections;

public abstract class Buff : System.Object 
{
    public int TurnCount;//回合数
    public Agent Spawner;//buff生成者
    public Agent Onwer;//buff拥有者
    //效果种类
    private E_BuffEffectType buffeffecttype;
    public E_BuffEffectType BuffEffectType
    {
        set { if (value != null) buffeffecttype = value; }
        get { return buffeffecttype; }
    }
    //Buff种类
    private E_BuffType bufftype;
    public E_BuffType BuffType
    {
        set { if (value != null) bufftype = value; }
        get { return bufftype; }
    }

    //实例化
    protected Buff(E_BuffEffectType effecttype, Agent onwer,Agent spawner,int turncount)
    {
        TurnCount = turncount;
        Spawner = spawner;
        Onwer = onwer;
        buffeffecttype = effecttype;
    }




    //Buff生效的时候调用一次，放置Buff逻辑
    public abstract void Active();

    //Buf失效的时候 调用一次，放置Buff逻辑
    public abstract void Deactivate();


}
