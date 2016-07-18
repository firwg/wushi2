using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(BoxCollider))]
public class SpawnZone : MonoBehaviour
{
    [System.Serializable]
    public class RoundInfo
    {
        [System.Serializable]
        public class SpawnInfo
        {
            public E_EnemyType EnemyType;
            public SpawnPointEnemy[] SpawnPoint;
            public float SpawnDelay = 0;
            public bool RotateToPlayer = true;
            public bool WhenKilledStopSpawn = false;
        }

        public SpawnInfo[] Spawns;
        public float SpawnDelay = 0;
        public int MinEnemiesFomLastRound = 0;
    }

    public RoundInfo[] SpawnRounds;

    public SpawnPointEnemy[] SpawnPoints = null;

    public PadLock LockIn = null;
    public PadLock LockOut = null;


    private GameObject GameObject;
	private List<Agent> EnemiesAlive = new List<Agent>();

    private GameZone MyGameZone;
    /*
    public AudioClip ActionMusic;
    public float ActionMusicFadeOutTime = 0.4f;
    public float ActionMusicFadeInTime = 0.4f;
    public float ActionMusicVolume = 1;

    public AudioClip CalmMusic;
    public float CalmMusicFadeOutTime = 0.4f;
    public float CalmMusicFadeInTime = 0.4f;
    public float CalmMusicVolume = 1;
    */
	public bool IsActive() { return EnemiesAlive.Count > 0; }
    public Agent GetEnemy(int index) { return EnemiesAlive[index]; }
    public int GetEnemyCount() { return EnemiesAlive.Count; }

    public enum E_State
    {
        E_WAITING_FOR_START,
        E_SPAWNING_ENEMIES,
        E_IN_PROGRESS,
        E_FINISHED,
    }

    public E_State State = E_State.E_WAITING_FOR_START;


	void Awake()
	{
        GameObject = gameObject;
        MyGameZone = GameObject.transform.parent.GetComponent<GameZone>();
	}

