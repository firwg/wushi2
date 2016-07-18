using UnityEngine;
using System.Collections.Generic;

public abstract class AnimFSM
{
    protected List<AnimState> AnimStates;
	protected AnimState CurrentAnimState;
	protected AnimState NextAnimState;
	protected AnimState DefaultAnimState;//Ä¬ÈÏ¶¯»­×´Ì¬

	protected Animation AnimEngine;
	protected Agent Owner;

	public AnimFSM( Animation anims, Agent owner)
	{
		AnimEngine  = anims;
		Owner = owner;
		AnimStates = new List<AnimState>();
	}

	public virtual void Initialize()
	{
		CurrentAnimState = DefaultAnimState;
		CurrentAnimState.OnActivate(null);
		NextAnimState = null;
	}

	// Update is called once per frame
	public void UpdateAnimStates()
	{
		//        Debug.Log("Update " + CurrentAnimState.ToString() + " to " + (NextAnimState? NextAnimState.ToString(): "null"));
		if (CurrentAnimState.IsFinished())
		{
			CurrentAnimState.OnDeactivate();
/*			if (NextAnimState != null)
			{
				//Debug.Log("Changing anim state from " + CurrentAnimState.ToString() + " to " + NextAnimState.ToString());
				CurrentAnimState = NextAnimState;
				CurrentAnimState.OnActivate();

				NextAnimState = null;
			}
			else*/
			{
				//Debug.Log("Changing to default state from " + CurrentAnimState.ToString());
				CurrentAnimState = DefaultAnimState;
				CurrentAnimState.OnActivate(null);
			}
		}

		CurrentAnimState.Update();
	}

    public void Reset()
    {
        for (int i = 0; i < AnimStates.Count; i++)
        {
            if (AnimStates[i].IsFinished() == false)
            {
                AnimStates[i].OnDeactivate();
                AnimStates[i].SetFinished(true);
            }
        }
    }

    public abstract void DoAction(AgentAction action);

    protected void ProgressToNextStage(AgentAction action)
    {
        if (NextAnimState != null)
        {
            CurrentAnimState.Release();

            CurrentAnimState.OnDeactivate();
            CurrentAnimState = NextAnimState;
            
            CurrentAnimState.OnActivate(action);
            
            NextAnimState = null;
        }
    }
}