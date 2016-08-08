using UnityEngine;
using System.Collections;

//Ӳ������
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
	Katana = 0,//��
	Body,//����
    Bow,//����
	Max,//
}

//�赲״̬
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


//����״̬
public enum E_WeaponState
{
    NotInHands,//��������
	Ready,//׼����
	Attacking,//������
	Reloading,//����ing
	Empty,//��
}


//��������
public enum E_AttackType
{
	None = -1,
	X = 0,
	O = 1,
    BossBash = 2,//Boss�ͻ�
    Fatality = 3,//������
    Counter = 4,//������
    Berserk = 5,//�񱩵�
	Max = 6,
}
    
//��������
public enum E_EnemyType
{
	None = -1,
    SwordsMan = 0,//����
    Peasant = 1,//ũ��
    TwoSwordsMan = 2,//˫����
    Bowman = 3,//������
    PeasantLow = 4,//��ũ
    MiniBoss01 = 5,//Сboss
    SwordsManLow = 6,//������
    notUsed03 = 7,
    notUsed04 = 8,
    notUsed05 = 9,
    BossOrochi = 10,//��Boss
	Max
}

//��Ϸ״̬
public enum E_GameState
{
	MainMenu,//���˵�
	IngameMenu,//��Ϸ�˵�
	Game,//��Ϸ
	SaleScreen,
    Tutorial,//����
    Shop,//�̵�
}


//��Ϸģʽ
public enum E_GameType
{
	SinglePlayer,//������Ϸ
    ChapterOnly,//����
	Survival,//����ģʽ
    FirstTimeTutorial,
    Tutorial,
    SaleScreen,
}

//��Ϸ�Ѷ�
public enum E_GameDifficulty
{
    Easy,
	Normal,
	Hard,
}


//�˺�����
public enum E_DamageType
{
    Front,//����
    Back,//��
    BreakBlock,//�Ʒ�
    InKnockdown,//�������µ��ͻ�
    Enviroment,//��Χ
}

//��������
public enum E_CriticalHitType
{
    None,
    Vertical,//��ֱ
	Horizontal,//ˮƽ
}

//��������
public enum E_DeadBodyType
{
    None = -1,
    Legs = 0,//����
    Beheaded,//ͷ­
    HalfBody,//��������
    SliceFrontBack,//ǰ�����
    SliceLeftRight,//���Ҷ���
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


//��ת����
public enum E_RotationType
{
    Left,
    Right
}


//�˶�����
public enum E_MotionType
{
	None,
	Walk,//����
	Run,//�ܲ�
    Sprint,//ȫ�ٶ���
    Roll,//����
    Attack,//����
    Block,//�赲
    BlockingAttack,//�赲ʱ���ܹ���
    Injury,//����
    Knockdown,//���������ͻ�
    Death,//����
    AnimationDrive,//��������
}


//�ƶ�����
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
    TrackTarget,//����Ŀ��
}

//��������
public enum E_InteractionObjects
{
    None,
    UseLever,
    Trigger,
    UseExperience,
    TriggerAnim,
}

//��������
public enum E_InteractionType
{
    None,
    On,
    Off
}

//�¼�����
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

//��������
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
    E_BeneficialEffectType,//����buff
    E_HarmfulEffectType,//����buff
}


public enum E_BuffEffectType
{
    E_Buff_IncreaseATK,//���ӹ���������Чʱ�̣����ɵ�ʱ����Ч�߼������ӹ�������
    E_Buff_IncreaseATKSPD,//���ӹ����ٶȣ���Чʱ�̣����ɵ�ʱ����Ч�߼������ӹ����ٶȡ�
    E_Buff_IncreaseDEF,//���ӷ�������Чʱ�̣����ɵ�ʱ����Ч�߼������ӷ�������
    E_Buff_Immunity,//���ߣ���Чʱ�̣�������debuff���������ʵ�ʱ����Ч�߼���������Ϊ0��
    E_Buff_ReflectDMG,//�����˺�������30%�˺�����Чʱ�̣�ÿ�α������ܵ��˺���ʱ����Ч�߼��������������30%���˺���
    E_Buff_IncreaseCRIRate,//���ӱ������ʣ���Чʱ�̣����ɵ�ʱ����Ч�߼������ӱ����ʡ�
    E_Buff_Recovery,//�ָ� ����Чʱ�̣�ÿ�λ�ûغϵ�ʱ����Ч�߼�������ָ�10%���޵�������
    E_Buff_Shield,//���ܣ���Чʱ�̣�ÿ�α��������������˺���ʱ����Ч�߼����������ֿ��˺���
    E_Buff_Counter,//��������Чʱ�̣�ÿ�α����й�����Ϊ�ļ�����ΪĿ��֮����������š���Ч�߼����Թ�����ʹ��1���ܡ�
    E_Buff_Invincible,//�޵У���Чʱ�̣�ÿ�α��������������˺���ʱ����Ч�߼����˺�Ϊ0
    E_Buff_IncreaseCRIResist,//���ͱ��������ʣ���Чʱ�̣�ÿ�α����������׼�˺���ʱ����Ч�߼������ٱ�������
    E_Buff_Endure,//���ţ���Чʱ�̣������˺���Ч֮ǰ����Ч�߼��������������������ʵ���˺�����
    E_Buff_Denfend,//��������Чʱ�̣������˺���Ч֮ǰ����Ч�߼���ת�������˺���buff�������ߡ�
    E_Buff_ProtectSoul,//��걣������Чʱ�̣�����֮����Ч�߼������

    E_Debuff_Stun,//��ѣ
    E_Debuff_Freeze,//����
    E_Debuff_DecreaseDEF,//���ͷ���
    E_Debuff_DecreaseATK,//���͹���
    E_Debuff_ContinuousDMG,//�����˺�
    E_Debuff_DecreaseATKSPD,//���͹����ٶ�
    E_Debuff_Sleep,//˯��
    E_Debuff_IncreasedChancesOfMissing,//���δ���м���
    E_Debuff_Unrecoverable,//��ֹ�ָ�����
    E_Debuff_Bomb,//ը��
    E_Debuff_Provoke,//����
    E_Debuff_BeneficialEffectsBlocked,//��ֹ����buff
    E_Debuff_Silence,//��ֹʹ����������
    E_Debuff_Brand,//��ӡ ����25%�˺�
    E_Debuff_Oblivion,//����
}






class Types
{

}