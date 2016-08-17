//using System;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public interface AgentCallbackterface
{
    void RecieveHit(Agent attacker, E_WeaponType weapon);
    void RecieveKnockDown(Agent attacker, E_WeaponType weapon);


    void GOAPGoalActivate(E_GOAPGoals goal);
    void GOAPGoalDeactivated(E_GOAPGoals goal);
}

[System.Serializable]
public class Agent : MonoBehaviour
{
    #region AudioClip
    public int Experience = 10;
    public Material TransparentMaterial;
    public Material DiffuseMaterial;
    public AudioClip[] SpawnSounds = null;
    public AudioClip[] PrepareAttackSounds = null;
    public AudioClip[] BerserkSounds = null;
    public AudioClip[] StepSounds = null;
    public AudioClip[] RollSounds = null;

    public AudioClip[] AttackMissSounds = null;
    public AudioClip[] AttackHitSounds = null;
    public AudioClip[] AttackBlockSounds = null;

    public AudioClip WeaponOn = null;
    public AudioClip WeaponOff = null;
    #endregion



    [System.NonSerialized]
	public AnimSet AnimSet;
	public BlackBoard BlackBoard = new BlackBoard();// { get { return BlackBoard; } private set { BlackBoard = value; } }

    [System.NonSerialized]
	public WorldState WorldState;// { get { return WorldState; } private set { WorldState = value; } }

    [System.NonSerialized]
	public Memory Memory;// { get { return Memory; } private set { Memory = value; } }

    [System.NonSerialized]
    public CharacterController CharacterController;


	private GOAPManager m_GoalManager;
	private Hashtable m_Actions = new Hashtable();

	public GOAPAction GetAction(E_GOAPAction type) { return (GOAPAction)m_Actions[type]; }
	public int GetNumberOfActions() { return m_Actions.Count; }
    
    [System.NonSerialized]
	public Transform Transform;
    [System.NonSerialized]
    public GameObject GameObject;
    [System.NonSerialized]
    public AudioSource Audio;

    private SkinnedMeshRenderer Renderer;

   // public  bool debugGOAP = false;
    //public bool debugAnims = false;

    [System.NonSerialized]
    public E_EnemyType EnemyType = E_EnemyType.None;

    public GOAPGoal CurrentGOAPGoal { get { return m_GoalManager.CurrentGoal; } }

    public bool IsPlayer { get { return BlackBoard.IsPlayer; } }
    public bool IsAlive { get { return BlackBoard.Health > 0 && GameObject.active; } }
    public bool IsVisible { get { return Renderer.isVisible; } }
    public bool IsAttacking { get { return false; } }

    public Vector3 Position { get { return Transform.position; } }
    public Vector3 Forward { get { return Transform.forward; } }
    public Vector3 Right { get { return Transform.right; } }

    public bool IsInvulnerable { get { return BlackBoard.Invulnerable; } }
    public bool IsBlocking { get { return BlackBoard.MotionType == E_MotionType.Block || BlackBoard.MotionType == E_MotionType.BlockingAttack; } }
    public bool IsKnockedDown { get { return BlackBoard.MotionType == E_MotionType.Knockdown && BlackBoard.KnockDownDamageDeadly; } }

    public Vector3 ChestPosition { get { return Transform.position + transform.up * 1.5f; } }

    public AudioClip SpawnSound { get { if (SpawnSounds == null || SpawnSounds.Length == 0) return null; return SpawnSounds[Random.Range(0, SpawnSounds.Length)]; } }
    public AudioClip StepSound { get { if (StepSounds == null || StepSounds.Length == 0) return null; return StepSounds[Random.Range(0, StepSounds.Length)]; } }
    public AudioClip RollSound { get { if (RollSounds == null || RollSounds.Length == 0) return null; return RollSounds[Random.Range(0, RollSounds.Length)]; } }
    public AudioClip PrepareAttackSound { get { if (PrepareAttackSounds == null || PrepareAttackSounds.Length == 0) return null; return PrepareAttackSounds[Random.Range(0, PrepareAttackSounds.Length)]; } }
    public AudioClip BerserkSound { get { if (BerserkSounds == null || BerserkSounds.Length == 0) return null; return BerserkSounds[Random.Range(0, BerserkSounds.Length)]; } }
    public AudioClip AttackMissSound { get { if (AttackMissSounds == null || AttackMissSounds.Length == 0) return null; return AttackMissSounds[Random.Range(0, AttackMissSounds.Length)]; } }
    public AudioClip AttackHitSound { get { if (AttackHitSounds == null || AttackHitSounds.Length == 0) return null; return AttackHitSounds[Random.Range(0, AttackHitSounds.Length)]; } }
    public AudioClip AttackBlockSound { get { if (AttackBlockSounds == null || AttackBlockSounds.Length == 0) return null; return AttackBlockSounds[Random.Range(0, AttackBlockSounds.Length)]; } }

