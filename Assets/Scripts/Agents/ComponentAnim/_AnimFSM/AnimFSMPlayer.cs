using UnityEngine;
using System.Collections;

// add enum
// add new  - in Initialize
// add if  - in DoAction

public class AnimFSMPlayer: AnimFSM
{
	enum E_MyAnimState
	{
		E_IDLE,
		E_GOTO,
        E_MOVE,
		E_ATTACK_MELEE,
        E_ROLL,
        E_USE_LEVER,
        Teleport,
        E_INJURY,
        E_DEATH,
	}

	public AnimFSMPlayer(Animation anims, Agent owner) : base(anims, owner) { }

	public override void Initialize()
	{
		AnimStates.Add(new AnimStateIdle(AnimEngine, Owner)); //E_MyAnimState.E_IDLE
		AnimStates.Add(new AnimStateGoTo(AnimEngine, Owner)); //E_MyAnimState.E_GOTO
        AnimStates.Add(new AnimStateMove(AnimEngine, Owner)); //E_MyAnimState.E_MOVE
		AnimStates.Add(new AnimStateAttackMelee(AnimEngine, Owner)); //E_MyAnimState.E_ATTACK_MELEE
        AnimStates.Add(new AnimStateRoll(AnimEngine, Owner)); //E_MyAnimState.E_ROLL
        AnimStates.Add(new AnimStateUseLever(AnimEngine, Owner)); //use lever
        AnimStates.Add(new AnimStateTeleport(AnimEngine, Owner)); // teleport
        AnimStates.Add(new AnimStateInjury(AnimEngine, Owner)); //E_MyAnimState.E_INJURY
        AnimStates.Add(new AnimStateDeath(AnimEngine, Owner)); //E_MyAnimState._EDEATHM

		DefaultAnimState = AnimStates[(int)E_MyAnimState.E_IDLE];
		base.Initialize();
	}

	public override void DoAction(AgentAction action)
	{
		if (CurrentAnimState.HandleNewAction(action) == true)
		{
			//Debug.Log("AC - Do Action " + action.ToString());
			NextAnimState = null;
		}
		else
		{
            if (action is AgentActionGoTo)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_GOTO];
            if (action is AgentActionMove)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_MOVE];
            else if (action is AgentActionAttack)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_ATTACK_MELEE];
            else if (action is AgentActionRoll)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_ROLL];
            else if (action is AgentActionWeaponShow)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_IDLE];
            else if (action is AgentActionUseLever)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_USE_LEVER];
            else if (action is AgentActionTeleport)
                NextAnimState = AnimStates[(int)E_MyAnimState.Teleport];
            else if (action is AgentActionInjury)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_INJURY];
            else if (action is AgentActionDeath)
                NextAnimState = AnimStates[(int)E_MyAnimState.E_DEATH];

            if(NextAnimState != null)
                ProgressToNextStage(action);
		}
	}
}