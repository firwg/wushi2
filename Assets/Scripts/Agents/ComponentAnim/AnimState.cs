/***************************************************************
 * Class Name : AnimState
 * Function   :  Base state for animation engine FSM.. 
 *				
 * Created by : Marek R.
 **************************************************************/

using UnityEngine;
using System.Collections;



public class AnimState : System.Object
{
	protected Animation AnimEngine;
	private bool m_Finished = true;
	protected Agent Owner;
	protected Transform Transform;
    protected Transform RootTransform;


    /// <summary>
    ///  Public interface
    /// </summary>
    
	public AnimState(Animation anims, Agent owner)
	{
		AnimEngine = anims;
		Owner = owner;
        Transform = Owner.transform;
        RootTransform = Transform.Find("root");
	}

    virtual public void OnActivate(AgentAction action) // state is being activated
    {
        //if (Owner.debugAnims) Debug.Log(Time.timeSinceLevelLoad + " " + this.ToString() + " Activate " + " by " + (action != null ? action.ToString() : "nothing"));

        SetFinished(false);

        Initialize(action);
    }

    virtual public void OnDeactivate() //..............deactivated
    {
        //if (Owner.debugAnims) Debug.Log(Time.timeSinceLevelLoad + " " + this.ToString() + " DeActivate");
    }

    virtual public void Release() { SetFinished(true); } // finish currrent action and then finish state

	virtual public bool HandleNewAction(AgentAction action) { return false; } // new action is comming..

	virtual public void Update() { } // update current state

	virtual public bool IsFinished() { return m_Finished; }

	public virtual void SetFinished(bool finished)  { m_Finished = finished; } // state is finished or not



    //

    virtual protected void Initialize(AgentAction action)
    {
        //if (Owner.debugAnims) Debug.Log(Time.timeSinceLevelLoad + " " + this.ToString() + " Initialize " + " by " + (action != null ? action.ToString() : "nothing"));
    }

    protected bool Move(Vector3 velocity, bool slide = true )
    {
        Vector3 old = Transform.position;

        Transform.position += Vector3.up * Time.deltaTime;

        velocity.y -= 9 * Time.deltaTime;
        CollisionFlags flags = Owner.CharacterController.Move(velocity);

        //Debug.Log("move " + flags.ToString());

        if (slide == false && (flags & CollisionFlags.Sides) != 0)
        {
            Transform.position = old;
            return false;
        }

        if ((flags & CollisionFlags.Below) == 0)
        {
            Transform.position = old;
            return false;
        }

        return true;
    }

    protected bool MoveEx(Vector3 velocity)
    {
        Vector3 old = Transform.position;

        Transform.position += Vector3.up * Time.deltaTime;

        velocity.y -= 9 * Time.deltaTime;
        CollisionFlags flags = Owner.CharacterController.Move(velocity);

        if (flags == CollisionFlags.None)
        {
            RaycastHit hit;
            if (Physics.Raycast(Transform.position, -Vector3.up, out hit, 3, 1 << 10) == false)
            {
                Transform.position = old;
                return false;
            }
        }

        return true;
    }

