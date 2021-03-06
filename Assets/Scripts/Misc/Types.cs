using UnityEngine;
using System.Collections;

//硬件类型
public enum E_HardwareType
{
    iPhone3G = 0,
    iPhone4G = 1,
    iPad = 2,
    Max = 3,
}



public enum E_WeaponType 
{
	None = -1,
	Katana = 0,//刀
	Body,//身体
    Bow,//弓箭
	Max,//
}

//阻挡状态
public enum E_BlockState
{
    None = -1,
    Start = 0,
    Loop,
    End,
    HitBlocked,
    Failed
}

public enum E_KnockdownState
{
    None = -1,
    Down = 0,
    Loop,
    Up,
    Fatality,
}


//武器状态
public enum E_WeaponState
{
    NotInHands,//不在手上
	Ready,//准备着
	Attacking,//攻击中
	Reloading,//回收ing
	Empty,//空
}


//攻击类型
public enum E_AttackType
{
	None = -1,
	X = 0,
	O = 1,
    BossBash = 2,//Boss猛击
    Fatality = 3,//致命性
    Counter = 4,//对立的
    Berserk = 5,//狂暴的
	Max = 6,
}
    
//敌人类型
public enum E_EnemyType
{
	None = -1,
    SwordsMan = 0,//剑客
    Peasant = 1,//农民
    TwoSwordsMan = 2,//双剑客
    Bowman = 3,//弓箭手
    PeasantLow = 4,//矮农
    MiniBoss01 = 5,//小boss
    SwordsManLow = 6,//矮剑客
    notUsed03 = 7,
    notUsed04 = 8,
    notUsed05 = 9,
    BossOrochi = 10,//大Boss
	Max
}

//游戏状态
public enum E_GameState
{
	MainMenu,//主菜单
	IngameMenu,//游戏菜单
	Game,//游戏
	SaleScreen,
    Tutorial,//辅助
    Shop,//商店
}


//游戏模式
public enum E_GameType
{
	SinglePlayer,//单人游戏
    ChapterOnly,//剧情
	Survival,//生存模式
    FirstTimeTutorial,
    Tutorial,
    SaleScreen,
}

//游戏难度
public enum E_GameDifficulty
{
    Easy,
	Normal,
	Hard,
}


//伤害类型
public enum E_DamageType
{
    Front,//正面
    Back,//后背
    BreakBlock,//破防
    InKnockdown,//由上至下的猛击
    Enviroment,//周围
}

//暴击种类
public enum E_CriticalHitType
{
    None,
    Vertical,//垂直
	Horizontal,//水平
}

//死亡姿势
public enum E_DeadBodyType
{
    None = -1,
    Legs = 0,//大腿
    Beheaded,//头颅
    HalfBody,//上下两半
    SliceFrontBack,//前后对切
    SliceLeftRight,//左右对切
    Max,
}

public enum E_ComboLevel
{
	One = 1,
	Two = 2,
    Three = 3,
    Max = 3
}

public enum E_ComboLevelPrice
{
    One = 0,
    Two = 1000,
    Three = 2000
}

public enum E_SwordLevel
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Max = 5
}

public enum E_SwordLevelPrice
{
    One = 0,
    Two = 1000,
    Three = 1500,
    Four = 2000,
    Five = 3000,
}

public enum E_HealthLevel
{
    One = 1,
    Two = 2,
    Three = 3,
    Max = 3
}

public enum E_HealtLevelPrice
{
    One = 0,
    Two = 1500,
    Three = 3000,
}


//旋转类型
public enum E_RotationType
{
    Left,
    Right
}


//运动类型
public enum E_MotionType
{
	None,
	Walk,//行走
	Run,//跑步
    Sprint,//全速短跑
    Roll,//滚动
    Attack,//攻击
    Block,//阻挡
    BlockingAttack,//阻挡时遭受攻击
    Injury,//受伤
    Knockdown,//由上至下猛击
    Death,//死亡
    AnimationDrive,//动画驱动
}


//移动方向
public enum E_MoveType
{
    None,
    Forward,
    Backward,
    StrafeLeft,
    StrafeRight,
}

