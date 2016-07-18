using System;
using System.Collections.Generic;
using UnityEngine;


public static class AgentActionFactory
{
	public enum E_Type
	{
		E_IDLE,
        E_MOVE,
		E_GOTO,
        E_COMBAT_MOVE,
		E_ATTACK,
        E_ATTACK_ROLL,
        E_ATTACK_JUMP,
        E_ATTACK_WHIRL,
		E_INJURY,
        E_DAMAGE_BLOCKED,
        E_BLOCK,
		E_ROLL,
		E_INCOMMING_ATTACK,
		E_WEAPON_SHOW,
		Rotate,
        E_USE_LEVER,
        E_PLAY_ANIM,
        E_PLAY_IDLE_ANIM,
        E_DEATH,
        E_KNOCKDOWN,
        Teleport,
		E_COUNT
	}

	static private Queue<AgentAction>[] m_UnusedActions = new Queue<AgentAction>[(int)E_Type.E_COUNT];

	// DEBUG !!!!!!!
	static private List<AgentAction> m_ActionsInAction = new List<AgentAction>();
	

	static AgentActionFactory()
	{
		for (E_Type i = 0 ; i < E_Type.E_COUNT ; i++)
		{
			m_UnusedActions[(int)i] = new Queue<AgentAction>();
			//maybe we could precreate few of them ?
		}
	}

	static public AgentAction Create(E_Type type)
	{
		int index = (int)type;

		AgentAction a;
		if (m_UnusedActions[index].Count > 0)
		{
			a = m_UnusedActions[index].Dequeue();
		}
		else
		{
			switch (type)
			{
			    case E_Type.E_IDLE:
				    a = new AgentActionIdle();
				    break;
                case E_Type.E_MOVE:
                    a = new AgentActionMove();
                    break;
                case E_Type.E_GOTO:
				    a = new AgentActionGoTo();
				    break;
                case E_Type.E_COMBAT_MOVE:
                    a = new AgentActioCombatMove();
                    break;
			    case E_Type.E_ATTACK:
				    a = new AgentActionAttack();
				    break;
                case E_Type.E_ATTACK_ROLL:
                    a = new AgentActionAttackRoll();
                    break;
                case E_Type.E_ATTACK_WHIRL:
                    a = new AgentActionAttackWhirl();
                    break;
                case E_Type.E_INJURY:
				    a = new AgentActionInjury();
				    break;
                case E_Type.E_DAMAGE_BLOCKED:
                    a = new AgentActionDamageBlocked();
                    break;
                case E_Type.E_BLOCK:
                    a = new AgentActionBlock();
                    break;
			    case E_Type.E_ROLL:
				    a = new AgentActionRoll();
				    break;
			    case E_Type.E_INCOMMING_ATTACK:
				    a = new AgentActionIncommingAttack();
				    break;
			    case E_Type.E_WEAPON_SHOW:
				    a =  new AgentActionWeaponShow();
				    break;
			    case E_Type.Rotate:
				    a = new AgentActionRotate();
				    break;
                case E_Type.E_USE_LEVER:
                    a = new AgentActionUseLever();
                    break;
                case E_Type.E_PLAY_ANIM:
                    a = new AgentActionPlayAnim();
                    break;
                case E_Type.E_PLAY_IDLE_ANIM:
                    a = new AgentActionPlayIdleAnim();
                    break;
                case E_Type.E_DEATH:
                    a = new AgentActionDeath();
                    break;
                case E_Type.E_KNOCKDOWN:
                    a = new AgentActionKnockdown();
                    break;
                case E_Type.Teleport:
                    a = new AgentActionTeleport();
                    break;
                default:
                    Debug.LogError("no AgentAction to create");
                    return null;
            }
		}
        a.Reset();
		a.SetActive();

		// DEBUG !!!!!!
		m_ActionsInAction.Add(a);
		return a;
	}

	static public void Return(AgentAction action)
	{
		action.SetUnused();

		m_UnusedActions[(int)action.Type].Enqueue(action);
		//DEBUG SHIT
		m_ActionsInAction.Remove(action);
	}

    static public void Report()
    {
        Debug.Log("Action Factory m_ActionsInAction " + m_ActionsInAction.Count);
        for (int i = 0; i < m_ActionsInAction.Count; i++)
            Debug.Log(m_ActionsInAction[i].Type);
    }
}

