using UnityEngine;
using System.Collections;

public abstract class Buff : System.Object 
{

    /// <summary>
    /// 回合数
    /// </summary>
    public int TurnCount;

    /// <summary>
    /// Buff生成者
    /// </summary>
    public Agent Spawner;

    /// <summary>
    /// Buff承受者
    /// </summary>
    public Agent Onwer;

    //Buff效果种类
    private E_BuffEffectType buffeffecttype;
    public E_BuffEffectType BuffEffectType
    {
        set { buffeffecttype = value; }
        get { return buffeffecttype; }
    }

    //Buff种类，增益还是减益
    private E_BuffType bufftype;
    public E_BuffType BuffType
    {
        set { bufftype = value; }
        get { return bufftype; }
    }



    //实例化
    protected Buff(E_BuffEffectType _effecttype, Agent _onwer,Agent _spawner,int _turncount)
    {
        TurnCount = _turncount;
        Spawner = _spawner;
        Onwer = _onwer;
        buffeffecttype = _effecttype;
        if ((int)_effecttype > (int)E_BuffEffectType.E_DIVIDE) BuffType = E_BuffType.E_HarmfulEffectType;
        else BuffType = E_BuffType.E_BeneficialEffectType;
    }



    /// <summary>
    /// Buff生效的时候调用一次，放置Buff逻辑
    /// </summary>
    public abstract void Active();


    /// <summary>
    /// Buff失效的时候调用一次，放置Buff逻辑
    /// </summary>
    public abstract void Deactivate();


}
