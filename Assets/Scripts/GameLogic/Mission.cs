using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SoundDataManager))]
[RequireComponent(typeof(SpriteEffectsManager))]
[RequireComponent(typeof(MissionBlackBoard))]

public class Mission : MonoBehaviour
{
    public GameZoneBaze[] GameZones = new GameZoneBaze[1];
    public GameObject[] ManagedGameObject = new GameObject[1];

    public AudioSource Music;
    public LiveHumanCache[] HumansCache;
    public DeadHumanCache[] DeadBodies;

    public int AchievementID;
    public int LeaderBoardID;
    const float MaxMusicVolume = 0.0f;

    [System.NonSerialized]
    public GameZoneBaze CurrentGameZone;

    public static Mission Instance;

    public int GameZoneIndex { get { for (int i = 0; i < GameZones.Length; i++) if (GameZones[i] == CurrentGameZone) return i; Debug.LogError("Unknow game zone"); return 0; } }
    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
       // Resources.UnloadUnusedAssets();

        Game.Instance.DisabledState = PlayerPrefs.GetInt("failed", 0);

        Game.Instance.GameState = E_GameState.Game;
        Game.Instance.CurrentLevel = Application.loadedLevelName;

        for (int i = 0; i < HumansCache.Length; i++)
            HumansCache[i].Init();

        for (int i = 0; i < DeadBodies.Length; i++)
            DeadBodies[i].Init();

        for (int i = 0; i < GameZones.Length; i++)
        {
            if (i == Game.Instance.CurrentGameZone)
            {
                CurrentGameZone = GameZones[i];
                CurrentGameZone.Enable(); 
            }
            else
            {
                GameZones[i].Disable();
            }
        }

