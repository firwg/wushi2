using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameZoneDojo : GameZoneBaze
{
    public enum E_State
    {
        E_WAITING_FOR_ROUND_START,
        E_ROUND_IN_PROGRESS,
        E_ROUND_DONE,
        E_FINISHED,
    }

    public E_State State;

    public DojoRound[] Rounds = null;

    public AudioClip AudioDojoStart;
    public AudioClip AudioDojoEnd;

	void Awake()
	{
        GameObject = gameObject;
	}

    void Start()
    {
       // GuiManager.Instance.SetFadeOut(0);
        StartCoroutine(StartNewRound());
        
    }

    public override void Reset()
    {
        for (int i = _Enemies.Count - 1; i >= 0; i--)
            Mission.Instance.ReturnHuman(_Enemies[i].GameObject);

        _Enemies.Clear();

        for (int i = DeadBodies.Count - 1; i >= 0; i--)
            Mission.Instance.ReturnDeadBody(DeadBodies[i]);

        DeadBodies.Clear();
    }

    public override void Enable()
    {
        base.Enable();
    }

    public override void SetInProgress()
    {
        State = E_State.E_ROUND_IN_PROGRESS;
    }

    // We'll draw a gizmo in the scene view, so it can be found....
    void OnDrawGizmos()
    {
        BoxCollider b = GetComponent("BoxCollider") as BoxCollider;
        if(b != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(b.transform.position + b.center, b.size );
        }
    }

	// Update is called once per frame
	void FixedUpdate()
	{
        if (State != E_State.E_ROUND_IN_PROGRESS)
            return;

        Rounds[Game.Instance.CurrentSurvivalRound].Update();

        for(int i = 0; i < Enemies.Count;i++)
        {
            if (Enemies[i].IsAlive == false)
            {
                //Debug.Log(Time.timeSinceLevelLoad + " remove enemy " + Enemies[i].gameObject.name );
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (State == E_State.E_ROUND_IN_PROGRESS && Rounds[Game.Instance.CurrentSurvivalRound].RoundState == DojoRound.E_RoundState.E_FINISHED)
        {
            State = E_State.E_ROUND_DONE;
            StartCoroutine(RoundFinished());
        }
	}

    IEnumerator RoundFinished()
    {
        //Debug.Log("RoundFinished " + Game.Instance.CurrentSurvivalRound);

        Game.Instance.CurrentSurvivalRound++;

        if (Game.Instance.CurrentSurvivalRound == Rounds.Length)
        {
            EndOfDojo();
            yield break;
        }

        Game.Instance.Save_Save();
        Game.Instance.SaveStatistics();

        yield return new WaitForSeconds(3);

        Mission.Instance.GetComponent<AudioSource>().PlayOneShot(AudioDojoEnd); 
        Player.Instance.Agent.BlackBoard.Stop = true;

        GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_AREA_CLEAR);
        Player.Instance.HealToFullHealth();

        yield return new WaitForSeconds(2);

        StartCoroutine(StartNewRound());
    }


    IEnumerator StartNewRound()
    {
        Player.Instance.Agent.BlackBoard.Stop  = true;

        State = E_State.E_WAITING_FOR_ROUND_START;

        //Debug.Log("StartNewRound " + Game.Instance.CurrentSurvivalRound);
        GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_NONE);

        yield return new WaitForSeconds(0.1f);
        GuiManager.Instance.FadeOut();

        Player.Instance.Agent.BlackBoard.Stop = true;

        yield return new WaitForSeconds(1.2f);

        Player.Instance.Agent.BlackBoard.Stop = true;

        Player.Instance.Agent.Teleport(Mission.Instance.CurrentGameZone.PlayerSpawn.Transform);

        GuiManager.Instance.FadeIn();

        yield return new WaitForSeconds(1.5f);

        Mission.Instance.GetComponent<AudioSource>().PlayOneShot(AudioDojoStart); 
        GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_DOJO_START);
        
        yield return new WaitForSeconds(1.5f);

        Player.Instance.Agent.BlackBoard.Stop = false;

        Rounds[Game.Instance.CurrentSurvivalRound].Activate();

        GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_NONE);
    }

    void EndOfDojo()
    {
        State = E_State.E_FINISHED;

        //Debug.Log("StartEndOfDojo " + Game.Instance.CurrentSurvivalRound);

        Mission.Instance.EndOfMission(true);
    }


}

