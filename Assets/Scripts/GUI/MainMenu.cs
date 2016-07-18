using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// 娓告垙鍚屘
	public string ofname;
	// 娓告垙id
	public string ofid;
	// 娓告垙key
	public string ofkey;
	// 娓告垙secret
	public string ofsecret;
	// 娓犻亾鏍囪瘑
	//public string appstore;

	public static OpenFeintFacade openFeint;

	enum E_MenuState
	{
		Busy,
		MainMenu,
        GameSubmenu,
        DojoSubmenu,
        ChaptersSubmenu,
	}

    public GameObject MainLine;
    public GameObject MainBackground;
    public GameObject SinglePlayer;
    public GameObject Dojo;
    public GameObject Chapters;
    public GameObject Help;
    public GameObject MoreGames;

    public GameObject ALBackground;
    public GameObject Achievements;
    public GameObject Leaderboards;
    public GameObject GameCenter;

    public GameObject GameTitle;
    public GameObject GameBackground;
    public GameObject GameResume;
    public GameObject GameEasy;
    public GameObject GameNormal;
    public GameObject GameHard;

    public GameObject DojoTitle;
    public GameObject DojoBackground;
    public GameObject DojoResume;
    public GameObject DojoNew;

    public GameObject ChaptersTitle;
    public GameObject ChaptersBackground;

    public GameObject ChaptersLocked;
    public GameObject[] Chapter = new GameObject[7];

    public GameObject Back;
    public GameObject Loading;

    public AudioClip[] SoundsButton;
    public AudioClip SoundMainMenuIn;
    public AudioClip SoundMainMenuOut;
    public AudioClip SoundSubMenuIn;
    public AudioClip SoundSubMenuOut;

    private const int AchievementsID = 18;
    private const int GameCenterID = 19;
 
    private const int SinglePlayerID = 20;
    private const int DojoID = 21;
    private const int ChaptersID = 22;
    private const int LeaderboardsID = 23;
    private const int HelpID = 24;
    private const int MoreGamesID = 25;

    private const int GameResumeID = 20;
    private const int GameNormalID = 21;
    private const int GameHardID = 22;
    private const int GameEasyID = 24;

    private const int DojoResumeID = 20;
    private const int DojoNewID = 21;

    private const int BackID = 30;

    private int[] ChapterID = {20,21,22,23,24,25,26};

    private Vector3 Position;
  	private E_MenuState MenuState = E_MenuState.MainMenu;
	private AudioSource Music;
	const float MaxMusicVolume = 0.7f;

	void Awake()
	{
		Music = GetComponent<AudioSource>();
        Position = transform.position;
        Loading.SetActiveRecursively(false);
	}
    	
	void Start () 
    {
        Resources.UnloadUnusedAssets();

    	StartCoroutine(FadeInMusic(0.1f));
		Game.Instance.GameState = E_GameState.MainMenu;

       /* if (iPhoneUtils.isApplicationGenuine == false && iPhoneUtils.isApplicationGenuineAvailable == true)
        {
            int numberofTest = PlayerPrefs.GetInt("failed", 0);
            numberofTest++;
            Game.Instance.DisabledState = numberofTest;
            PlayerPrefs.SetInt("failed", numberofTest);
            //Camera.mainCamera.orthographicSize = 0.2f;
        }
        else*/
            PlayerPrefs.SetInt("failed", 0);
            

      //  StartCoroutine(StartNewEasyGame());
	  
		if(openFeint == null){
			// 鍒涘缓鑴氭湰鎺ュ彛瀵硅薄
			openFeint = new OpenFeintFacade();
			// 璁剧疆娓犻亾鏍囪瘑锛堜竴瀹氳?鍦ㄥ垵濮嬪寲涔嬪墠璁剧疆锛屽?鏋滀笉闇瑕佹笭閬撴爣璇嗭紝鍙?互涓嶇敤璁剧疆锛屽叾榛樿?鍊间负"default"锛執
			//openFeint.setAppstore(The9Settings.appstoreName);
			openFeint.setAppstore("htctegra");
			// 鍒濆?鍖栦節鍩庢父鎴忎腑蹇偺
			openFeint.Init(ofname, ofkey, ofsecret, ofid);
		}
	}

	public void Update()
	{
//        Debug.Log("main menu updating");
        if(Input.touchCount == 0)
            return;

        Touch t = Input.touches[0];

		if(t.phase != TouchPhase.Ended)
            return;

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(t.position), out hit, Mathf.Infinity) == false)
            return;

    	int id = hit.collider.gameObject.layer;
        AudioSource.PlayClipAtPoint(SoundsButton[Random.Range(0, SoundsButton.Length)], Position);
        
        if (MenuState == E_MenuState.MainMenu)
	    {
            switch(id)
            {
                case SinglePlayerID:
                    SinglePlayer.GetComponent<Animation>().Play("MainButtonsPress");
                    StartCoroutine(StartSinglePlayerSubMenu());
                    break;
                case DojoID:
                    Dojo.GetComponent<Animation>().Play("MainButtonsPress");
                    StartCoroutine(StartDojoSubMenu());
                    break;
                case ChaptersID:
                    Chapters.GetComponent<Animation>().Play("MainButtonsPress");
                    StartCoroutine(StartChaptersSubMenu());
                    break;
                case LeaderboardsID:
                    Leaderboards.GetComponent<Animation>().Play("AandL_press");
                    StartCoroutine(StartLeaderboards());
                    break;
                case AchievementsID:
                    Achievements.GetComponent<Animation>().Play("AandL_press");
                    StartCoroutine(StartAchievements());
                    break;
                case GameCenterID:
                    //GameCenter.animation.Play("AandL_press");
                    StartCoroutine(StartGameCenter());
                    break;
                case HelpID:
                    Help.GetComponent<Animation>().Play("MainButtonsPress");
                    StartCoroutine(StartTutorial());
                    break;
                case MoreGamesID:
                    MoreGames.GetComponent<Animation>().Play("MainButtonsPress");
                    StartCoroutine(StartMoreGamesSubMenu());
                    break;
			}
			//if(id != GameCenterID) {
			//	GameCenter.animation.Play("MainButtonsOut");
			//}
        }
        else if(MenuState == E_MenuState.DojoSubmenu)
        {
            switch(id)
            {
                case DojoResumeID:
                    DojoResume.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartResumeDojo());
                    break;
                case DojoID:
                    DojoNew.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartNewDojo());
                    break;
                case BackID:
                    Back.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartFromDojo2MainMenu());
                    break;
            }
        }
        else if (MenuState == E_MenuState.GameSubmenu)
        {
            switch(id)
            {
                case GameResumeID:
                    GameResume.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartResumeGame());
                    break;
                case GameEasyID:
                    GameEasy.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartNewEasyGame());
                    break;
                case GameNormalID:
                    GameNormal.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartNewNormalGame());
                    break;
                case GameHardID:
                    GameHard.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartNewHardGame());
                    break;
                case BackID:
                    Back.GetComponent<Animation>().Play("press");
                    StartCoroutine(StartFromGame2MainMenu());
                    break;
			}
		}
        else if (MenuState == E_MenuState.ChaptersSubmenu)
        {
            if(id == BackID)
            {
                Back.GetComponent<Animation>().Play("press");
                StartCoroutine(StartFromChapters2MainMenu());
            }
            else 
            {   
                int  index = id - ChapterID[0];
                Chapter[index].GetComponent<Animation>().Play("press");
                StartCoroutine(StartChapter(index));
            }
        }
	}

	void FadeIn()
	{
		MenuState = E_MenuState.MainMenu;
	}

    void GameCenterPlayerLog()
    {
        if (MenuState == E_MenuState.MainMenu)
            StartCoroutine(_StartGameCenterMenu());
    }

    IEnumerator _StartGameCenterMenu()
    {
        MenuState = E_MenuState.Busy;
        ALBackground.GetComponent<Animation>().Play("AandL_out");
        GameCenter.GetComponent<Animation>().Play("AandL_out");

        

        yield return new WaitForSeconds(0.2f);

        ALBackground.GetComponent<Animation>().Play("AandL_in");
        Achievements.GetComponent<Animation>().Play("AandL_in");
        Leaderboards.GetComponent<Animation>().Play("AandL_in");

        MenuState = E_MenuState.MainMenu;
    }

    void GameCenterNotSupported()
    {
        if (MenuState == E_MenuState.MainMenu)
        {
            ALBackground.GetComponent<Animation>().Play("AandL_out");
            GameCenter.GetComponent<Animation>().Play("AandL_out");
        }
    }

    void GameCenterPlayerFailedToLog(string error)
    {
        if (MenuState == E_MenuState.MainMenu)
            StartCoroutine(_ShowGameCenter());
    }

    void GameCenterPlayerLogOut()
    {
        if (MenuState == E_MenuState.MainMenu)
            StartCoroutine(_ShowGameCenter());
    }

    IEnumerator _ShowGameCenter()
    {
        MenuState = E_MenuState.Busy;

        ALBackground.GetComponent<Animation>().Play("AandL_out");
        Achievements.GetComponent<Animation>().Play("AandL_out");
        Leaderboards.GetComponent<Animation>().Play("AandL_out");

        yield return new WaitForSeconds(0.2f);

        ALBackground.GetComponent<Animation>().Play("AandL_in");
        GameCenter.GetComponent<Animation>().Play("AandL_in");

        MenuState = E_MenuState.MainMenu;
    }

	IEnumerator StartSinglePlayerSubMenu()
	{
		MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundMainMenuOut, Position);

        MainBackground.GetComponent<Animation>().Play("toSubmenu");
        MainLine.GetComponent<Animation>().Play("ButtonLineOut");
        SinglePlayer.GetComponent<Animation>().Play("MainButtonsOut");
        Dojo.GetComponent<Animation>().Play("MainButtonsOut");
        Chapters.GetComponent<Animation>().Play("MainButtonsOut");
        Help.GetComponent<Animation>().Play("MainButtonsOut");
        MoreGames.GetComponent<Animation>().Play("MainButtonsOut");
		GameCenter.GetComponent<Animation>().Play("MainButtonsOut");

        yield return new WaitForSeconds(0.3f);

        GameBackground.GetComponent<Animation>().Play("in");
        GameTitle.GetComponent<Animation>().Play("TitleIn");

        if (Game.Instance.IsResumePossible(E_GameType.SinglePlayer))
            GameResume.GetComponent<Animation>().Play("in");

        GameEasy.GetComponent<Animation>().Play("in");
        GameNormal.GetComponent<Animation>().Play("in");
        GameHard.GetComponent<Animation>().Play("in");
        Back.GetComponent<Animation>().Play("in");

        AudioSource.PlayClipAtPoint(SoundSubMenuIn, Position);

        MenuState = E_MenuState.GameSubmenu;
	}

    IEnumerator StartDojoSubMenu()
	{
		MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundMainMenuOut, Position);
		
        MainBackground.GetComponent<Animation>().Play("toSubmenu");
        MainLine.GetComponent<Animation>().Play("ButtonLineOut");
        SinglePlayer.GetComponent<Animation>().Play("MainButtonsOut");
        Dojo.GetComponent<Animation>().Play("MainButtonsOut");
        Chapters.GetComponent<Animation>().Play("MainButtonsOut");
        Help.GetComponent<Animation>().Play("MainButtonsOut");
        MoreGames.GetComponent<Animation>().Play("MainButtonsOut");
		GameCenter.GetComponent<Animation>().Play("MainButtonsOut");

        yield return new WaitForSeconds(0.3f);

        AudioSource.PlayClipAtPoint(SoundSubMenuIn, Position);

        DojoBackground.GetComponent<Animation>().Play("in");
        DojoTitle.GetComponent<Animation>().Play("TitleIn");

        if (Game.Instance.IsResumePossible(E_GameType.Survival))
            DojoResume.GetComponent<Animation>().Play("in");
        
        DojoNew.GetComponent<Animation>().Play("in");
        Back.GetComponent<Animation>().Play("in");

        MenuState = E_MenuState.DojoSubmenu;
	}

    IEnumerator StartChaptersSubMenu()
	{
		MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundMainMenuOut, Position);

        MainBackground.GetComponent<Animation>().Play("toSubmenu");
        MainLine.GetComponent<Animation>().Play("ButtonLineOut");
        SinglePlayer.GetComponent<Animation>().Play("MainButtonsOut");
        Dojo.GetComponent<Animation>().Play("MainButtonsOut");
        Chapters.GetComponent<Animation>().Play("MainButtonsOut");
        Help.GetComponent<Animation>().Play("MainButtonsOut");
        MoreGames.GetComponent<Animation>().Play("MainButtonsOut");
		GameCenter.GetComponent<Animation>().Play("MainButtonsOut");

        yield return new WaitForSeconds(0.3f);

        AudioSource.PlayClipAtPoint(SoundSubMenuIn, Position);

        ChaptersBackground.GetComponent<Animation>().Play("in");
        ChaptersTitle.GetComponent<Animation>().Play("TitleIn");

        ChaptersLocked.GetComponent<Animation>().Play("in");

        int unlockedMissions = Game.Instance.GetUnlockedMission();
        for (int i = 0; i < Chapter.Length && i < unlockedMissions; i++)
        {
            Chapter[i].GetComponent<Animation>().Play("in");
        }

        Back.GetComponent<Animation>().Play("in");

        MenuState = E_MenuState.ChaptersSubmenu;
	}

    IEnumerator StartLeaderboards()
    {
		MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.2f);

        yield return new WaitForSeconds(0.2f);

        MenuState = E_MenuState.MainMenu;
    }

    IEnumerator StartAchievements()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.2f);

        yield return new WaitForSeconds(0.2f);

        MenuState = E_MenuState.MainMenu;

    }

    IEnumerator StartGameCenter()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.2f);
		openFeint.OpenDashboard();
        yield return new WaitForSeconds(0.2f);

        MenuState = E_MenuState.MainMenu;
    }

    IEnumerator StartTutorial()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeOutMusic(4));
        StartCoroutine(FadeInLoading());

        yield return new WaitForSeconds(0.3f);

        Game.Instance.StartTutorial();

        MenuState = E_MenuState.MainMenu;
    }
    
    IEnumerator StartMoreGamesSubMenu()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeOutMusic(4));
