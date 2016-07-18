using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScoreManager : MonoBehaviour
{
	public SpriteUI DefaultSpriteUI = null;
	// Alpha start value
	public float startAlpha = 1;
	// Default time a fade takes in seconds
	public float fadeDuration = 1;
	// Fade into scene at start
	public bool fadeIntoScene = true;
	// Current alpha of the texture
	private float currentAlpha = 1;
	// Current duration of the fade
	private float currentDuration;
	// Direction of the fade
	private int fadeDirection = -1;
	// Fade alpha to
	private float targetAlpha = 0;
	// Alpha difference
	private float alphaDifference = 0;
	// Color object for alpha setting
    private Color alphaColor = new Color();

	private Sprite Fade;
    private AudioSource Audio;

    private GuiNumbers Kills;
    private GuiNumbers CritCount;
    private GuiNumbers CritScore;
    private static int CritPoint = 25;

    private GuiNumbers KnockCount;
    private GuiNumbers KnockScore;
    private static int KnockdownPoint = 50;

    private GuiNumbers BreakBlockCount;
    private GuiNumbers BreakBlockScore;
    private static int BreakBlockPoint = 10;

    private GuiNumbers BlockedCount;
    private GuiNumbers BlockedScore;
    private static int BlockedPoint = -5;

    private GuiNumbers InjuryCount;
    private GuiNumbers InjuryScore;
    private static int InjuryPoint = -20;

    private GuiNumbers DeathCount;
    private GuiNumbers DeathScore;
    private static int DeathPoint = -250;

    private GuiNumbers FinalScore;

    public AudioClip[] ControlSounds = null;

    const float MaxMusicVolume = 0.0f;

    AudioClip ControlSound { get { return ControlSounds[Random.Range(0, ControlSounds.Length)]; } }

	void Awake()
	{
        Audio = GetComponent<AudioSource>();
	}

    void Start ()
	{
        DefaultSpriteUI.AddElement(new Vector2(GuiScore.Instance.ScreenLeft, GuiScore.Instance.ScreenBottom), GuiScore.Instance.ScreenWidth, GuiScore.Instance.ScreenHeight, 9, GuiScore.Instance.UvLeft, GuiScore.Instance.UvTop, GuiScore.Instance.UvWidth, GuiScore.Instance.UvHeight);

        DefaultSpriteUI.AddElement(new Vector2(GuiScoreContinue.Instance.ScreenLeft, GuiScoreContinue.Instance.ScreenBottom), GuiScoreContinue.Instance.ScreenWidth, GuiScoreContinue.Instance.ScreenHeight, 9, GuiScoreContinue.Instance.UvLeft, GuiScoreContinue.Instance.UvTop, GuiScoreContinue.Instance.UvWidth, GuiScoreContinue.Instance.UvHeight);

        int rowOfseet = 0;
        Kills = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 4 , GuiScoreTable.ScoreScreenBottom),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        if (Game.Instance.GameDifficulty == E_GameDifficulty.Easy)
            Game.Instance.Score = (int)(Game.Instance.Score * 0.75f);
        else if (Game.Instance.GameDifficulty == E_GameDifficulty.Hard)
            Game.Instance.Score = (int)(Game.Instance.Score * 1.5f);

        ShowNumbers(Kills, Game.Instance.Score, 99999);

        rowOfseet = GuiScoreTable.RowHeight * 2;
        CritCount = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft, GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth * 2 , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            },
        };

        ShowNumbers(CritCount,  Game.Instance.NumberOfCriticals, 999);

        CritScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(CritScore, Game.Instance.NumberOfCriticals * CritPoint, 9999);

        rowOfseet = GuiScoreTable.RowHeight * 3;

        KnockCount = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft, GuiScoreTable.CountScreenBottom - rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth * 2 , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(KnockCount, Game.Instance.NumberOfKnockdowns, 999);

        KnockScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(KnockScore, Game.Instance.NumberOfKnockdowns * KnockdownPoint, 9999);

        rowOfseet = GuiScoreTable.RowHeight * 4;

        BreakBlockCount = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft, GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth * 2 , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(BreakBlockCount, Game.Instance.NumberOfBreakBlocks, 999);

        BreakBlockScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(BreakBlockScore, Game.Instance.NumberOfBreakBlocks * BreakBlockPoint, 9999);

        rowOfseet = GuiScoreTable.RowHeight * 6;

        BlockedCount = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft, GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth * 2 , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(BlockedCount, Game.Instance.NumberOfBlockedHits, 999);

        BlockedScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            },
            Minus = DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom - rowOfseet), GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft + GuiScoreNumbers.UvWidth * 10, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
            MinusLeftScreen = GuiScoreTable.ScoreScreenLeft,
            MinusBottomScreen = GuiScoreTable.ScoreScreenBottom - rowOfseet,
        };

        ShowNumbers(BlockedScore, Game.Instance.NumberOfBlockedHits * BlockedPoint, 9999);

        rowOfseet = GuiScoreTable.RowHeight * 7;

        InjuryCount = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft, GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth * 2 , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(InjuryCount, Game.Instance.NumberOfInjuries, 999);

        InjuryScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            },
            Minus = DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom - rowOfseet), GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft + GuiScoreNumbers.UvWidth * 10, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
            MinusLeftScreen = GuiScoreTable.ScoreScreenLeft,
            MinusBottomScreen = GuiScoreTable.ScoreScreenBottom - rowOfseet,
        };

        ShowNumbers(InjuryScore, Game.Instance.NumberOfInjuries * InjuryPoint, 9999);

        rowOfseet = GuiScoreTable.RowHeight * 8;

        DeathCount = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft, GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.CountScreenLeft - GuiScoreTable.CountScreenWidth * 2 , GuiScoreTable.CountScreenBottom- rowOfseet),GuiScoreTable.CountScreenWidth, GuiScoreTable.CountScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            }
        };

        ShowNumbers(DeathCount, Game.Instance.NumberOfDeath, 999);

        DeathScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft,
            UvTop = GuiScoreNumbers.UvTop,
            UvWidth = GuiScoreNumbers.UvWidth,
            UvHeight = GuiScoreNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 2 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft - GuiScoreTable.ScoreScreenWidth * 3 , GuiScoreTable.ScoreScreenBottom- rowOfseet),GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight)
            },
            Minus = DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.ScoreScreenLeft, GuiScoreTable.ScoreScreenBottom - rowOfseet), GuiScoreTable.ScoreScreenWidth, GuiScoreTable.ScoreScreenHeight, 9, GuiScoreNumbers.UvLeft + GuiScoreNumbers.UvWidth * 10, GuiScoreNumbers.UvTop, GuiScoreNumbers.UvWidth, GuiScoreNumbers.UvHeight),
            MinusLeftScreen = GuiScoreTable.ScoreScreenLeft,
            MinusBottomScreen = GuiScoreTable.ScoreScreenBottom - rowOfseet,
        };

        ShowNumbers(DeathScore, Game.Instance.NumberOfDeath * DeathPoint, 9999);

        FinalScore = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbersFinal.UvLeft,
            UvTop = GuiScoreNumbersFinal.UvTop,
            UvWidth = GuiScoreNumbersFinal.UvWidth,
            UvHeight = GuiScoreNumbersFinal.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.FinalScreenLeft, GuiScoreTable.FinalScreenBottom),GuiScoreTable.FinalScreenWidth, GuiScoreTable.FinalScreenHeight, 9, GuiScoreNumbersFinal.UvLeft, GuiScoreNumbersFinal.UvTop, GuiScoreNumbersFinal.UvWidth, GuiScoreNumbersFinal.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.FinalScreenLeft - GuiScoreTable.FinalScreenWidth , GuiScoreTable.FinalScreenBottom),GuiScoreTable.FinalScreenWidth, GuiScoreTable.FinalScreenHeight, 9, GuiScoreNumbersFinal.UvLeft, GuiScoreNumbersFinal.UvTop, GuiScoreNumbersFinal.UvWidth, GuiScoreNumbersFinal.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.FinalScreenLeft - GuiScoreTable.FinalScreenWidth * 2 , GuiScoreTable.FinalScreenBottom),GuiScoreTable.FinalScreenWidth, GuiScoreTable.FinalScreenHeight, 9, GuiScoreNumbersFinal.UvLeft, GuiScoreNumbersFinal.UvTop, GuiScoreNumbersFinal.UvWidth, GuiScoreNumbersFinal.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.FinalScreenLeft - GuiScoreTable.FinalScreenWidth * 3 , GuiScoreTable.FinalScreenBottom),GuiScoreTable.FinalScreenWidth, GuiScoreTable.FinalScreenHeight, 9, GuiScoreNumbersFinal.UvLeft, GuiScoreNumbersFinal.UvTop, GuiScoreNumbersFinal.UvWidth, GuiScoreNumbersFinal.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreTable.FinalScreenLeft - GuiScoreTable.FinalScreenWidth * 4 , GuiScoreTable.FinalScreenBottom),GuiScoreTable.FinalScreenWidth, GuiScoreTable.FinalScreenHeight, 9, GuiScoreNumbersFinal.UvLeft, GuiScoreNumbersFinal.UvTop, GuiScoreNumbersFinal.UvWidth, GuiScoreNumbersFinal.UvHeight)
            }
        };
       
        int score = Game.Instance.Score + Game.Instance.NumberOfDeath * DeathPoint + Game.Instance.NumberOfInjuries * InjuryPoint + Game.Instance.NumberOfBlockedHits * BlockedPoint + Game.Instance.NumberOfBreakBlocks * BreakBlockPoint + Game.Instance.NumberOfKnockdowns * KnockdownPoint  + Game.Instance.NumberOfCriticals * CritPoint;

        if (score < 0)
            score = 0;

        ShowNumbers(FinalScore, score, 99999);

        Fade = DefaultSpriteUI.AddElement(new Vector2(0, 0), Screen.width, Screen.height, 10, GuiScoreBlack.UvLeft, GuiScoreBlack.UvTop, GuiScoreBlack.UvWidth, GuiScoreBlack.UvHeight);

        GuiScoreLoading.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiScoreLoading.Instance.ScreenLeft, GuiScoreLoading.Instance.ScreenBottom), GuiScoreLoading.Instance.ScreenWidth, GuiScoreLoading.Instance.ScreenHeight, 9, GuiScoreLoading.Instance.UvLeft, GuiScoreLoading.Instance.UvTop, GuiScoreLoading.Instance.UvWidth, GuiScoreLoading.Instance.UvHeight);
        DefaultSpriteUI.HideSprite(GuiScoreLoading.Instance.Sprite);

		FadeIn();
		
		
		// 娓告垙涓?績鏁版嵁缁熻?
		
		// 鍙戝姩蹇呮潃鎶娆℃暟
		int uniqueSkillCount = Game.Instance.NumberOfCriticals;
		PlayerPrefs.SetInt("uniqueSkillCount", PlayerPrefs.GetInt("uniqueSkillCount") + uniqueSkillCount);
		uniqueSkillCount = PlayerPrefs.GetInt("uniqueSkillCount");
		
		// 绉掓潃鏁屼汉娆℃暟
		int koCount = Game.Instance.NumberOfKnockdowns;
		PlayerPrefs.SetInt("koCount", PlayerPrefs.GetInt("koCount") + koCount);
		koCount = PlayerPrefs.GetInt("koCount");
		
		// 鐮撮槻娆℃暟
		int breakBlockCount = Game.Instance.NumberOfBreakBlocks;
		PlayerPrefs.SetInt("breakBlockCount", PlayerPrefs.GetInt("breakBlockCount") + breakBlockCount);
		breakBlockCount = PlayerPrefs.GetInt("breakBlockCount");
		
		// 琚?姷鎸＄殑鏀诲嚮娆℃暟
		int blockedHits = Game.Instance.NumberOfBlockedHits;
		PlayerPrefs.SetInt("blockedHits", PlayerPrefs.GetInt("blockedHits") + blockedHits);
		blockedHits = PlayerPrefs.GetInt("blockedHits");
		
		// 鍙椾激娆℃暟
		int injuries = Game.Instance.NumberOfInjuries;
		PlayerPrefs.SetInt("injuries", PlayerPrefs.GetInt("injuries") + injuries);
		injuries = PlayerPrefs.GetInt("injuries");
		
		// 姝讳骸娆℃暟
		int death = Game.Instance.NumberOfDeath;
		PlayerPrefs.SetInt("death", PlayerPrefs.GetInt("death") + death);
		death = PlayerPrefs.GetInt("death");
		
		
		Game game = Game.Instance;
		// 閬撳満
		if(game.GameType == E_GameType.Survival){
		
		}
		// 鏁呬簨妯″紡鎴栫珷鑺傛ā寮幪
		else {
			if(game.GameDifficulty == E_GameDifficulty.Easy) {
				
			}
			else if(game.GameDifficulty == E_GameDifficulty.Normal) {
				
			}
			else {
				
			}
		}
		
		
		// 涓婁紶鍒嗘暟
		if(Game.Instance.GameType == E_GameType.Survival && score > 0){
			MainMenu.openFeint.SubmitScore("916954412", score);
		}
		
		// 鎴愬氨
		/*
		鎵撶垎50涓?湪妗堤
		姝讳骸50娆犔
		姝讳骸100娆犔
		瀛︿細娴?炕銆佸崐鏈堛佷簯鍔ㄣ侀?榫欍佽笍姝汇佺牬灏吿
		鍓戝＋绾у埆閫氬叧
		鍓戝湥绾у埆閫氬叧
		10娆¤繛鍑禾
		50娆¤繛鍑禾
		100娆¤繛鍑禾
		鏉姝诲ぇ铔囦父
		鍓戣豹绾у埆瀹屾垚绗?銆?銆?銆?銆?銆?銆?鍏蔡
		鐢熷懡鍊肩瓑绾ц揪鍒版渶澶μ
		瀹屾垚鎵鏈夐毦搴ヌ
        */
		
		// 
		
		
	}

	void Update()
	{
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (touch.position.x > GuiScoreContinue.Instance.ScreenLeft && touch.position.x < GuiScoreContinue.Instance.ScreenLeft + GuiScoreContinue.Instance.ScreenWidth &&
               touch.position.y > GuiScoreContinue.Instance.ScreenBottom && touch.position.y < GuiScoreContinue.Instance.ScreenBottom + GuiScoreContinue.Instance.ScreenHeight)
            {
                PlayButtonSound();
                StartCoroutine(LoadNextLevel());
                
            }
        }
	}

    IEnumerator LoadNextLevel()
    {
        FadeOut();

        yield return new WaitForSeconds(1);

        DefaultSpriteUI.ShowSprite(GuiScoreLoading.Instance.Sprite);

        Resources.UnloadUnusedAssets();

        if (Game.Instance.GameType == E_GameType.ChapterOnly || Game.Instance.GameType == E_GameType.Survival)
            Game.Instance.LoadMainMenu();
        else
            Game.Instance.LoadNextLevel(Game.Instance.NextLevelToLoad, 0);
    }

    public void ShowNumbers(GuiNumbers numbers, int number, int max)
    {
        int absNumber = Mathf.Abs(number);

        if (absNumber > max)
            absNumber = max;

        int one = absNumber % 10;
        int tents = (absNumber % 100) / 10;
        int hundreds = (absNumber % 1000) / 100;
        int thousands = (absNumber % 10000) / 1000;
        int hundredsthousands = absNumber / 10000;

        //Debug.Log(ToString() + " " + hundredsthousands.ToString() + " "  + thousands.ToString() + " " + hundreds.ToString() + " " + tents.ToString() + " " + one.ToString());

        int minusPosition = 1;

        DefaultSpriteUI.ShowSprite(numbers.Sprites[0]);
        numbers.Sprites[0].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * one, numbers.UvTop);

        if (absNumber > 9)
        {
            DefaultSpriteUI.ShowSprite(numbers.Sprites[1]);
            numbers.Sprites[1].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * tents, numbers.UvTop);
            minusPosition++;
        }
        else if (numbers.Sprites.Length > 1)
            DefaultSpriteUI.HideSprite(numbers.Sprites[1]);
        
        if (absNumber > 99)
        {
            DefaultSpriteUI.ShowSprite(numbers.Sprites[2]);
            numbers.Sprites[2].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * hundreds, numbers.UvTop);
            minusPosition++;
        }
        else if (numbers.Sprites.Length > 2)
            DefaultSpriteUI.HideSprite(numbers.Sprites[2]);

        if (absNumber > 999)
        {
            DefaultSpriteUI.ShowSprite(numbers.Sprites[3]);
            numbers.Sprites[3].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * thousands, numbers.UvTop);
            minusPosition++;
        }
        else if (numbers.Sprites.Length > 3)
            DefaultSpriteUI.HideSprite(numbers.Sprites[3]);

        if (absNumber > 9999)
        {
            DefaultSpriteUI.ShowSprite(numbers.Sprites[4]);
            numbers.Sprites[4].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * hundredsthousands, numbers.UvTop);
            minusPosition++;
        }
        else if (numbers.Sprites.Length > 4)
            DefaultSpriteUI.HideSprite(numbers.Sprites[4]);

        if (numbers.Minus)
        {
            if (number < 0)
            {
                DefaultSpriteUI.SetSpritePosition(numbers.Minus, new Vector2(numbers.MinusLeftScreen - numbers.UvWidth * minusPosition, numbers.MinusBottomScreen ));
                DefaultSpriteUI.ShowSprite(numbers.Minus);
            }
            else
                DefaultSpriteUI.HideSprite(numbers.Minus);
        }
    }

    public void PlayButtonSound()
    {
        Audio.PlayOneShot(ControlSound);
    }


	public void FadeIn(float duration, float to)
	{
		// Set fade duration
		currentDuration = duration;
		// Set target alpha
		targetAlpha = to;
		// Difference
		alphaDifference = Mathf.Clamp01(currentAlpha - targetAlpha);
		// Set direction to Fade in
		fadeDirection = -1;
	}

    public void SetFadeOut(float fadeOut)
    {
        currentAlpha = fadeOut;
        targetAlpha = fadeOut;
        alphaColor.a = fadeOut;
        Fade.SetColor(alphaColor);

        if (currentAlpha == fadeOut)
			DefaultSpriteUI.HideSprite(Fade);
		else
			DefaultSpriteUI.ShowSprite(Fade);

    }

	public void FadeIn()
	{
		FadeIn(fadeDuration, 0);
	}

	public void FadeIn(float duration)
	{
		FadeIn(duration, 0);
	}

	public void FadeOut(float duration, float to)
	{
		// Set fade duration
		currentDuration = duration;
		// Set target alpha
		targetAlpha = to;
		// Difference
		alphaDifference = Mathf.Clamp01(targetAlpha - currentAlpha);
		// Set direction to fade out
		fadeDirection = 1;
	}

	public void FadeOut()
	{
		FadeOut(fadeDuration, 1);
	}

	public void FadeOut(float duration)
	{
		FadeOut(duration, 1);
	}

	void LateUpdate()
	{
		// Fade alpha if active
		if ((fadeDirection == -1 && currentAlpha > targetAlpha) ||
			 (fadeDirection == 1 && currentAlpha < targetAlpha))
		{
			// Advance fade by fraction of full fade time
			currentAlpha += (fadeDirection * alphaDifference) * (Time.deltaTime / currentDuration);
			// Clamp to 0-1
			currentAlpha = Mathf.Clamp01(currentAlpha);
		}

		// Draw only if not transculent
		if (currentAlpha >= 0 && currentAlpha <= 1)
		{
			alphaColor.a = currentAlpha;
			Fade.SetColor(alphaColor);
		}

		if (currentAlpha == 0)
			DefaultSpriteUI.HideSprite(Fade);
		else
			DefaultSpriteUI.ShowSprite(Fade);
	}
}