public enum E_LookType
{
    None,
    TrackTarget,//跟踪目标
}

//互动物体
public enum E_InteractionObjects
{
    None,
    UseLever,
    Trigger,
    UseExperience,
    TriggerAnim,
}

//互动种类
public enum E_InteractionType
{
    None,
    On,
    Off
}

//事件种类
public enum E_EventTypes
{
    None,
    EnemyStep,
    EnemySee,
    EnemyLost,
    Hit,
    Died,
    ImInPain,
    HitBlocked,
    Knockdown,
    FriendInjured,
}

//方向种类
public enum E_Direction
{
    Forward,
    Backward,
    Left,
    Right,
    Up,
    Down
}

public enum E_BuffType
{
    E_BeneficialEffectType,//增益buff
    E_HarmfulEffectType,//减益buff
}

public enum E_TimeNodeType
{
    E_StandardDGMCalculate,//标准伤害计算时


}



public enum E_BuffEffectType
{
    E_Buff_IncreaseATK,//增加攻击力，生效时刻：Buff添加的时候。生效逻辑：临时增加攻击力。
    E_Buff_IncreaseATKSPD,//增加攻击速度，生效时刻：生成的时候。生效逻辑：增加攻击速度。
    E_Buff_IncreaseDEF,//增加防御，生效时刻：生成的时候。生效逻辑：增加防御力。
    E_Buff_Immunity,//免疫，生效时刻：被命中debuff计算命中率的时候。生效逻辑：命中率为0。
    E_Buff_ReflectDMG,//反射伤害，反射30%伤害。生效时刻：每次被攻击受到伤害的时候。生效逻辑：给攻击者造成30%的伤害。
    E_Buff_IncreaseCRIRate,//增加暴击几率，生效时刻：生成的时候。生效逻辑：增加暴击率。
    E_Buff_Recovery,//恢复 ，生效时刻：每次获得回合的时候。生效逻辑：自身恢复10%上限的体力。
    E_Buff_Shield,//护盾，生效时刻：每次被攻击计算最终伤害的时候。生效逻辑：护盾量抵扣伤害。
    E_Buff_Counter,//反击，生效时刻：每次被具有攻击行为的技能作为目标之后如果还活着。生效逻辑：对攻击者使用1技能。
    E_Buff_Invincible,//无敌，生效时刻：每次被攻击计算最终伤害的时候。生效逻辑：伤害为0
    E_Buff_IncreaseCRIResist,//降低被暴击几率，生效时刻：每次被攻击计算标准伤害的时候。生效逻辑：减少暴击几率
    E_Buff_Endure,//对峙，生效时刻：最终伤害生效之后，死亡之前。生效逻辑：根据最后生命量计算实际伤害量。
    E_Buff_Denfend,//护卫，生效时刻：最终伤害生效之前。生效逻辑：转移最终伤害至buff的生成者。
    E_Buff_ProtectSoul,//灵魂保护，生效时刻：死亡之后。生效逻辑：复活。


    //
    E_DIVIDE,

    E_Debuff_Stun,//晕眩，生效时刻：获得回合之时。生效逻辑：取消本次当前回合。
    E_Debuff_Freeze,//冰冻，生效时刻：获得回合之时。生效逻辑：取消本次当前回合。
    E_Debuff_DecreaseDEF,//降低防御
    E_Debuff_DecreaseATK,//降低攻击
    E_Debuff_ContinuousDMG,//持续伤害
    E_Debuff_DecreaseATKSPD,//降低攻击速度
    E_Debuff_Sleep,//睡眠
    E_Debuff_IncreasedChancesOfMissing,//提高未击中几率
    E_Debuff_Unrecoverable,//禁止恢复体力
    E_Debuff_Bomb,//炸弹
    E_Debuff_Provoke,//挑衅
    E_Debuff_BeneficialEffectsBlocked,//禁止增益buff
    E_Debuff_Silence,//禁止使用主动技能
    E_Debuff_Brand,//烙印 增加25%伤害
    E_Debuff_Oblivion,//遗忘
}






class Types
{

}