//        StartCoroutine(FadeInLoading());

        yield return new WaitForSeconds(0.3f);

        Application.Quit();
        /*
        Game.Instance.StartSaleScreens();

        MenuState = E_MenuState.MainMenu;*/
    }

    IEnumerator StartResumeGame()
    {
   		//MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeOutMusic(4));
        StartCoroutine(FadeInLoading());
        yield return new WaitForSeconds(0.3f);

        Game.Instance.ResumeSinglePlayer();

    }

    IEnumerator StartNewEasyGame()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(FadeOutMusic(4));
        StartCoroutine(FadeInLoading());
        yield return new WaitForSeconds(0.3f);
        Game.Instance.StartNewGame(E_GameDifficulty.Easy);
    }

    IEnumerator StartNewNormalGame()
    {
   		MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(FadeOutMusic(4));
        StartCoroutine(FadeInLoading());
        yield return new WaitForSeconds(0.3f);
        Game.Instance.StartNewGame(E_GameDifficulty.Normal);
    }
    IEnumerator StartNewHardGame()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(FadeOutMusic(4));
        StartCoroutine(FadeInLoading());

        yield return new WaitForSeconds(0.3f);
        Game.Instance.StartNewGame(E_GameDifficulty.Hard);

    }
    IEnumerator StartFromGame2MainMenu()
    {
   		MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundSubMenuOut, Position);

        GameBackground.GetComponent<Animation>().Play("out");
        GameTitle.GetComponent<Animation>().Play("TitleOut");
        
        if (Game.Instance.IsResumePossible(E_GameType.SinglePlayer))
            GameResume.GetComponent<Animation>().Play("out");

        GameEasy.GetComponent<Animation>().Play("out");
        GameNormal.GetComponent<Animation>().Play("out");
        GameHard.GetComponent<Animation>().Play("out");
        Back.GetComponent<Animation>().Play("out");

        yield return new WaitForSeconds(0.3f);

        AudioSource.PlayClipAtPoint(SoundMainMenuIn, Position);

        MainBackground.GetComponent<Animation>().Play("toMainMenu");
        MainLine.GetComponent<Animation>().Play("ButtonLineIn");
        SinglePlayer.GetComponent<Animation>().Play("MainButtonsIn");
        Dojo.GetComponent<Animation>().Play("MainButtonsIn");
        Chapters.GetComponent<Animation>().Play("MainButtonsIn");
        Help.GetComponent<Animation>().Play("MainButtonsIn");
        MoreGames.GetComponent<Animation>().Play("MainButtonsIn");
		GameCenter.GetComponent<Animation>().Play("MainButtonsIn");

        MenuState = E_MenuState.MainMenu;
    }

    IEnumerator StartResumeDojo()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeInLoading());
        yield return new WaitForSeconds(0.3f);
        Game.Instance.ResumeSurvivalMode();
    }

    IEnumerator StartNewDojo()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeInLoading());
        yield return new WaitForSeconds(0.3f);
        Game.Instance.StartSurvivalMode();
    }

    IEnumerator StartFromDojo2MainMenu()
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundSubMenuOut, Position);

        DojoBackground.GetComponent<Animation>().Play("out");
        DojoTitle.GetComponent<Animation>().Play("TitleOut");

        if (Game.Instance.IsResumePossible(E_GameType.Survival))
            DojoResume.GetComponent<Animation>().Play("out");
        DojoNew.GetComponent<Animation>().Play("out");
        Back.GetComponent<Animation>().Play("out");

        yield return new WaitForSeconds(0.3f);

        AudioSource.PlayClipAtPoint(SoundMainMenuIn, Position);

        MainBackground.GetComponent<Animation>().Play("toMainMenu");
        MainLine.GetComponent<Animation>().Play("ButtonLineIn");
        SinglePlayer.GetComponent<Animation>().Play("MainButtonsIn");
        Dojo.GetComponent<Animation>().Play("MainButtonsIn");
        Chapters.GetComponent<Animation>().Play("MainButtonsIn");
        Help.GetComponent<Animation>().Play("MainButtonsIn");
        MoreGames.GetComponent<Animation>().Play("MainButtonsIn");
		GameCenter.GetComponent<Animation>().Play("MainButtonsIn");

        MenuState = E_MenuState.MainMenu;
    }                           

    IEnumerator StartChapter(int index)
    {
        MenuState = E_MenuState.Busy;
        yield return new WaitForSeconds(0.1f);

        FadeOutMusic(4);
        StartCoroutine(FadeInLoading());
        yield return new WaitForSeconds(0.3f);
        Game.Instance.StartChapterMode(index);
    }

    IEnumerator StartFromChapters2MainMenu()
    {
   		MenuState = E_MenuState.Busy;
		yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundSubMenuOut, Position);

        ChaptersLocked.GetComponent<Animation>().Play("out");
        ChaptersBackground.GetComponent<Animation>().Play("out");
        ChaptersTitle.GetComponent<Animation>().Play("TitleOut");

        int unlockedMissions = Game.Instance.GetUnlockedMission();

        for (int i = 0; i < Chapter.Length && i < unlockedMissions; i++)
            Chapter[i].GetComponent<Animation>().Play("out");

        Back.GetComponent<Animation>().Play("out");

        yield return new WaitForSeconds(0.1f);

        AudioSource.PlayClipAtPoint(SoundMainMenuIn, Position);

        MainBackground.GetComponent<Animation>().Play("toMainMenu");
        MainLine.GetComponent<Animation>().Play("ButtonLineIn");
        SinglePlayer.GetComponent<Animation>().Play("MainButtonsIn");
        Dojo.GetComponent<Animation>().Play("MainButtonsIn");
        Chapters.GetComponent<Animation>().Play("MainButtonsIn");
        Help.GetComponent<Animation>().Play("MainButtonsIn");
        MoreGames.GetComponent<Animation>().Play("MainButtonsIn");
		GameCenter.GetComponent<Animation>().Play("MainButtonsIn");

        MenuState = E_MenuState.MainMenu;

    }
    
	IEnumerator FadeInMusic(float speed)
	{
		float volume = 0;
        Music.Play();
		while (volume < MaxMusicVolume)
		{
			volume += speed * Time.deltaTime * MaxMusicVolume;
			if (volume > MaxMusicVolume)
				volume = MaxMusicVolume;

            //Debug.Log(volume);
            Music.volume = volume;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator FadeOutMusic(float speed)
	{
		float volume = MaxMusicVolume;
		while (volume > 0)
		{
			volume -= speed * Time.deltaTime * MaxMusicVolume;
			if (volume < 0)
			{
				Music.Stop();
				volume = 0;
			}

			Music.volume = volume;

			yield return new WaitForEndOfFrame();
		}
	}

     IEnumerator FadeInLoading()
    {
        Loading.SetActiveRecursively(true);
        Material mat = Loading.GetComponent<MeshRenderer>().material;

        Color color = new Color(1, 1, 1, 0);
        mat.SetColor("_Color", color);

        while (color.a < 1)
        {
            color.a += Time.deltaTime * 10;
            if (color.a > 1)
                color.a = 1;

            mat.SetColor("_Color", color);
            yield return new WaitForEndOfFrame();
        }

        color.a = 1;
        mat.SetColor("_Color", color);
    }

     void OnApplicationFocus(bool focus)
     {
         if(focus && Music)
             StartCoroutine(FadeInMusic(0.1f));
     }

    /*
	IEnumerator ShowConfirmDialog()
	{
		MenuState = E_MenuState.E_BUSY;
		Anims.Play("newGameR");
		yield return new WaitForSeconds(0.5f);
//		Anims.Play("fadeoutR");
//		yield return new WaitForSeconds(0.5f);
		ConfirmDialog.animation.Play("fadeIn");
		MenuState = E_MenuState.E_CONFIRM;
	}

	IEnumerator ConfirmDialogYes()
	{
		MenuState = E_MenuState.E_BUSY;
		ConfirmDialog.animation.Play("clickYes");
		yield return new WaitForSeconds(0.1f);
		ConfirmDialog.animation.Play("fadeOut");

		yield return new WaitForSeconds(0.1f);

		StartCoroutine(StartNewGameSubMenu());
	}

	IEnumerator ConfirmDialogNo()
	{
		MenuState = E_MenuState.E_BUSY;
		ConfirmDialog.animation.Play("clickNo");
		yield return new WaitForSeconds(0.4f);
		ConfirmDialog.animation.Play("fadeOut");
		yield return new WaitForSeconds(0.4f);
		MenuState = E_MenuState.E_RDY;
	}
*/
    /*
     void OnLevelWasLoaded()
     {
         Debug.Log("**** factory report ***");
         AgentActionFactory.Report();
         AgentOrderFactory.Report();
         FactsFactory.Report();

         Debug.Log("**** Assets report ***");

        UnityEngine.Object[] textures = Resources.FindObjectsOfTypeAll(typeof(Texture));
        UnityEngine.Object[] audioclips = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
        UnityEngine.Object[] animations = Resources.FindObjectsOfTypeAll(typeof(AnimationClip));
        UnityEngine.Object[] meshes = Resources.FindObjectsOfTypeAll(typeof(Mesh));
        UnityEngine.Object[] components = Resources.FindObjectsOfTypeAll(typeof(Component));
        UnityEngine.Object[] gameobjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        Debug.Log("All " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
        Debug.Log("Textures " + textures.Length);
        Debug.Log("AudioClips " + audioclips.Length);
        Debug.Log("Meshes " + meshes.Length);
        Debug.Log("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
        Debug.Log("Animation " + animations.Length);
        Debug.Log("GameObjects " + gameobjects.Length);
        Debug.Log("Components " + components.Length);


        Debug.Log("**** textures ***");

        for (int i = 0; i < textures.Length; i++)
        {
            Texture t = textures[i] as Texture;
            Debug.Log("Texture " + t.name + " - " + t.width +"x" + t.height);
        }

        Debug.Log("**** Audio ***");
        for (int i = 0; i < audioclips.Length; i++)
        {
            AudioClip t = audioclips[i] as AudioClip;
            Debug.Log("audio " + t.name + " - " + t.length);
        }

        Debug.Log("**** Animation ***");

        for (int i = 0; i < animations.Length; i++)
        {
            AnimationClip t = animations[i] as AnimationClip;
            Debug.Log("animation " + t.name + " - " + t.length);
        }

        Debug.Log("**** Meshes ***");
        for (int i = 0; i < meshes.Length; i++)
        {
            Mesh t = meshes[i] as Mesh;
            Debug.Log("mesh " + t.name);
        }

        Debug.Log("**** Components ***");
        for (int i = 0; i < components.Length; i++)
        {
            Component t = components[i] as Component;
            Debug.Log("component " + t.name);
        }

        Debug.Log("**** GameObjects ***");
        for (int i = 0; i < gameobjects.Length; i++)
        {
            GameObject t = gameobjects[i] as GameObject;
            Debug.Log("gameobject " + t.name);
        }
     }*/
}
