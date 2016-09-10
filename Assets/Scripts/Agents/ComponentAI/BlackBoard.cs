/***************************************************************
 * Class Name :	Blackboard
 * Function   : Central memory for GOAPController and other subsystems. 
 * 
 * Created by : Marek Rabas
 *
 **************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public interface IActionHandler
{
    void HandleAction(AgentAction a); 
}

/// <summary>
/// 持有关键数据的引用，所以激活的AgentAction，每一帧对于不激活的agentaction进行回收
/// </summary>
[System.Serializable]
public class BlackBoard
{
    #region AGENT ACTIONS
    private List<AgentAction> m_ActiveActions = new List<AgentAction>();
    private List<IActionHandler> m_ActionHandlers = new List<IActionHandler>();

    [System.NonSerialized]
    public Agent Owner;
    [System.NonSerialized]
    public GameObject myGameObject;// { get { return myGameObject; } private set { myGameObject = value; } }
    #endregion

    #region STATES
    /////////////// STATES ////////////////////////////
    [System.NonSerialized]
    public bool _Stop = false;

    public bool Stop { get { return _Stop; } set { _Stop = value; if (IsPlayer) Player.Instance.StopMove(value); } }
    [System.NonSerialized]
    public E_MotionType MotionType = E_MotionType.None;
    [System.NonSerialized]
    public E_WeaponState WeaponState = E_WeaponState.NotInHands;
    [System.NonSerialized]
    public E_WeaponType WeaponSelected = E_WeaponType.Katana;
    [System.NonSerialized]
    public E_WeaponType WeaponToSelect = E_WeaponType.None;
    [System.NonSerialized]
    public bool IsPlayer = false;

    public float WeaponRange = 2;
    public float sqrWeaponRange { get { return WeaponRange * WeaponRange; } }


    public float CombatRange = 4;
    public float sqrCombatRange { get { return CombatRange * CombatRange; } }

    #endregion

    #region INIT STATS
    //////////////// INIT STATS /////////////////////////
    public float MaxSprintSpeed = 8;
    public float MaxRunSpeed = 4;
    public float MaxWalkSpeed = 1.5f;
    public float MaxCombatMoveSpeed = 1;
    public float MaxHealth = 100;
    public float MaxKnockdownTime = 4;
    #endregion

    #region SETTINGS
    ///////////////// SETTINGS /////////////////////////////
    public float SpeedSmooth = 2.0f;
    public float RotationSmooth = 2.0f;
    public float RotationSmoothInMove = 8.0f;
    public float RollDistance = 4.0f;
    public float MoveSpeedModifier = 1;

    public float DontAttackTimer = 2.0f;

    #endregion

    #region Damage settings
    // Damage settings
    public bool KnockDown = true;
    public bool KnockDownDamageDeadly = true;
    public bool Invulnerable = false;
    public bool CriticalAllowed = true;
    public bool DamageOnlyFromBack = false;
    public float CriticalHitModifier = 1;
    #endregion

    #region COMBAT SETTINGS
    /////////////// COMBAT SETTINGS ///////////////////////
    public float RageMin = 0; //0 = no attack
    public float RageMax = 0;// 100 % chance is do do attack
    public float RageModificator = 0;//per second
    public float DodgeMin = 0;
    public float DodgeMax = 0;
    public float DodgeModificator = 0; //per second
    public float FearMin = 0;
    public float FearMax = 0;
    public float FearModificator = 0; //per second
    public float BerserkMin = 0; //0 = no attack
    public float BerserkMax = 0;// 100 % chance is do do attack
    public float BerserkModificator = 0;//per second

    public float RageInjuryModificator = 0; // each injury increase rage
    public float DodgeInjuryModificator = 0;  // each injury increase dodge
    public float FearInjuryModificator = 0;
    public float BerserkInjuryModificator = 0;

    public float RageBlockModificator = 0; // each block increase rage
    public float FearBlockModificator = 0; // each block increase rage
    public float BerserkBlockModificator = 0; // each block increase rage

    public float DodgeAttackModificator = 0; // each attack increase rage
    public float FearAttackModificator = 0; // each attack increase rage
    public float BerserkAttackModificator = 0; // each attack increase rage

    //    public float FearFriendDeathModificator = 10;
    //    public float DodgeFriendDeathModificator = 0;
    //    public float RageFriendDeathModificator = 10;
    #endregion

    #region GOAP_Relevancy & Delay  Settings
    ///////////////// GOAP Settings /////////////////////////////
    public float GOAP_AlertRelevancy = 0.8f;
    public float GOAP_CalmRelevancy = 0.2f;
    public float GOAP_BlockRelevancy = 0.7f;
    public float GOAP_DodgeRelevancy = 0.9f;
    public float GOAP_GoToRelevancy = 0.5f;
    public float GOAP_CombatMoveBackwardRelevancy = 0.7f;
    public float GOAP_CombatMoveForwardRelevancy = 0.75f;
    public float GOAP_CombatMoveLeftRelevancy = 0.6f;
    public float GOAP_CombatMoveRightRelevancy = 0.6f;
    public float GOAP_LookAtTargetRelevancy = 0.7f;
    public float GOAP_KillTargetRelevancy = 0.8f;
    public float GOAP_PlayAnimRelevancy = 0.95f;
    public float GOAP_UseWorlObjectRelevancy = 0.9f;
    public float GOAP_ReactToDamageRelevancy = 1.0f;
    public float GOAP_IdleActionRelevancy = 0.4f;
    public float GOAP_TeleportRelevancy = 0.9f;

    public float GOAP_AlertDelay = 2.0f;
    public float GOAP_CalmDelay = 2.2f;
    public float GOAP_BlockDelay = 2.7f;
    public float GOAP_DodgeDelay = 5.0f;
    public float GOAP_GoToDelay = 0.5f;
    public float GOAP_CombatMoveDelay = 3.5f;
    public float GOAP_CombatMoveLeftDelay = 3.6f;
    public float GOAP_CombatMoveRightDelay = 3.6f;
    public float GOAP_LookAtTargetDelay = 0.4f;
    public float GOAP_KillTargetDelay = 2.8f;
    public float GOAP_PlayAnimDelay = 0.0f;
    public float GOAP_UseWorlObjectDelay = 5.0f;
    public float GOAP_CombatMoveBackwardDelay = 3.5f;
    public float GOAP_CombatMoveForwardDelay = 3.5f;
    public float GOAP_IdleActionDelay = 10;
    public float GOAP_TeleportDelay = 4;
    #endregion

    #region STATS
    ///////////////// STATS /////////////////////////////
    [System.NonSerialized]
    public float Speed = 0;
    [System.NonSerialized]
    public float Health = 30;

    //main AI parameters
    [System.NonSerialized]
    public float Rage = 0;
    [System.NonSerialized]
    public float Fear = 0;
    [System.NonSerialized]
    public float Dodge = 0;
    [System.NonSerialized]
    public float Berserk = 0;


    [System.NonSerialized]
    public float IdleTimer = 0;

    [System.NonSerialized]
    public Vector3 MoveDir;
    #endregion

    #region Aditional informations
    [System.NonSerialized]
    public Vector3 CameraDirection;
    [System.NonSerialized]
    public Vector3 DesiredPosition;
    [System.NonSerialized]
    public Vector3 DesiredDirection;
    [System.NonSerialized]
    public InteractionGameObject InteractionObject;
    [System.NonSerialized]
    public E_InteractionType Interaction;
    [System.NonSerialized]
    public string DesiredAnimation;
    [System.NonSerialized]
    public Agent DesiredTarget;
    [System.NonSerialized]
    public E_AttackType DesiredAttackType;
    [System.NonSerialized]
    public AnimAttackData DesiredAttackPhase;
    [System.NonSerialized]
    public Agent DesiredAttacker;

    [System.NonSerialized]
    public Agent Attacker;
    [System.NonSerialized]
    public E_WeaponType AttackerWeapon;
    [System.NonSerialized]
    public E_DamageType DamageType;
    [System.NonSerialized]
    public Vector3 Impuls;
    [System.NonSerialized]
    public E_LookType LookType;
    [System.NonSerialized]
    public E_MoveType MoveType;
    [System.NonSerialized]
    public float DistanceToTarget;
    [System.NonSerialized]
    public Teleport TeleportDestination;
    [System.NonSerialized]
    public Agent DangerousFriend;
    [System.NonSerialized]
    public float DistanceToDangerousFriend;

    [System.NonSerialized]
    public bool DontUpdate = true;
    [System.NonSerialized]
    public bool ReactOnHits = true;
    #endregion

    #region Test Value
    public int Monster_Level;
    public float Monster_Basic_HP;
    public float Monster_Temp_HP;

    //ATK
    public float Monster_Basic_ATK;
    public float Monster_Temp_ATK;

    //DEF
    public float Test_Basic_DEF;
    public float Test_Temp_DEF;

    //ATKSPD
    public float Test_Basic_ATKSPD;
    public float Test_Temp_ATKSPD;
    
    public float Test_Basic_CRI_Rate;
    public float Test_Temp_CRI_Rate;
    public float Test_Basic_CRI_Dmg;
    public float Test_Temp_CRI_Dmg;
    public float Test_Basic_Resistance;
    public float Test_Temp_Resistance;
    public float Test_Basic_Accuracy;
    public float Test_Temp_Accuracy;
    #endregion


    public float RealMaxHealth
    {
        get
        {
            if (IsPlayer == false || Game.Instance.HealthLevel == E_HealthLevel.One)
                return MaxHealth;

            if (Game.Instance.HealthLevel == E_HealthLevel.Two)
                return MaxHealth + 25;

            return MaxHealth + 50;
        }
    }
    public void Reset()
    {
        m_ActiveActions.Clear();

        Stop = false;
        MotionType = E_MotionType.None;
        WeaponState = E_WeaponState.NotInHands;
        WeaponToSelect = E_WeaponType.None;

        Speed = 0;

        Health = RealMaxHealth;

        Rage = RageMin;
        Dodge = DodgeMin;
        Fear = FearMin;
        IdleTimer = 0;

        MoveDir = Vector3.zero;

        DesiredPosition = Vector3.zero;
        DesiredDirection = Vector3.zero;

        InteractionObject = null;
        Interaction = E_InteractionType.None;

        DesiredAnimation = "";

        DesiredTarget = null;
        DesiredAttackType = E_AttackType.None;

        DontUpdate = false;

    }

    #region ORDERS

    public bool IsOrderAddPossible(AgentOrder.E_OrderType orderType)
    {
        AgentOrder.E_OrderType currentOrder = (AgentOrder.E_OrderType)Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER).GetValue();

        if(orderType == AgentOrder.E_OrderType.E_DODGE && currentOrder != AgentOrder.E_OrderType.E_DODGE && currentOrder != AgentOrder.E_OrderType.E_USE)
            return true;
        else if (currentOrder != AgentOrder.E_OrderType.E_ATTACK && currentOrder != AgentOrder.E_OrderType.E_DODGE && currentOrder != AgentOrder.E_OrderType.E_USE)
            return true;
        else
            return false;
    }

    public void OrderAdd(AgentOrder order)
    {
        // Debug.Log(Time.timeSinceLevelLoad + " order arrived " + order.Type);

        if (IsOrderAddPossible(order.Type))
        {
            Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER, order.Type);
            switch (order.Type)
            {
                case AgentOrder.E_OrderType.E_STOPMOVE:
                    Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
                    DesiredPosition = Owner.Position;
                    break;
                case AgentOrder.E_OrderType.E_GOTO:
                    Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, false);
                    DesiredPosition = order.Position;
                    //Debug.Log("DesiredPosition="+ DesiredPosition);
                    //float van = Mathf.Atan2(2, 5);
                    CameraDirection.y = 0;
                   Quaternion q = Quaternion.FromToRotation(Vector3.forward, CameraDirection);
                    DesiredDirection = q*order.Direction;
                    Debug.Log("DesiredDirection=" + DesiredDirection);
                    MoveSpeedModifier = order.MoveSpeedModifier;
                    break;
                case AgentOrder.E_OrderType.E_DODGE:
                    Quaternion qq = Quaternion.FromToRotation(Vector3.forward, CameraDirection);
                    DesiredDirection = qq * order.Direction;
                    //Debug.Log(Time.timeSinceLevelLoad + " order arrived " + order.Type);
                    break;
                case AgentOrder.E_OrderType.E_USE:
                    Owner.WorldState.SetWSProperty(E_PropKey.E_USE_WORLD_OBJECT, true);

                    if ((order.Position - Owner.Position).sqrMagnitude <= 1)
                        Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
                    else
                        Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, false);
                    DesiredPosition = order.Position;
                    InteractionObject = order.InteractionObject;
                    Interaction = order.Interaction;
                    break;
                case AgentOrder.E_OrderType.E_ATTACK:
                    if (order.Target == null || (order.Target.Position - Owner.Position).magnitude <= (WeaponRange + 0.2f))
                        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, true);
                    else
                        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);

                    DesiredAttackType = order.AttackType;
                    DesiredTarget = order.Target;
                    Quaternion qqq = Quaternion.FromToRotation(Vector3.forward, CameraDirection);
                    DesiredDirection = qqq*order.Direction;
                    DesiredAttackPhase = order.AnimAttackData;
                    break;
            }

            //  Debug.Log(Time.timeSinceLevelLoad + " new order arrived " + order.Type);
        }
        else if (order.Type == AgentOrder.E_OrderType.E_ATTACK)
        {
            // Debug.Log(Time.timeSinceLevelLoad +  " " +order.Type + " is nto allowed because " + currentOrder);
        }
        AgentOrderFactory.Return(order);
    }

#endregion

    #region  ACTIONS

    public void ActionAdd(AgentAction action)
    {
        IdleTimer = 0;

        m_ActiveActions.Add(action);

        for (int i = 0; i < m_ActionHandlers.Count; i++)
            m_ActionHandlers[i].HandleAction(action);
    }

    public AgentAction ActionGet(int index)
    {
        return m_ActiveActions[index];
    }

    public AgentActionAttack ActionGetAttackAction()
    {
        for (int i = 0; i < m_ActiveActions.Count; i++)
            if (m_ActiveActions[i] is AgentActionAttack)
                return m_ActiveActions[i] as AgentActionAttack;

        return null;
    }

    public int ActionCount()
    {
        return m_ActiveActions.Count;
    }

    public void ActionHandlerAdd(IActionHandler handler)
    {
        for (int i = 0; i < m_ActionHandlers.Count; i++)
            if (m_ActionHandlers[i] == handler)
                return;

        m_ActionHandlers.Add(handler);
    }

    public void ActionHandlerRemove(IActionHandler handler)
    {
        m_ActionHandlers.Remove(handler);
    }


    public void Update()
    {
        IdleTimer += Time.deltaTime;


        //从引用中剔除已经完成的AgentAction的引用，在工厂中回收
        for (int i = 0; i < m_ActiveActions.Count; i++)
        {
            if (m_ActiveActions[i].IsActive())
                continue;

            ActionDone(m_ActiveActions[i]);
            m_ActiveActions.RemoveAt(i);

            return;
        }



        if(DesiredTarget && DesiredTarget.IsAlive == false)
            DesiredTarget = null;

        if (DesiredTarget == null)
            Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, true);
        else if ((DesiredTarget.Position - Owner.Position).magnitude <= (WeaponRange + 0.2f))
            Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, true);
        else
            Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);
    }

    private void ActionDone(AgentAction action)
    {
        #region  //
        /*if(action.IsSuccess())
        {
            /*if (action is AgentActionGoTo && (action as AgentActionGoTo).FinalPosition == DesiredPosition)
            {
                //Debug.Log(action.ToString() + "is done, setting E_AT_TARGET_POS to true"); 
                Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
            }
            else if (action is AgentActionWeaponShow)
            {
                //Debug.Log(action.ToString() + "is done, setting E_WEAPON_IN_HANDS to " + (action as AgentActionWeaponShow).Show.ToString()); 
                Owner.WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, (action as AgentActionWeaponShow).Show);
            }
            else if (action is AgentActionUseLever)
            {
                Owner.WorldState.SetWSProperty(E_PropKey.E_USE_WORLD_OBJECT, false);
                InteractionObject = null;
                Interaction = E_InteractionType.E_NONE;
                
            }
            else if (action is AgentActionPlayAnim)
            {
                Owner.WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM, false);
                DesiredAnimation = null;
            }
        }*/
        #endregion
        AgentActionFactory.Return(action);
    }


    public void SetFear(float value)
    {
        Fear = value;
        if (Fear > FearMax)
            Fear = FearMax;
        else if (Fear < FearMin)
            Fear = FearMin;
    }

    public void SetRage(float value)
    {
        Rage = value;
        if (Rage > RageMax)
            Rage = RageMax;
        else if (Rage < RageMin)
            Rage = RageMin;
    }

    public void SetBerserk(float value)
    {
        Berserk = value;
        if (Berserk > BerserkMax)
            Berserk = BerserkMax;
        else if (Berserk < BerserkMin)
            Berserk = BerserkMin;
    }

    public void SetDodge(float value)
    {
        Dodge = value;
        if (Dodge > DodgeMax)
            Dodge = DodgeMax;
        else if (Dodge < DodgeMin)
            Dodge = DodgeMin;
    }

    public void UpdateCombatSetting()
    {
        if (Game.Instance.GameDifficulty == E_GameDifficulty.Hard && IsPlayer == false)
        {
            SetRage(Rage + RageModificator * 1.2f * Time.fixedDeltaTime);
            SetBerserk(Berserk + BerserkModificator * 1.2f * Time.fixedDeltaTime);
        }
        else if (Game.Instance.GameDifficulty == E_GameDifficulty.Easy && IsPlayer == false)
        {
            SetRage(Rage + RageModificator * 0.8f * Time.fixedDeltaTime);
            SetBerserk(Berserk + BerserkModificator * 0.8f * Time.fixedDeltaTime);
        }
        else
        {
            SetRage(Rage + RageModificator * Time.fixedDeltaTime);
            SetBerserk(Berserk + BerserkModificator * Time.fixedDeltaTime);
        }


        if (DesiredTarget && Owner.WorldState.GetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY).GetValue() !=null)
            SetFear(Fear + FearModificator * Time.fixedDeltaTime);
        else
            SetFear(Fear - FearModificator * Time.fixedDeltaTime);

        if ((bool)Owner.WorldState.GetWSProperty(E_PropKey.E_IN_BLOCK).GetValue() != true)
            SetDodge(Dodge + Owner.BlackBoard.DodgeModificator * Time.fixedDeltaTime);
    }

    public void UpdateCombatSetting(AgentAction a)
    {
        if (a is AgentActionDamageBlocked)
        {
            if ((a as AgentActionDamageBlocked).BreakBlock)
            {
                SetFear(Fear + FearInjuryModificator * 0.5f);
                SetRage(Rage + RageInjuryModificator * 0.5f);
            }
            else
            {
                SetFear(Fear + FearBlockModificator);
                SetRage(Rage + RageBlockModificator);
                SetBerserk(Berserk + BerserkBlockModificator);

            }
        }
        else if (a is AgentActionInjury)
        {
            SetFear(Fear + FearInjuryModificator);
            SetRage(Rage + RageInjuryModificator);
            SetDodge(Dodge + DodgeInjuryModificator);
            SetBerserk(Berserk + BerserkInjuryModificator);
        }
        else if (a is AgentActionAttackWhirl || a is AgentActionAttackRoll)
        {
            SetBerserk(BerserkMin);
        }
        else if (a is AgentActionAttack)
        {
            SetRage(RageMin);// reset

            SetDodge(Dodge + DodgeAttackModificator);
            SetFear(Fear + FearAttackModificator);
            SetBerserk(Berserk + BerserkAttackModificator);
        }
        else if (a is AgentActionBlock) // reset
        {
            SetDodge(DodgeMin);
        }
        else if (a is AgentActioCombatMove) //reset
        {
            if ((a as AgentActioCombatMove).MoveType == E_MoveType.Backward)
                SetFear(Owner.BlackBoard.FearMin);
        }
    }
#endregion
}