    public void Enable()
    {
        //Debug.Log(GameObject.name + " Enable");

        if (LockIn != null)
            LockIn.Unlock();
        if (LockOut != null)
            LockOut.Unlock();
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

        if(SpawnPoints != null)
        {
            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                if (b != null)
                   Gizmos.DrawLine(b.transform.position + b.center, SpawnPoints[i].transform.position);
                else
                    Gizmos.DrawLine(GameObject.transform.position , SpawnPoints[i].transform.position);
            }
        }
    }

    /*void OnEnable()
    {
        Debug.Log(GameObject.name + " OnEnable");
    }
    void OnDisable()
    {
        Debug.Log(GameObject.name + " Ondisable");
    }*/

	// Update is called once per frame
	void FixedUpdate()
	{
        for (int i = EnemiesAlive.Count - 1; i >= 0; i--)
        {
            if (EnemiesAlive[i].IsAlive == true)
                continue;

            EnemiesAlive.RemoveAt(i);
        }

        if (State != E_State.E_IN_PROGRESS)
            return;

		if (EnemiesAlive.Count == 0)
		{
            State = E_State.E_FINISHED;

            if (LockOut != null)
                LockOut.Unlock();

            if (SoundDataManager.Instance.IsSwitchingMusic())
                Mission.Instance.SetNewMusic(SoundDataManager.Instance.CalmMusic, SoundDataManager.Instance.CalmMusicVolume, SoundDataManager.Instance.CalmMusicFadeOutTime, SoundDataManager.Instance.CalmMusicFadeInTime);

            MyGameZone.EnableAllActiveInteraction(true);
		}

	}

	public void Restart()
	{
       // Debug.Log(GameObject.name + " Restart");

		StopAllCoroutines();

        State = E_State.E_WAITING_FOR_START;

        if (LockIn != null)
            LockIn.Reset();
        if (LockOut != null)
            LockOut.Reset();
    }


    void OnTriggerEnter(Collider other)
    {
        if (State != E_State.E_WAITING_FOR_START || other != Player.Instance.Agent.CharacterController)
            return;

        MyGameZone.SetInProgress();

        MyGameZone.EnableAllActiveInteraction(false);

        if (SpawnRounds != null && SpawnRounds.Length > 0)
            StartCoroutine(SpawnEnemiesInRounds());
        else
            StartCoroutine(SpawnEnemies());

        if (LockIn != null)
            LockIn.Lock();
        if (LockOut != null)
            LockOut.Lock();
        
        if(SoundDataManager.Instance.IsSwitchingMusic())
            Mission.Instance.SetNewMusic(SoundDataManager.Instance.ActionMusic, SoundDataManager.Instance.ActionMusicVolume, SoundDataManager.Instance.ActionMusicFadeOutTime, SoundDataManager.Instance.ActionMusicFadeInTime);
    }

    IEnumerator SpawnEnemies()
    {
        State = E_State.E_SPAWNING_ENEMIES;
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);

            if (SpawnPoints[i].Difficulty > Game.Instance.GameDifficulty)
                continue;

            StartCoroutine(SpawnEnemy(SpawnPoints[i]));

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.4f));
        }

        yield return new WaitForSeconds(4.0f);

        State = E_State.E_IN_PROGRESS;
    }

    IEnumerator SpawnEnemy(SpawnPointEnemy spawnpoint)
    {
        CombatEffectsManager.Instance.PlaySpawnEffect(spawnpoint.Transform.position, spawnpoint.Transform.forward);

        yield return new WaitForSeconds(0.1f);

        GameObject enemy = Mission.Instance.GetHuman(spawnpoint.EnemyType, spawnpoint.Transform);


        Agent agent = enemy.GetComponent("Agent") as Agent;
        agent.PrepareForStart();
     
        if (spawnpoint.SpawnAnimation != null)
            agent.PlayAnim(spawnpoint.SpawnAnimation.name);

        MyGameZone.AddEnemy(agent);
        EnemiesAlive.Add(agent);
    }

    IEnumerator SpawnEnemiesInRounds()
    {
        State = E_State.E_SPAWNING_ENEMIES;

        Agent ImportantAgent = null;

        for (int i = 0; i < SpawnRounds.Length; i++)
        {
            RoundInfo round = SpawnRounds[i];

            float delay = round.SpawnDelay;

            while (delay > 0)
            {
                if (ImportantAgent != null && ImportantAgent.IsAlive == false)
                {
                    State = E_State.E_IN_PROGRESS;
                    yield break;
                }

                if (EnemiesAlive.Count == 0 || EnemiesAlive.Count <= round.MinEnemiesFomLastRound)
                    break;// dont wait, when enemies are killed or less then required 

                yield return new WaitForSeconds(0.5f);
                delay -= 0.5f;
            }

            for(int ii = 0; ii < round.Spawns.Length; ii++)
            {
                RoundInfo.SpawnInfo spawnInfo = round.Spawns[ii];

                yield return new WaitForSeconds(spawnInfo.SpawnDelay);

                SpawnPointEnemy spawnpoint = GetAvailableSpawnPoint(spawnInfo.SpawnPoint == null || spawnInfo.SpawnPoint.Length == 0 ? SpawnPoints : spawnInfo.SpawnPoint);

                if (spawnInfo.RotateToPlayer)
                {
                    Vector3 dir = Player.Instance.Agent.Position - spawnpoint.Transform.position;
                    dir.Normalize();
                    spawnpoint.Transform.forward = dir;
                }

                GameObject enemy = Mission.Instance.GetHuman(spawnInfo.EnemyType, spawnpoint.Transform);

                while (enemy == null)
                {
                    yield return new WaitForSeconds(0.2f);
                    enemy = Mission.Instance.GetHuman(spawnInfo.EnemyType, spawnpoint.Transform);
                }

                CombatEffectsManager.Instance.PlaySpawnEffect(spawnpoint.Transform.position, spawnpoint.Transform.forward);

                Agent agent = enemy.GetComponent("Agent") as Agent;
                agent.PrepareForStart();
                
                MyGameZone.AddEnemy(agent);
                EnemiesAlive.Add(agent);

                if (spawnInfo.WhenKilledStopSpawn)
                    ImportantAgent = agent;

                yield return new WaitForSeconds(0.1f);
            }
        }

        State = E_State.E_IN_PROGRESS;
    }

    SpawnPointEnemy GetAvailableSpawnPoint(SpawnPointEnemy[] spawnPoints)
    {
        Vector3 pos = Player.Instance.Agent.Position;

        float bestValue = 0;
        int bestSpawn = -1; 

        for(int i = 0; i < spawnPoints.Length;i++)
        {
            if (MyGameZone.IsEnemyInRange(spawnPoints[i].transform.position, 2))
            {
            //    Debug.Log(i + " Spawnpoint " + spawnPoints[i].name + " is near to enemy");
                continue;
            }

            float value = 0;
            float dist = Mathf.Min(14, (spawnPoints[i].Transform.position - pos).magnitude);
            value = Mathfx.Hermite(0, 7, dist/7);

           // Debug.Log(i + " Spawnpoint " + spawnPoints[i].name + " dist " + dist + " Value " + value);
            if (value <= bestValue)
                continue;

            bestValue = value;
            bestSpawn = i;
        }

        //Debug.Log("Best spaqwn point is " + bestSpawn);

        if( bestSpawn == -1)
            return spawnPoints[Random.Range(0, spawnPoints.Length)];

        return spawnPoints[bestSpawn];
    }
}