        Game.Instance.LeaderBoardID = LeaderBoardID;
        Invoke("PrepareForStart", 0.1f);
    }

    void PrepareForStart()
    {
        //Debug.Log("Respawning - currentgamezone" + CurrentGameZone.gameObject.name );

   //     Game.Instance.ComboLevel = new E_ComboLevel[] { E_ComboLevel.Three, E_ComboLevel.Three, E_ComboLevel.Three, E_ComboLevel.Three, E_ComboLevel.Three, E_ComboLevel.Three };
   //     Game.Instance.SwordLevel = E_SwordLevel.Five;
   //     Game.Instance.HealthLevel = E_HealthLevel.Three;

        Player.Instance.Agent.SendMessage("Activate", CurrentGameZone.PlayerSpawn.Transform);

        Mission.Instance.SetNewMusic(SoundDataManager.Instance.CalmMusic, SoundDataManager.Instance.CalmMusicVolume, SoundDataManager.Instance.CalmMusicFadeOutTime, SoundDataManager.Instance.CalmMusicFadeInTime);

        CurrentGameZone.Restart();

        GuiManager.Instance.Reset();
        GuiManager.Instance.FadeIn(2);
    }

    public void EndOfMission(bool success)
    {
        if (Game.Instance.GameType == E_GameType.Survival)
        {
            if(success == false)
                GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_DEATH);

            
            StartCoroutine(EndOfSurvivalMode(3));
        }
        else
        {
            if (success)
            {
                StartCoroutine(FadeOutMusic(1));
                GuiManager.Instance.FadeOut();
            }
            else
            {
                GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_DEATH);
                StartCoroutine(LoadLastSave(3));
            }
        }
    }


    IEnumerator LoadLastSave(float delay)
    {
       // Debug.Log("mission - load last save point  " + Game.Instance.GameType);

        StartCoroutine(FadeOutMusic(2));
        

        yield return new WaitForSeconds(3);

        GuiManager.Instance.FadeOut();

        yield return new WaitForSeconds(1);

        Player.Instance.Agent.Transform.position = new Vector3(0, -1000, 0);

        yield return new WaitForSeconds(0.1f);

        //Player.Instance.Agent.CharacterController()

        CurrentGameZone.Reset();
        CurrentGameZone.Disable();

       // Debug.Log("mission - show last obejcts");

        for (int i = 0; i < ManagedGameObject.Length; i++)
            ManagedGameObject[i].SetActiveRecursively(true);

        SpriteEffectsManager.Instance.ReleaseBloodSprites();

        Game.Instance.Save_Load();
        
        yield return new WaitForEndOfFrame();

        //Debug.Log("mission - set current zone" + Game.Instance.CurrentGameZone);
        CurrentGameZone = GameZones[Game.Instance.CurrentGameZone];
        CurrentGameZone.Enable();

        //Debug.Log("Respawning player - currentgamezone" + Game.Instance.CurrentGameZone);

        Player.Instance.Agent.SendMessage("Activate", CurrentGameZone.PlayerSpawn.Transform);

        GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_NONE);

      //  Debug.Log("mission - set music");
        SetNewMusic(SoundDataManager.Instance.CalmMusic, SoundDataManager.Instance.CalmMusicVolume, SoundDataManager.Instance.CalmMusicFadeOutTime, SoundDataManager.Instance.CalmMusicFadeInTime);

        CurrentGameZone.Restart();

        GuiManager.Instance.Reset();

        GuiManager.Instance.FadeIn(2);


        //Debug.Log("mission - loaded");
    }

    IEnumerator EndOfSurvivalMode(float delay)
    {
        Debug.Log("mission - end of survival  " + Game.Instance.GameType);
        Player.Instance.Agent.BlackBoard.Stop = true;

        StartCoroutine(FadeOutMusic(2));

        yield return new WaitForSeconds(delay);

        GuiManager.Instance.ShowMessage(GuiManager.E_HudMessageType.E_NONE);
        
        GuiManager.Instance.FadeOut();

        Game.Instance.Save_Clear();

        yield return new WaitForSeconds(1.1f);
        
        Game.Instance.LoadScoreScreen();
    }

    public GameObject GetHuman(E_EnemyType Type, Transform t)
    {
        int l = HumansCache.Length;
        GameObject g;
        for (int i = 0; i < l; i++)
        {
            if (HumansCache[i].EnemyType == Type)
            {
                g = HumansCache[i].Get();
                if (g != null)
                {
                    g.SendMessage("Activate", t);

                    if(Type == E_EnemyType.MiniBoss01)
                        SpriteEffectsManager.Instance.CreateShadow(g, 1.5f, 1.5f);
                    else if(Type == E_EnemyType.BossOrochi)
                        SpriteEffectsManager.Instance.CreateShadow(g, 4, 4);
                    else
                        SpriteEffectsManager.Instance.CreateShadow(g, 1, 1);
                }
                return g;
            }
        }
        return null;
    }

    public void ReturnHuman(GameObject enemy)
    {
        enemy.SendMessage("Deactivate");
        SpriteEffectsManager.Instance.ReleaseShadow(enemy);
        
        int l = HumansCache.Length;
        for (int i = 0; i < l; i++)
        {
            if (HumansCache[i].Return(enemy) == true)
                return;
        }
        //Debug.LogError(enemy.name.ToString() + " Unable return enemie in cache!!!");
    }

    public GameObject GetDeadBody(Agent agent, E_DeadBodyType type)
    {
        GameObject g;
        for (int i = 0; i < DeadBodies.Length; i++)
        {
            if ((DeadBodies[i]).EnemyType == agent.EnemyType)
            {
                g = DeadBodies[i].Get(type);
                if (g != null)
                    g.SendMessage("Activate", agent.Transform);
                return g;
            }
        }

        //Debug.LogError(agent.name.ToString() + " Unable find dead body " + type + " in cache!!!");

        return null;
    }

    public void ReturnDeadBody(GameObject gameObject)
    {
        gameObject.SendMessage("Deactivate");

        for (int i = 0; i < DeadBodies.Length; i++)
        {
            if (DeadBodies[i].Return(gameObject) == true)
                return;
        }
        //Debug.LogError(gameObject.name + " Unable return death in cache!!!");
    }

    public void SetNewMusic(AudioClip clip, float volume, float fadeOutTime, float fadeIntime)
    {
        StopCoroutine("FadeOutInMusic");
        StopCoroutine("FadeOutMusic");
        //StopCoroutine("FadeOutInMusic");
        StartCoroutine(FadeOutInMusic(clip, volume, fadeOutTime, fadeIntime));
    }

    IEnumerator FadeOutInMusic(AudioClip clip, float musicVolume, float fadeOutTime, float fadeIntime)
    {
        if (Music.isPlaying)
        {
            if (fadeOutTime == 0)
            {
                Music.volume = 0;
                Music.Stop();
            }
            else
            {
                float maxVolume = Music.volume;
                float volume = Music.volume;
                while (volume > 0)
                {
                    volume -= 1 / fadeOutTime * Time.deltaTime * maxVolume;

                    if (volume < 0)
                        volume = 0;

                    Music.volume = volume;

                    yield return new WaitForEndOfFrame();
                }
                Music.Stop();
            }
        }


        yield return new WaitForEndOfFrame();

        if (clip != null)
        {
            Music.clip = clip;
            Music.Play();

            if (fadeIntime == 0)
            {
                Music.volume = musicVolume;
            }
            else
            {
                float maxVolume = musicVolume;
                float volume = 0;

                while (volume < maxVolume)
                {
                    volume += 1 / fadeIntime * Time.deltaTime * maxVolume;

                    if (volume > maxVolume)
                        volume = maxVolume;

                    Music.volume = volume;

                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    public void UnlockNextGameZone()
    {
        //Debug.Log("unlocking next zone");
        int i = GameZoneIndex;

        if(i + 1< GameZones.Length)
            GameZones[i+1].Enable();
    }


    public void LockPrevGameZone()
    {
        //Debug.Log("locking prev zone");

        int i = GameZoneIndex;

        if (i > 0 )
            GameZones[i -1].Disable();
    }


    /*
    IEnumerator FadeInMusic(float time)
    {
        float volume = 0;
        StopCoroutine("FadeOutMusic");
        Music.Play();

        if (time == 0)
        {
            Music.volume = MaxMusicVolume;
            yield break;
        }


        //Debug.Log("Fade in music");
        while (volume < MaxMusicVolume)
        {
            volume += 1 / time * Time.deltaTime * MaxMusicVolume;
            if (volume > MaxMusicVolume)
                volume = MaxMusicVolume;

            Music.volume = volume;

            yield return new WaitForEndOfFrame();
        }
    }
    */

    public IEnumerator FadeOutMusic(float time)
    {
        StopCoroutine("FadeInMusic");

        if (time == 0)
        {
            Music.volume = 0;
            Music.Stop();
            yield break;
        }

        //Debug.Log("Fade out music");
        float volume = MaxMusicVolume;
        while (volume > 0)
        {
            volume -= 1 / time * Time.deltaTime * MaxMusicVolume;
            if (volume < 0)
                volume = 0;

            Music.volume = volume;

            yield return new WaitForEndOfFrame();
        }
        Music.Stop();
        //Debug.Log("end of music");
    }

    void OnApplicationPause()
    {
        //if(GuiManager.Instance)
        //    GuiManager.Instance.ShowIngameMenu();
    }

    void OnApplicationFocus(bool focus)
    {
        //if(GuiManager.Instance)
        //    GuiManager.Instance.ShowIngameMenu();
    }
}
