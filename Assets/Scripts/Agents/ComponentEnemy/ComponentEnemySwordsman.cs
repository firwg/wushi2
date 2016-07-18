using UnityEngine;using System.Collections;[RequireComponent(typeof(Agent))][RequireComponent(typeof(Animation))][RequireComponent(typeof(AnimSetEnemySwordsMan))][RequireComponent(typeof(AudioSource))][RequireComponent(typeof(AnimComponent))]
[RequireComponent(typeof(SensorEyes))]

public class ComponentEnemySwordsman : MonoBehaviour, IActionHandler{	Agent Owner;
    public Agent Agent { get { return Owner; } }    void Awake()    {        Owner = GetComponent("Agent") as Agent;
    }

    void Start()
    {
        // Agent.AddGOAPAction(E_GOAPAction.E_GOTO);
        Agent.AddGOAPAction(E_GOAPAction.gotoMeleeRange);
        Agent.AddGOAPAction(E_GOAPAction.combatMoveRight);
        Agent.AddGOAPAction(E_GOAPAction.combatMoveLeft);
        Agent.AddGOAPAction(E_GOAPAction.combatMoveForward);
        Agent.AddGOAPAction(E_GOAPAction.combatMoveBackward);
        Agent.AddGOAPAction(E_GOAPAction.combatRunForward);
        Agent.AddGOAPAction(E_GOAPAction.combatRunBackward);
        Agent.AddGOAPAction(E_GOAPAction.lookatTarget);
        Agent.AddGOAPAction(E_GOAPAction.weaponShow);
        Agent.AddGOAPAction(E_GOAPAction.weaponHide);
        Agent.AddGOAPAction(E_GOAPAction.attackMeleeOnce);
        Agent.AddGOAPAction(E_GOAPAction.attackCounter);
        Agent.AddGOAPAction(E_GOAPAction.block);
        Agent.AddGOAPAction(E_GOAPAction.playAnim);
        Agent.AddGOAPAction(E_GOAPAction.tount);
        Agent.AddGOAPAction(E_GOAPAction.injury);
        Agent.AddGOAPAction(E_GOAPAction.knockdown);
        Agent.AddGOAPAction(E_GOAPAction.death);

        //Agent.AddGOAPGoal(E_GOAPGoals.E_GOTO);
        Agent.AddGOAPGoal(E_GOAPGoals.E_COMBAT_MOVE_RIGHT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_COMBAT_MOVE_LEFT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_COMBAT_MOVE_FORWARD);
        Agent.AddGOAPGoal(E_GOAPGoals.E_COMBAT_MOVE_BACKWARD);
        Agent.AddGOAPGoal(E_GOAPGoals.E_LOOK_AT_TARGET);
        Agent.AddGOAPGoal(E_GOAPGoals.E_KILL_TARGET);
        Agent.AddGOAPGoal(E_GOAPGoals.E_DO_BLOCK);
        Agent.AddGOAPGoal(E_GOAPGoals.E_ALERT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_CALM);
        Agent.AddGOAPGoal(E_GOAPGoals.E_USE_WORLD_OBJECT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_PLAY_ANIM);
        Agent.AddGOAPGoal(E_GOAPGoals.E_IDLE_ANIM);
        Agent.AddGOAPGoal(E_GOAPGoals.E_REACT_TO_DAMAGE);

        Agent.InitializeGOAP();

        Owner.BlackBoard.ActionHandlerAdd(this);
        //Owner.debugGOAP = true;
        //Owner.debugAnims = true;
    }

    void FixedUpdate()
    {
        //Debug.Log("SWORD Rage " + Owner.BlackBoard.Rage + " Fear " + Owner.BlackBoard.Fear + " Dodge " + Owner.BlackBoard.Dodge + " Berserk " + Owner.BlackBoard.Berserk);

        Agent.BlackBoard.UpdateCombatSetting();
    }

    public void HandleAction(AgentAction a)
    {
        Owner.BlackBoard.UpdateCombatSetting(a);
    }    public void StopMove(bool stop)    {         }    void Activate(Transform t)    {
        Owner.BlackBoard.Rage = Owner.BlackBoard.RageMin;

        Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);

        Owner.WorldState.SetWSProperty(E_PropKey.E_IDLING, true);
        Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        Owner.WorldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_LOOKING_AT_TARGET, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_USE_WORLD_OBJECT, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM, false);

        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_DODGE, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_BLOCK, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_COMBAT_RANGE, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, false);
        Owner.WorldState.SetWSProperty(E_PropKey.MoveToRight, false);
        Owner.WorldState.SetWSProperty(E_PropKey.MoveToLeft, false);

        Owner.WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.None);    }    void Deactivate()    {    }}