    /*
    protected bool Move(Vector3 direction, float distance)
	{
        Vector3 endPos = Transform.position + direction * distance;

        if (Owner.BlackBoard.CollideWithEnemies && Mission.Instance.CurrentGameZone)
        {
            Vector3 test = endPos + direction * 0.5f;
            if (Mission.Instance.CurrentGameZone.GetNearestAliveEnemy(test, Owner.BlackBoard.CollisionRadius, true) != null)
                return false;
        }

        if (Owner.BlackBoard.CollideWithPlayer)
        {
            Vector3 test = endPos + direction * 0.25f;
            if ((test - Player.Instance.Agent.Position).sqrMagnitude < Owner.BlackBoard.CollisionRadius * Owner.BlackBoard.CollisionRadius)
                return false;
        }

		endPos.y += 1;
		RaycastHit hit;
        if (Physics.Raycast(endPos, -Vector3.up, out hit, 3, 1 << 10) == false)
			return false;

		Transform.position = hit.point;
		return true;
     }
    /*
    protected bool MoveTo(Vector3 finalPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(finalPos + Vector3.up, -Vector3.up, out hit, 5, 1 << 10) == false)
            return false;

        Transform.position = hit.point;
        return true;
    }

    protected bool MoveToCollideWithEnemy(Vector3 finalPos, Vector3 direction)
    {
        if (Owner.BlackBoard.CollideWithEnemies && Mission.Instance.CurrentGameZone)
        {//FIX_ME pridat test podle sirky enemace !!!
            Vector3 test = finalPos + direction * 0.25f;
            if (Mission.Instance.CurrentGameZone.GetNearestAliveEnemy(test, Owner.BlackBoard.CollisionRadius, true) != null)
                return false;
        }

        if (Owner.BlackBoard.CollideWithPlayer)
        {
            Vector3 test = finalPos + direction * 0.25f;

            if ((test - Player.Instance.Agent.Position).sqrMagnitude < Owner.BlackBoard.CollisionRadius * Owner.BlackBoard.CollisionRadius)
                return false;
        }

        RaycastHit hit;
        if (Physics.Raycast(finalPos + Vector3.up, -Vector3.up, out hit, 5, 1 << 10) == false)
            return false;

        Transform.position = hit.point;
        return true;
    }

    protected bool JumpTo(Vector3 finalPos, Vector3 direction, float height)
    {
        MoveToCollideWithEnemy(finalPos, direction);

        Transform.position += new Vector3(0,height,0);
        return true;
    }
    */
	protected bool IsGroundThere(Vector3 pos)
	{
		return Physics.Raycast(pos + Vector3.up, -Vector3.up, 5, 1 << 10);
	}

    protected void CrossFade(string anim, float fadeInTime)
    {
        //if (Owner.debugAnims) Debug.Log(Time.timeSinceLevelLoad + " " + this.ToString() + " cross fade anim: " + anim + " in " + fadeInTime + "s.");

        if(AnimEngine.IsPlaying(anim))
            AnimEngine.CrossFadeQueued(anim, fadeInTime, QueueMode.PlayNow);
        else
            AnimEngine.CrossFade(anim, fadeInTime);
    }

    protected void Blend(string anim, float fadeInTime)
    {
        AnimEngine.Blend(anim, 1, fadeInTime);
    }
    
    protected IEnumerator ShowTrail(AnimAttackData data, float speed, float delay, bool critical, float dustDelay)
    {
       // Time.timeScale = 0.1f;
        if (data.Trail == null)
            yield break;

        if (dustDelay < 0)
            dustDelay = 0;

        //yield return new WaitForSeconds(delay);

        data.Parent.position = Transform.position + Vector3.up * 0.15f;
        data.Parent.rotation = Quaternion.AngleAxis(Transform.rotation.eulerAngles.y, Vector3.up);

        data.Trail.SetActiveRecursively(true);

        if (data.Dust)
            data.Dust.SetActiveRecursively(false);

        Color color = Color.white;

        data.Material.SetColor("_TintColor", color);

        if (data.Dust)
        {
            yield return new WaitForSeconds(dustDelay);
            Owner.StartCoroutine(ShowTrailDust(data));
        }

        yield return new WaitForSeconds(delay - dustDelay);

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * speed;
            if (color.a < 0)
                color.a = 0;

            data.Material.SetColor("_TintColor", color);
            yield return new WaitForEndOfFrame();
        }

        data.Trail.SetActiveRecursively(false);
    }

    public IEnumerator ShowTrailDust(AnimAttackData data)
    {
        Color colorDust = new Color(1,1,1,1);
        data.Dust.SetActiveRecursively(true);

        data.MaterialDust.SetColor("_TintColor", colorDust);
        
        data.AnimationDust["Anim_Dust"].speed = 2.0f;
        data.AnimationDust.Play();

        while (colorDust.a > 0)
        {
            colorDust.a -= Time.deltaTime * 3;
            if (colorDust.a < 0)
                colorDust.a = 0;

            data.MaterialDust.SetColor("_TintColor", colorDust);
            yield return new WaitForEndOfFrame();
        }

        data.Dust.SetActiveRecursively(false);
    }
}