    private Vector3 CollisionCenter;
	//only once throught whole level
	void Awake()
	{
        Transform = transform;
        GameObject = gameObject;
        Audio = GetComponent<AudioSource>();
        CharacterController = Transform.GetComponent<CharacterController>();
        CollisionCenter = CharacterController.center;

        BlackBoard.Owner = this;
		BlackBoard.myGameObject = GameObject;

        AnimSet = GetComponent<AnimSet>();

		WorldState = new WorldState();
		Memory = new Memory();
		m_GoalManager = new GOAPManager(this);

		ResetAgent();
        
        WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);

        WorldState.SetWSProperty(E_PropKey.E_IDLING, true);
        WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        WorldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, false);
        WorldState.SetWSProperty(E_PropKey.E_LOOKING_AT_TARGET, false);
        WorldState.SetWSProperty(E_PropKey.E_USE_WORLD_OBJECT, false);
        WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM, false);

        WorldState.SetWSProperty(E_PropKey.E_IN_DODGE, false);
        WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);
        WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, false);
        WorldState.SetWSProperty(E_PropKey.E_IN_BLOCK, false);
        WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        WorldState.SetWSProperty(E_PropKey.E_IN_COMBAT_RANGE, false);
        WorldState.SetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY, false);
        WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, false);
        WorldState.SetWSProperty(E_PropKey.MoveToRight, false);
        WorldState.SetWSProperty(E_PropKey.MoveToLeft, false);
        WorldState.SetWSProperty(E_PropKey.E_TELEPORT, false);

        WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.None);

        
		//SetupActions();

		//SetupGoals();

        
	}

    void Start()
    {
        //Debug.Log("start");
        Renderer = GameObject.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
    }

    void Activate(Transform t)
    {
        //Debug.Log(name + " Aactivate");

        Reset();

        RaycastHit hit;
        if (Physics.Raycast(t.position + Vector3.up, -Vector3.up, out hit, 5, 1 << 10) == false)
            Transform.position = t.position;
        else
            Transform.position = hit.point;

        Transform.rotation = t.rotation;
        
        WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);

        WorldState.SetWSProperty(E_PropKey.E_IDLING, true);
        WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        WorldState.SetWSProperty(E_PropKey.E_IN_DODGE, false);
        WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        WorldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, false);

        WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);
        WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, false);
        WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM, false);
        WorldState.SetWSProperty(E_PropKey.E_IN_BLOCK, false);
        WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        WorldState.SetWSProperty(E_PropKey.E_IN_COMBAT_RANGE, false);
        WorldState.SetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY, false);
        WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, false);
        WorldState.SetWSProperty(E_PropKey.MoveToRight, false);
        WorldState.SetWSProperty(E_PropKey.MoveToLeft, false);

        WorldState.SetWSProperty(E_PropKey.E_TELEPORT, false);

        WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.None);

        StartCoroutine(FadeIn());

        SoundPlay(SpawnSound);

    }


    void Deactivate()
    {
        //Debug.Log(name + " Deactivate");
        StopAllCoroutines();
        Memory.Reset();
        m_GoalManager.Reset();
        BlackBoard.Reset();
    }


    //player Agent更新
	void LateUpdate()
	{
        if (IsPlayer == false)
        {
            //Debug.LogError("LateUpdate&&IsPlayer=false,FindCriticalGoal()");
            m_GoalManager.FindCriticalGoal();
            //UpdateAgent();
            //WorldState.SetWSProperty(E_PropKey.E_IDLING, m_GoalManager.CurrentGoal == null);
            //Debug.LogError(CurrentGOAPGoal);
            return;
        }
        UpdateAgent();
	}



    //敌人的Agent更新
    void FixedUpdate()
    {
        if (IsPlayer)
        {
            //Debug.Log(transform.rotation);
            return;
        }

        UpdateAgent();
        WorldState.SetWSProperty(E_PropKey.E_IDLING, m_GoalManager.CurrentGoal == null);

    }

    void UpdateAgent()
    {
        if (BlackBoard.DontUpdate == true)
            return;

        //update blackboard
        BlackBoard.Update();

        m_GoalManager.UpdateCurrentGoal();

        //Manage the list of goals we have
        m_GoalManager.ManageGoals();

        //Update the working memory.Cleans up facts marked for deletion
        Memory.Update();
    }



    public void PrepareForStart()
    {
        BlackBoard.Reset();
    }
	
	// could be called after death.. when agent should disappear
	void ResetAgent()
	{
        WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.None);

        StopAllCoroutines();
		BlackBoard.Reset();
		WorldState.Reset();
		Memory.Reset();
		m_GoalManager.Reset();

		//BlackBoard.GameObject.SetActiveRecursively(false);
	}


    public void AddGOAPAction(E_GOAPAction action)
    {
        m_Actions.Add(action, GOAPActionFactory.Create(action, this));
    }

    public void AddGOAPGoal(E_GOAPGoals goal)
    {
        m_GoalManager.AddGoal(goal);
    }

    public void InitializeGOAP()
    {
        m_GoalManager.Initialize();
    }

    // RECIEVE FUNCTIONS

    public void ReceiveRangeDamage(Agent attacker, float damage, Vector3 impuls)
    {
        BlackBoard.DamageType = E_DamageType.Front;
        BlackBoard.Attacker = attacker;
        BlackBoard.AttackerWeapon = E_WeaponType.Bow;
        BlackBoard.Impuls = impuls;

        if (Game.Instance.GameDifficulty == E_GameDifficulty.Easy)
            damage *= 0.8f;

        if (Game.Instance.DisabledState > 0)
            damage *= 5;

        BlackBoard.Health = Mathf.Max(0, BlackBoard.Health - damage);

        Fact f = FactsFactory.Create(Fact.E_FactType.E_EVENT);
        f.Belief = 1;
        f.EventType = E_EventTypes.Hit;
        Memory.AddFact(f);

        if (IsAlive)
        {
            WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Hit);
            SpriteEffectsManager.Instance.CreateBlood(Transform);
        }
        else
        {
            WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Died);
            StartCoroutine(Fadeout(15));
        }
        CombatEffectsManager.Instance.PlayBloodEffect(Transform.position, impuls);
    }

    public void ReceiveEnviromentDamage(float damage, Vector3 impuls)
    {
        if (Game.Instance.GameDifficulty == E_GameDifficulty.Easy)
            damage *= 0.5f;

        BlackBoard.DamageType = E_DamageType.Enviroment;
        BlackBoard.Attacker = null;
        BlackBoard.AttackerWeapon =  E_WeaponType.None;
        BlackBoard.Impuls = impuls;

        BlackBoard.Health = Mathf.Max(0, BlackBoard.Health - damage);

        Fact f = FactsFactory.Create(Fact.E_FactType.E_EVENT);
        f.Belief = 1;
        f.EventType = E_EventTypes.Hit;
        Memory.AddFact(f);

        if (IsPlayer)
            CameraBehaviour.Instance.PlayCameraAnim("shakeCombo3", false, false);

        if (IsAlive)
        {
            WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Hit);
            SpriteEffectsManager.Instance.CreateBlood(Transform);
        }
        else
        {
            WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Died);
            StartCoroutine(Fadeout(15));
        }

        CombatEffectsManager.Instance.PlayBloodEffect(Transform.position, impuls);

    }

    public void ReceiveHitCompletelyBlocked(Agent attacker)
    {
        CombatEffectsManager.Instance.PlayBlockHitEffect(ChestPosition, -attacker.Forward);

        BlackBoard.Berserk += BlackBoard.BerserkBlockModificator;
        BlackBoard.Rage += BlackBoard.RageBlockModificator;


        if (attacker.IsPlayer)
            Game.Instance.NumberOfBlockedHits++;
    }

    public void ReceiveBlockedHit(Agent attacker, E_WeaponType byWeapon, float damage, AnimAttackData data)
    {
        BlackBoard.Attacker = attacker;
        BlackBoard.AttackerWeapon = byWeapon;

        //if (debugGOAP == true) Debug.Log(Time.timeSinceLevelLoad + " Recieve blocked damage " + name.ToString() + " from " + attacker.name);

        WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.HitBlocked);

        bool fromBehind = Vector3.Dot(attacker.Forward, Forward) > -0.1f;

        if (fromBehind) // utok zezadu,kdyz koren blokuje
        {
            BlackBoard.Health = Mathf.Max(1, BlackBoard.Health - damage);
            BlackBoard.DamageType = E_DamageType.BreakBlock;
            CombatEffectsManager.Instance.PlayBloodEffect(Transform.position, -attacker.Forward);
            SpriteEffectsManager.Instance.CreateBlood(Transform);
        }
        else
        { // blocked attack
            if (data.BreakBlock)
            {
                BlackBoard.DamageType = E_DamageType.BreakBlock;
                if (attacker.IsPlayer)
                    Game.Instance.NumberOfBreakBlocks++;

                CombatEffectsManager.Instance.PlayBlockBreakEffect(Transform.position, -attacker.Forward);
            }
            else
            {
                BlackBoard.DamageType = E_DamageType.Front;
                if (attacker.IsPlayer)
                    Game.Instance.NumberOfBlockedHits++;

                CombatEffectsManager.Instance.PlayBlockHitEffect(ChestPosition, -attacker.Forward);
            }



            
        }
    }

    public void ReceiveImpuls(Agent attacker, Vector3 impuls)
    {
        //Debug.Log(GameObject.name + " impuls " + impuls.magnitude);
        BlackBoard.Attacker = attacker;
        BlackBoard.AttackerWeapon =  E_WeaponType.None;
        BlackBoard.Impuls = impuls;

        BlackBoard.DamageType = E_DamageType.Front;

        Fact f = FactsFactory.Create(Fact.E_FactType.E_EVENT);
        f.Belief = 1;
        f.EventType = E_EventTypes.Hit;
        Memory.AddFact(f);

        WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Hit);

    }

    public void ReceiveDamage(Agent attacker, E_WeaponType byWeapon, float damage, AnimAttackData data)
    {
        if(IsAlive == false)
            return;

        if (attacker.IsPlayer)
        {
            Game.Instance.Hits += 1;
            if (Game.Instance.GameDifficulty == E_GameDifficulty.Easy)
                damage *= 1.2f;
        }

        if (IsPlayer && Game.Instance.GameDifficulty == E_GameDifficulty.Easy)
            damage *= 0.8f;

        if (IsPlayer && Game.Instance.DisabledState > 5)
            damage *= 100;

        BlackBoard.Attacker = attacker;
        BlackBoard.AttackerWeapon = byWeapon;
        BlackBoard.Impuls = attacker.Forward * data.HitMomentum;

        //if(debugGOAP == true) Debug.Log(Time.timeSinceLevelLoad +  " Recieve damage " + name.ToString() + " from " + attacker.name);

        if (IsKnockedDown)
        { //pokud je v knockdownu , umre
            BlackBoard.Health = 0;
            BlackBoard.DamageType = E_DamageType.InKnockdown;
            WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Died);
            CombatEffectsManager.Instance.PlayCriticalEffect(Transform.position, -attacker.Forward);
            StartCoroutine(Fadeout(3));
            SoundPlay(SoundDataManager.Instance.FatalitySound);
            if (attacker.IsPlayer)
            {
                Game.Instance.Score += Experience;
                Player.Instance.AddExperience(Experience, 1.5f + Game.Instance.Hits * 0.1f);
            }
        }
        else
        { // normal zraneni
            BlackBoard.Health = Mathf.Max(0, BlackBoard.Health - damage);
            BlackBoard.DamageType = E_DamageType.Front;

            Fact f = FactsFactory.Create(Fact.E_FactType.E_EVENT);
            f.Belief = 1;
            f.EventType = E_EventTypes.Hit;
            Memory.AddFact(f);

            if (IsAlive)
            {
                WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Hit);
                SpriteEffectsManager.Instance.CreateBlood(Transform);
            }
            else
            {
                WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Died);
                StartCoroutine(Fadeout(3));

                if (attacker.IsPlayer)
                {
                    Game.Instance.Score += Experience;
                    Player.Instance.AddExperience(Experience, 1 + Game.Instance.Hits * 0.1f);
                }
           }

            if (damage >= 15)
                CombatEffectsManager.Instance.PlayBloodBigEffect(Transform.position, -attacker.Forward);
            else
                CombatEffectsManager.Instance.PlayBloodEffect(Transform.position, -attacker.Forward);

        }
    }

    public void ReceiveKnockDown(Agent attacker, Vector3 impuls)
    {
        if (IsAlive == false || BlackBoard.KnockDown == false)
            return;

        if (attacker.IsPlayer)
        {
            Game.Instance.Hits += 1;
            Game.Instance.NumberOfKnockdowns++;
        }

        //if (debugGOAP == true) Debug.Log(Time.timeSinceLevelLoad + " Recieve knockdown " + name.ToString() + " from " + attacker.name + " impuls " + impuls.ToString());

        BlackBoard.Attacker = attacker;
        BlackBoard.Impuls = impuls;

        WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.Knockdown);

        CombatEffectsManager.Instance.PlayKnockdownEffect(Transform.position, -attacker.Forward);
    }

    public void ReceiveCriticalHit(Agent attacker, E_CriticalHitType type, bool effectOnly = false)
    {//     Legs = 0,   Beheaded,    HalfBody,    SliceFrontBack,    SliceLeftRight,

        if (attacker.IsPlayer)
        {
            Game.Instance.Hits += 1;

           // Debug.Log("enemy receive critical " + EnemyType + " " +(int)EnemyType);

            Game.Instance.Score += Experience;
            Player.Instance.AddExperience(Experience, 1.5f + Game.Instance.Hits * 0.1f);
            Game.Instance.NumberOfCriticals++;
        }

        BlackBoard.Stop = true;
        BlackBoard.Health = 0;

        if (type == E_CriticalHitType.Horizontal)
        {
            int r = Random.Range(0, 100);
            if (r < 33)
                Mission.Instance.GetDeadBody(this, E_DeadBodyType.Legs);
            else if (r < 66)
                Mission.Instance.GetDeadBody(this, E_DeadBodyType.Beheaded);
            else
                Mission.Instance.GetDeadBody(this, E_DeadBodyType.HalfBody);
        }
        else
        {
            float dot = Vector3.Dot(Forward, attacker.Forward);

            if(dot < 0.5 && dot > -0.5f)
                Mission.Instance.GetDeadBody(this, E_DeadBodyType.SliceLeftRight);
            else
                Mission.Instance.GetDeadBody(this, E_DeadBodyType.SliceFrontBack);
        }

        CombatEffectsManager.Instance.PlayCriticalEffect(Transform.position, -attacker.Forward);

        Mission.Instance.ReturnHuman(GameObject);
    }

    /// WEAPONS
    /*
    public void ShowWeapon(bool show, float delay)
    {
        if (Weapons == null || Weapons[(int)BlackBoard.WeaponSelected] == null)
            return;

        StopCoroutine("_ShowWeapon");
           
        StartCoroutine(_ShowWeapon(show, delay));
    }
    
    IEnumerator _ShowWeapon(bool show, float delay)
    {
        if (show == false)
        {
            yield return new WaitForSeconds(0.8f);
            SoundPlayWeaponOff();
            yield return new WaitForSeconds(delay - 0.8f);
        }
        else
            yield return new WaitForSeconds(delay);

        

        Weapons[(int)BlackBoard.WeaponSelected].SetActiveRecursively(show);
    }*/



    public void PlayAnim(string animName)
    {
        if (animName != null)
        {
            BlackBoard.DesiredAnimation = animName;
            WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM, true);
        }
    }


    public void Teleport(Transform destination)
    {
        Transform.position = destination.position;
        Transform.rotation = destination.rotation;
    }


    void SpawnBlood()
    {
        SpriteEffectsManager.Instance.CreateBloodSlatter(Transform, 1, 3);
    }

    protected IEnumerator FadeIn()
    {
        if (TransparentMaterial == null)
            yield break;

        yield return new WaitForEndOfFrame();

        //Material old = Renderer.material;

        Renderer.material = TransparentMaterial;

        Color color = new Color(1, 1, 1, 0);
        TransparentMaterial.SetColor("_Color", color);

        while (color.a < 1)
        {
            color.a += Time.deltaTime * 4;
            if (color.a > 1)
                color.a = 1;

            TransparentMaterial.SetColor("_Color", color);
            yield return new WaitForEndOfFrame();
        }

        color.a = 1;
        TransparentMaterial.SetColor("_Color", color);

        Renderer.material = DiffuseMaterial;
    }

    
    protected IEnumerator Fadeout(float delay)
    {
        if (TransparentMaterial == null)
            yield break;

        yield return new WaitForSeconds(delay);

        CombatEffectsManager.Instance.PlayDisappearEffect(Transform.position, Transform.forward);

        SpriteEffectsManager.Instance.ReleaseShadow(GameObject);

        //Material old = Renderer.material;

        Renderer.material = TransparentMaterial;

        Color color = new Color(1, 1, 1, 1);
        TransparentMaterial.SetColor("_Color", color);

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * 4;
            if (color.a < 0)
                color.a = 0;

            TransparentMaterial.SetColor("_Color", color);
            yield return new WaitForEndOfFrame();
        }

        color.a = 0;
        TransparentMaterial.SetColor("_Color", color);

        Mission.Instance.ReturnHuman(GameObject);
    }

    public void Reset()
    {
        if (TransparentMaterial != null)
        {
            if(Renderer == null)
                Renderer = (gameObject.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
            
            Renderer.material = TransparentMaterial;
            //Renderer.material = TransparentMaterial;

            Color color = new Color(1, 1, 1, 0);
            TransparentMaterial.SetColor("_Color", color);
        }
        ResetAgent();

        EnableCollisions();
    }

    public float GetCriticalChance()
    {
        return 18;
    }

    public void SoundPlay(AudioClip clip)
    {
        if(clip)
            Audio.PlayOneShot(clip);
    }

    public void SoundPlayStep()
    {
        SoundPlay(StepSound);
    }

    public void SoundPlayRoll()
    {
        SoundPlay(RollSound);
    }

    public void SoundPlayKnockdown()
    {
        SoundPlay(SoundDataManager.Instance.KnockDownSound);
    }

    public void SoundPlayBerserk()
    {
        SoundPlay(BerserkSound);
    }

    public void SoundPlayHit()
    {
        SoundPlay(AttackHitSound);
    }

    public void SoundPlayMiss()
    {
        SoundPlay(AttackMissSound);
    }

    public void SoundPlayBlockHit()
    {
        SoundPlay(AttackBlockSound);
    }

    public void SoundPlayPrepareAttack()
    {
        SoundPlay(PrepareAttackSound);
    }

    public void SoundPlayWeaponOff()
    {
        SoundPlay(WeaponOff);
    }

    public void PlayLoopSound(AudioClip clip, float delay, float time, float fadeInTime, float fadeOutTime)
    {
        StartCoroutine(_PlayLoopSound(clip, delay, time, fadeInTime, fadeOutTime));
    }

    IEnumerator _PlayLoopSound(AudioClip clip, float delay, float time, float fadeInTime, float fadeOutTime)
    {
        Audio.volume = 0;
        Audio.loop = true;
        Audio.clip = clip;

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(delay);
       
        Audio.Play();

        float step = 1 / fadeInTime;
        while (Audio.volume < 1)
        {
            Audio.volume = Mathf.Min(1.0f, Audio.volume + step * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(time - fadeInTime - fadeOutTime);

        step = 1 / fadeInTime;
        while (Audio.volume > 0)
        {
            Audio.volume = Mathf.Max(0.0f, Audio.volume - step * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Audio.Stop();

        yield return new WaitForEndOfFrame();

        Audio.volume = 1;
    }


    public void DisableCollisions()
    {
        CharacterController.detectCollisions = false;
        CharacterController.center = Vector3.up * -20;
    }

    public void EnableCollisions()
    {
        CharacterController.detectCollisions = true;
        CharacterController.center = CollisionCenter;
    }



    //public event EventHandler<>





}
