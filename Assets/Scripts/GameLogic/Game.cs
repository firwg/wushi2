using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour 
{
	private string _CurrentLevel;
    private int _CurrentGameZone;
    private int _CurrentSurvivalRound;

    private int _Score = 0;
	private int _Money = 90000;
    private int _Hits = 90000;
    private float TimeToClearHits; 

    [System.NonSerialized]
    public E_ComboLevel[] ComboLevel = { E_ComboLevel.One, E_ComboLevel.One, E_ComboLevel.One, E_ComboLevel.One, E_ComboLevel.One, E_ComboLevel.One };
    [System.NonSerialized]
    public E_SwordLevel SwordLevel = E_SwordLevel.One;
    [System.NonSerialized]
    public E_HealthLevel HealthLevel = E_HealthLevel.One;


	private E_GameState _GameState = E_GameState.Game; // for editor
    private E_GameType _GameType = E_GameType.ChapterOnly; //for editor game play

	private static Game _Instance = null;
    
    public static bool IsLowEndHardware() { return false; }

    //statistic
    private int _NumberOfDeath;
    public int NumberOfDeath { get { return _NumberOfDeath; } set { _NumberOfDeath = value; if (GameState == E_GameState.Game) PlayerPrefs.SetInt("NumberOfDeath", NumberOfDeath); } }
    [System.NonSerialized]
    public int NumberOfInjuries;
    [System.NonSerialized]
    public int NumberOfBlockedHits;
    [System.NonSerialized]
    public int NumberOfKnockdowns;
    [System.NonSerialized]
    public int NumberOfCriticals;
    [System.NonSerialized]
    public int NumberOfBreakBlocks;
    [System.NonSerialized]
    public int NumberOfBarrels;

    int _disabled = 0;
    public int DisabledState { get { return _disabled; } set { _disabled = value; } }

    //player progress
    public int Score { get { return _Score; } set { _Score = value; } }
    public int Money { get { return _Money; } set {int old = _Money; _Money = value; if (GuiManager.Instance) GuiManager.Instance.SetMoney(old, _Money);}
    }
    public int Hits { get { return _Hits; } 
        set { 
            _Hits = value; 
            TimeToClearHits = Time.timeSinceLevelLoad + 3;
            if (GameState == E_GameState.Game && GuiManager.Instance)
            {
                GuiManager.Instance.SetHitsCount(_Hits);
            }
        }
    }

    public int LeaderBoardID;

    public static E_HealtLevelPrice[] PriceHealth = { E_HealtLevelPrice.One, E_HealtLevelPrice.Two, E_HealtLevelPrice.Three };
    public static E_SwordLevelPrice[] PriceSword = { E_SwordLevelPrice.One, E_SwordLevelPrice.Two, E_SwordLevelPrice.Three, E_SwordLevelPrice.Four, E_SwordLevelPrice.Five };
    public static E_ComboLevelPrice[] PriceCombo = { E_ComboLevelPrice.One, E_ComboLevelPrice.Two, E_ComboLevelPrice.Three };

    public bool HealthLevelMaxed() { return HealthLevel == E_HealthLevel.Max; }
    public bool SwordLevelMaxed() { return SwordLevel == E_SwordLevel.Max; }
    public bool ComboLevelMaxed(int index) { return ComboLevel[index] == E_ComboLevel.Max; }

    public bool CouldBuyHealthLevel() { return  HealthLevelMaxed() == false && (int)PriceHealth[(int)HealthLevel] <= Money;}
    public bool CouldBuySwordLevel()  { return  SwordLevelMaxed() == false && (int)PriceSword[(int)SwordLevel] <= Money;}
    public bool CouldBuyComboLevel(int index) { return ComboLevelMaxed(index) == false && (int)PriceCombo[(int)ComboLevel[index]] <= Money;}

	public string CurrentLevel { get { return _CurrentLevel; } set { _CurrentLevel = value; } }
    public int CurrentGameZone { get { return _CurrentGameZone; } set { _CurrentGameZone = value; } }
    public int CurrentSurvivalRound { get { return _CurrentSurvivalRound; } set { _CurrentSurvivalRound = value; } }

    public E_GameState GameState { get { return _GameState; } set { _GameState = value; } }
    public E_GameDifficulty GameDifficulty = E_GameDifficulty.Normal;
    public E_GameType GameType { get { return _GameType; } }

    public Vector2 ControlsButtonOffset = Vector2.zero;
    public Vector2 ControlsJoystickOffset = Vector2.zero;

    private Vector2 _ButtonXPositon;
    public Vector2 ButtonXPositon { get { return ControlsButtonOffset + _ButtonXPositon;} }

    private Vector2 _ButtonOPositon;
    public Vector2 ButtonOPositon { get { return ControlsButtonOffset + _ButtonOPositon; } }

    private Vector2 _ButtonRPositon;
    public Vector2 ButtonRPositon { get { return ControlsButtonOffset + _ButtonRPositon; } }

    private Vector2 _JoystickPositon;
    public Vector2 JoystickPositon { get { return ControlsJoystickOffset + _JoystickPositon; } }

    private Vector2 _JoystickHatPositon;
    public Vector2 JoystickHatPositon { get { return ControlsJoystickOffset + _JoystickHatPositon; } }

    [System.NonSerialized]
    public string NextLevelToLoad;

	public bool FirstTimePlayer { get { return PlayerPrefs.GetInt("firstTime", 0) == 0; } }

    private bool GameCenterSupported = true;
    private bool GameCenterPlayerIsLogged = false;

    public static Game Instance { get { return _Instance; } }

    void Awake()
	{
		if (_Instance)
		{
			Destroy(this);
			return;
		}
		_Instance = this;
		DontDestroyOnLoad(this);
	
    	/*System.Object[] tmp = new System.Object[1024];
	    // make allocations in smaller blocks to avoid them to be treated in a special way, which is designed for large blocks
        for (int i = 0; i < 1024; i++)
		    tmp[i] = new byte[1024];
	
        // release reference
        t
         *  = null;*/
        LoadControls();

        _ButtonXPositon = new Vector2(GuiButtonX.Instance.ScreenLeft, GuiButtonX.Instance.ScreenBottom);
        _ButtonOPositon = new Vector2(GuiButtonY.Instance.ScreenLeft, GuiButtonY.Instance.ScreenBottom);
        _ButtonRPositon = new Vector2(GuiButtonRoll.Instance.ScreenLeft, GuiButtonRoll.Instance.ScreenBottom);
        _JoystickPositon = new Vector2(GuiJoystick.Instance.ScreenLeft, GuiJoystick.Instance.ScreenBottom);
        _JoystickHatPositon = new Vector2(GuiJoystickHat.ScreenLeft, GuiJoystickHat.ScreenBottom);

        //?????????iPhoneKeyboard.autorotateToPortrait = false;
		//?????????iPhoneKeyboard.autorotateToPortraitUpsideDown = false;

        /*UnlockMission(2);

        UnlockHealth(E_HealthLevel.Two);
        UnlockSword(E_SwordLevel.Five);
        UnlockCombo(0, E_ComboLevel.Three);
        UnlockCombo(1, E_ComboLevel.Three);
        UnlockCombo(2, E_ComboLevel.Three);
        UnlockCombo(3, E_ComboLevel.Three);
        UnlockCombo(4, E_ComboLevel.Two);
        UnlockCombo(5, E_ComboLevel.One);*/
	}

    public void Save_Save()
    {
        //Debug.Log("Save lv: " + Application.loadedLevel + " zone: " + ( Mission.Instance? Mission.Instance.GameZoneIndex: 0)+ "money " + Money);
        if (Game.Instance.DisabledState > 1)
            return;

        PlayerPrefs.SetInt(GameType + "SaveExist", 1);
        PlayerPrefs.SetString(GameType + "Level", Application.loadedLevelName);
        PlayerPrefs.SetInt(GameType + "GameZone", Mission.Instance ? Mission.Instance.GameZoneIndex : 0);
        PlayerPrefs.SetInt(GameType + "SurvivalRound", CurrentSurvivalRound);
        PlayerPrefs.SetInt(GameType + "Difficulty", (int)GameDifficulty);

        PlayerPrefs.SetInt(GameType + "Money", Money);

        PlayerPrefs.SetInt(GameType + "Sword", (int)SwordLevel);
        PlayerPrefs.SetInt(GameType + "Health", (int)HealthLevel);

        for (int i = 0; i < ComboLevel.Length; i++)
            PlayerPrefs.SetInt(GameType + "Combo" + i, (int)ComboLevel[i]);

        if (GuiManager.Instance)
            GuiManager.Instance.ShowSaveProgress();
    }

    public void Save_Clear()
    {
        //Debug.Log("clear save");
        PlayerPrefs.SetInt(GameType + "SaveExist", 0);
        PlayerPrefs.SetString(GameType + "Level", "level01");
        PlayerPrefs.SetInt(GameType + "GameZone", 0);
        PlayerPrefs.SetInt(GameType + "SurvivalRound", 0);
        PlayerPrefs.SetInt(GameType + "Difficulty", 0);

        PlayerPrefs.SetInt(GameType + "Money", 0);

        PlayerPrefs.SetInt(GameType + "Sword", (int)E_SwordLevel.One);
        PlayerPrefs.SetInt(GameType + "Health", (int)E_HealthLevel.One);

        for (int i = 0; i < ComboLevel.Length; i++)
            PlayerPrefs.SetInt(GameType + "Combo" + i, (int)E_ComboLevel.One);
    }

    public void Save_Load()
    {
        CurrentLevel = PlayerPrefs.GetString(GameType + "Level", "level01");
        CurrentGameZone = PlayerPrefs.GetInt(GameType + "GameZone", 0);
        CurrentSurvivalRound = PlayerPrefs.GetInt(GameType + "SurvivalRound", 0);
        GameDifficulty = (E_GameDifficulty)PlayerPrefs.GetInt(GameType + "Difficulty", 0);

        Money = PlayerPrefs.GetInt(GameType + "Money", 0);
        SwordLevel = (E_SwordLevel)PlayerPrefs.GetInt(GameType + "Sword", 0);
        HealthLevel = (E_HealthLevel)PlayerPrefs.GetInt(GameType + "Health", 0);

        for (int i = 0; i < ComboLevel.Length; i++)
            ComboLevel[i] = (E_ComboLevel)PlayerPrefs.GetInt(GameType + "Combo" + i, 0);

    }

    public void ClearStatistics()
    {
        //Debug.Log("ClearStatistics");
        PlayerPrefs.SetInt(GameType + "NumberOfInjuries", 0);
        PlayerPrefs.SetInt(GameType + "NumberOfDeath", 0);
        PlayerPrefs.SetInt(GameType + "NumberOfBlockedHits", 0);

        PlayerPrefs.SetInt(GameType + "Score", 0);

        PlayerPrefs.SetInt(GameType + "NumberOfKnockdowns", 0);
        PlayerPrefs.SetInt(GameType + "NumberOfCriticals", 0);
        PlayerPrefs.SetInt(GameType + "NumberOfBreakBlocks", 0);
        PlayerPrefs.SetInt(GameType + "NumberOfBarrels", 0);

        LoadStatistics();
    }

    public void SaveStatistics()
    {
        //Debug.Log("SaveStatistics");
        PlayerPrefs.SetInt(GameType + "NumberOfInjuries", NumberOfInjuries);
        PlayerPrefs.SetInt(GameType + "NumberOfDeath", NumberOfDeath);
        PlayerPrefs.SetInt(GameType + "NumberOfBlockedHits", NumberOfBlockedHits);

        PlayerPrefs.SetInt(GameType + "Score", Score);

        PlayerPrefs.SetInt(GameType + "NumberOfKnockdowns", NumberOfKnockdowns);
        PlayerPrefs.SetInt(GameType + "NumberOfCriticals", NumberOfCriticals);
        PlayerPrefs.SetInt(GameType + "NumberOfBreakBlocks", NumberOfBreakBlocks);
        PlayerPrefs.SetInt(GameType + "NumberOfBarrels", NumberOfBarrels);
    }

    public void LoadStatistics()
    {
        //Debug.Log("LoadStatistics");
        NumberOfInjuries = PlayerPrefs.GetInt(GameType + "NumberOfInjuries", 0);
        NumberOfDeath = PlayerPrefs.GetInt(GameType + "NumberOfDeath", 0);
        Score = PlayerPrefs.GetInt(GameType + "Score", 0);

        NumberOfBlockedHits = PlayerPrefs.GetInt(GameType + "NumberOfBlockedHits", 0);
        NumberOfKnockdowns = PlayerPrefs.GetInt(GameType + "NumberOfKnockdowns", 0);
        NumberOfCriticals = PlayerPrefs.GetInt(GameType + "NumberOfCriticals", 0);
        NumberOfBreakBlocks = PlayerPrefs.GetInt(GameType + "NumberOfBreakBlocks", 0);
        NumberOfBarrels = PlayerPrefs.GetInt(GameType + "NumberOfBarrels", 0);
    }


    public void UnlockMission(int count)
    {
        if(count >  GetUnlockedMission())
            PlayerPrefs.SetInt("MissionsUnlocked", count);
    }

    public int GetUnlockedMission()
    {
        return PlayerPrefs.GetInt("MissionsUnlocked", 0);
    }

    public void UnlockCombo(int comboIndex, E_ComboLevel comboLevel)
    {
        if((E_ComboLevel)PlayerPrefs.GetInt("Combo " + comboIndex, (int)E_ComboLevel.One) < comboLevel)
            PlayerPrefs.SetInt("Combo " + comboIndex, (int)comboLevel);
		if(comboLevel == E_ComboLevel.Max) {
			Achievements.UnlockAchievement(3 + comboIndex);
		}
    }

    public E_ComboLevel GetUnlockedCombo(int comboIndex)
    {
        return (E_ComboLevel)PlayerPrefs.GetInt("Combo " + comboIndex, (int)E_ComboLevel.One);
    }

    public void UnlockHealth(E_HealthLevel heatlh)
    {
        if ((E_HealthLevel)PlayerPrefs.GetInt("Health ", (int)E_HealthLevel.One) < heatlh)
            PlayerPrefs.SetInt("Health ", (int)heatlh);
    }

    public E_HealthLevel GetUnlockedHealth()
    {
        return (E_HealthLevel)PlayerPrefs.GetInt("Health ", (int)E_HealthLevel.One);
    }

    public void UnlockSword(E_SwordLevel sword)
    {
        if ((E_SwordLevel)PlayerPrefs.GetInt("Sword ", (int)E_SwordLevel.One) < sword)
            PlayerPrefs.SetInt("Sword ", (int)sword);
    }

    public E_SwordLevel GetUnlockedSword()
    {
        return (E_SwordLevel)PlayerPrefs.GetInt("Sword ", (int)E_SwordLevel.One);
    }

    /*
    public void SaveControls()
    {
        PlayerPrefs.SetFloat(PlayerControls.E_ButtonsName.AttackX + "_X", );
        PlayerPrefs.SetFloat(PlayerControls.E_ButtonsName.AttackX + "_Y", );
    }*/


    public bool IsResumePossible(E_GameType gameType)
	{
        return PlayerPrefs.GetInt(gameType + "SaveExist", 0) == 1;
	}

	public void LoadMainMenu()
	{
        ClearInstances();

		if(GameState == E_GameState.MainMenu)
			return;

		/*if(GameState == E_GameState.Game || GameState == E_GameState.IngameMenu)
			SpriteEffectsManager.Instance.ReleaseShadows();*/

		Time.timeScale = 1.0f;
		Money = 0;
		Application.LoadLevel("MainMenu");
	}

	public void StartNewGame(E_GameDifficulty difficulty)
	{
        ClearInstances();

        _GameType = E_GameType.SinglePlayer;

    	Save_Clear();
        Save_Load();
        ClearStatistics();


        GameDifficulty = difficulty;
        
        Money = 0;


       // Debug.Log("start new game - first " + FirstTimePlayer);
		if (FirstTimePlayer)
		{
            _GameType = E_GameType.FirstTimeTutorial;
			PlayerPrefs.SetInt("firstTime", 1);
            
            CurrentLevel = "tutorial";
            Application.LoadLevel("empty");
			Application.LoadLevel(CurrentLevel);
		}
		else
		{
            CurrentLevel = "Comics01";
            Application.LoadLevel("empty");
			Application.LoadLevel(CurrentLevel);
		}
	}

	public void ResumeSinglePlayer()
	{
        if (Game.Instance.DisabledState > 1)
        {
            StartNewGame(E_GameDifficulty.Hard);
            return;
        }

        ClearInstances();
        _GameType = E_GameType.SinglePlayer;
        Save_Load();
        LoadStatistics();
		Application.LoadLevel(CurrentLevel);
	}

    public void StartChapterMode(int index)
    {
        string[] chapters = {"level01", "level01b", "level02", "level03", "level05", "level06", "level07"};
        ClearInstances();

        _GameType = E_GameType.ChapterOnly;

        Save_Clear();
        Save_Load();
        ClearStatistics();
        
        GameDifficulty = E_GameDifficulty.Hard;
        CurrentLevel = chapters[index];

        ComboLevel = new E_ComboLevel[] { GetUnlockedCombo(0), GetUnlockedCombo(1), GetUnlockedCombo(2), GetUnlockedCombo(3), GetUnlockedCombo(4), GetUnlockedCombo(5) };
        SwordLevel = GetUnlockedSword();
        HealthLevel = GetUnlockedHealth();

        Application.LoadLevel(CurrentLevel);
    }

    public void ResumeSurvivalMode()
    {
        if (Game.Instance.DisabledState > 1)
        {
            StartSurvivalMode();
            return;
        }
        ClearInstances();
        _GameType = E_GameType.Survival;

        Save_Load();
        LoadStatistics();

        GameDifficulty = E_GameDifficulty.Hard;
        _CurrentLevel = "dojo";

        Application.LoadLevel(CurrentLevel);
    }

    public void StartSurvivalMode()
    {
        ClearInstances();
        _GameType = E_GameType.Survival;

        ClearStatistics();
        Save_Clear();
        Save_Load();

        GameDifficulty = E_GameDifficulty.Hard;

        ComboLevel = new E_ComboLevel[] { GetUnlockedCombo(0), GetUnlockedCombo(1), GetUnlockedCombo(2), GetUnlockedCombo(3), GetUnlockedCombo(4), GetUnlockedCombo(5) };
        SwordLevel = GetUnlockedSword();
        HealthLevel = GetUnlockedHealth();

        _CurrentLevel = "dojo";

        Application.LoadLevel(CurrentLevel);
    }

	public void StartTutorial()
	{
        ClearInstances();

		PlayerPrefs.SetInt("firstTime", 1);
        _GameType = E_GameType.Tutorial;
        _GameState = E_GameState.Tutorial;

        CurrentLevel = "tutorial";

		Application.LoadLevel(CurrentLevel);
	}

	public void StartSaleScreens()
	{
        return;
        /*
        _GameType = E_GameType.SaleScreen;
        _GameState = E_GameState.SaleScreen;

        if (Game.Instance.HardwareType == E_HardwareType.iPad)
            CurrentLevel = 30;
        else
            CurrentLevel = 29;

		Application.LoadLevel(CurrentLevel);
         */ 
	}

    public void LoadScoreScreen()
    {
        ClearInstances();
        Application.LoadLevel("empty");
        Application.LoadLevel("ScoreScreen");
    }

	public void LoadNextLevel(string nextLevel, int currentZone)
	{
        //Debug.Log("Loading next level " + nextLevel);
		CurrentLevel = nextLevel;
        CurrentGameZone = currentZone;

        ClearInstances();

        ClearStatistics();

        Application.LoadLevel("empty");

       /* AgentActionFactory.Report();
        AgentOrderFactory.Report();
        FactsFactory.Report();*/

        Resources.UnloadUnusedAssets();

		Application.LoadLevel(nextLevel);
	}



    public void SaveControls()
    {
        PlayerPrefs.SetFloat("ControlsButtonOffsetX", ControlsButtonOffset.x);
        PlayerPrefs.SetFloat("ControlsButtonOffsetY", ControlsButtonOffset.y);
        PlayerPrefs.SetFloat("ControlsJoystickOffsetX", ControlsJoystickOffset.x);
        PlayerPrefs.SetFloat("ControlsJoystickOffsetY", ControlsJoystickOffset.y);
    }

	public void LoadControls()
	{
        ControlsButtonOffset.x = PlayerPrefs.GetFloat("ControlsButtonOffsetX", 0);
        ControlsButtonOffset.y = PlayerPrefs.GetFloat("ControlsButtonOffsetY", 0);
        ControlsJoystickOffset.x = PlayerPrefs.GetFloat("ControlsJoystickOffsetX", 0);
        ControlsJoystickOffset.y = PlayerPrefs.GetFloat("ControlsJoystickOffsetY", 0);
	}

    public void CleatControls()
    {
        ControlsButtonOffset.x = 0;
        ControlsButtonOffset.y = 0;
        ControlsJoystickOffset.x = 0;
        ControlsJoystickOffset.y = 0;
    }

	public void WriteScore()
	{

	}

    public void BuyHealthLevel()
    {
        Money -= (int)PriceHealth[(int)HealthLevel];

        if (HealthLevel == E_HealthLevel.One)
        {
            HealthLevel = E_HealthLevel.Two;
        }
        else if (HealthLevel == E_HealthLevel.Two)
        {
            HealthLevel = E_HealthLevel.Three;
        }

        if(GameType == E_GameType.SinglePlayer)
            UnlockHealth(HealthLevel);

        GuiManager.Instance.SetHealthPercent(Player.Instance.Agent.BlackBoard.Health, Player.Instance.Agent.BlackBoard.RealMaxHealth);

        AudioSource.PlayClipAtPoint(SoundDataManager.Instance.ShopBuyHealth, Camera.main.transform.position);
    }

    public void BuySwordLevel()
    {
        Money -= (int)PriceSword[(int)SwordLevel];

        int percent = (int)(((float)(SwordLevel) / 4.0f) * 100);

        AudioSource.PlayClipAtPoint(SoundDataManager.Instance.ShopBuySword, Camera.main.transform.position);

        SwordLevel++;
        if (SwordLevel > E_SwordLevel.Max)
            SwordLevel = E_SwordLevel.Max;

        if (GameType == E_GameType.SinglePlayer)
            UnlockSword(SwordLevel);

    }

    public void BuyComboLevel(int index)
    {
        int price = (int)PriceCombo[(int)ComboLevel[index]];
        Money -= price;

        if (ComboLevel[index] == E_ComboLevel.One)
        {
            ComboLevel[index] = E_ComboLevel.Two;
        }
        else if (ComboLevel[index] == E_ComboLevel.Two)
        {
            ComboLevel[index] = E_ComboLevel.Three;
        }

        if (GameType == E_GameType.SinglePlayer)
            UnlockCombo(index, ComboLevel[index]);

        AudioSource.PlayClipAtPoint(SoundDataManager.Instance.ShopBuyCombo, Camera.main.transform.position);
    }

    void ClearInstances()
    {
        Player.Instance = null;
        GuiManager.Instance = null;
        SpriteEffectsManager.Instance = null;
        CameraBehaviour.Instance = null;
        CombatEffectsManager.Instance = null;
        SoundDataManager.Instance = null;
        ProjectileManager.Instance = null;
        Mission.Instance = null;
    }

    void FixedUpdate()
    {
        if (TimeToClearHits < Time.timeSinceLevelLoad && Hits > 0)
            Hits = 0;

        //////// screen orientation
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) && (Screen.orientation != ScreenOrientation.LandscapeLeft))
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight) && (Screen.orientation != ScreenOrientation.LandscapeRight))
            Screen.orientation = ScreenOrientation.LandscapeRight;

    }

    void GameCenterPlayerLog()
    {
        GameCenterSupported = true;
        GameCenterPlayerIsLogged = true;
    }

    void GameCenterNotSupported()
    {
        GameCenterSupported = false;
        GameCenterPlayerIsLogged = false;
    }

    void GameCenterPlayerFailedToLog(string error)
    {
        GameCenterSupported = true;
        GameCenterPlayerIsLogged = false;
    }

    void GameCenterPlayerLogOut()
    {
        GameCenterSupported = true;
        GameCenterPlayerIsLogged = false;
    }

    public bool IsGameCenterPlayerLogged()
    {
        return GameCenterPlayerIsLogged;
    }
}
