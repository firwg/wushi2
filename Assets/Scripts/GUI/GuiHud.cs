using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuiHud
{
    Sprite[] SpriteComboProgress;

    Sprite SpriteHealthBackground;
    Sprite SpriteHealth;
    Sprite SpriteHealthBar;

    Sprite SpriteHitsMessage;
    Sprite SpriteComboMessage;

    GuiNumbers SpriteHitNumbers;
    GuiNumbers SpriteMoneyNumbers;
    //GuiNumbers SpriteScoreNumbers;

	class BloodEffectsUI
	{
        public List<Sprite> GuiSprites = new List<Sprite>();
        public List<Sprite> GuiSpritesInUse = new List<Sprite>();
		public const int MaxBloodTypes = 4;
	}
	
	private BloodEffectsUI BloodSplashes = new BloodEffectsUI();

    private bool On = false;
    private bool ComboMessageOn = false;
    private int HardwareIndex;

    private GuiManager GuiManager;
    private SpriteUI DefaultSpriteUI;

    public void Start(GuiManager manager)
    {
        GuiManager = manager;
        DefaultSpriteUI = GuiManager.DefaultSpriteUI;

        SpriteComboProgress = new Sprite[] {
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft -  GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft -  GuiCombatProgress.CPScreenWidth * 2, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft -  GuiCombatProgress.CPScreenWidth * 3, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft -  GuiCombatProgress.CPScreenWidth * 4, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft -  GuiCombatProgress.CPScreenWidth * 5, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CPScreenLeft -  GuiCombatProgress.CPScreenWidth * 6, GuiCombatProgress.CPScreenBottom), GuiCombatProgress.CPScreenWidth, GuiCombatProgress.CPScreenHeight, 9, GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop, GuiCombatProgress.XUvWidth, GuiCombatProgress.XUvHeight),
        };

        SpriteHealthBackground = DefaultSpriteUI.AddElement(new Vector2(GuiHealthBar.BScreenLeft, GuiHealthBar.BScreenBottom), GuiHealthBar.BScreenWidth, GuiHealthBar.BScreenHeight, 9, GuiHealthBar.BUvLeft, GuiHealthBar.BUvTop, GuiHealthBar.BUvWidth, GuiHealthBar.BUvHeight);
        if (SpriteHealthBackground == null)
        {
            Debug.Log("null");
        }
        else
        {
            //Debug.Log("!null");
        }
        
        SpriteHealthBar = DefaultSpriteUI.AddElement(new Vector2(GuiHealthBar.HBScreenLeft, GuiHealthBar.HBScreenBottom), GuiHealthBar.HBScreenWidth, GuiHealthBar.HBScreenHeight, 9, GuiHealthBar.HBUvLeft, GuiHealthBar.HBUvTop, GuiHealthBar.HBUvWidth, GuiHealthBar.HBUvHeight);
        SpriteHealth = DefaultSpriteUI.AddElement(new Vector2(GuiHealthBar.HScreenLeft, GuiHealthBar.HScreenBottom), GuiHealthBar.HScreenWidth, GuiHealthBar.HScreenHeight, 9, GuiHealthBar.HUvLeft, GuiHealthBar.HUvTop, GuiHealthBar.HUvWidth, GuiHealthBar.HUvHeight);

        SpriteHitsMessage = DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.HScreenLeft, GuiCombatProgress.HScreenBottom), GuiCombatProgress.HScreenWidth, GuiCombatProgress.HScreenHeight, 9, GuiCombatProgress.HUvLeft, GuiCombatProgress.HUvTop, GuiCombatProgress.HUvWidth, GuiCombatProgress.HUvHeight);

        SpriteHitNumbers = new GuiNumbers()
        {
            UvLeft = GuiHitNumbers.Instance.UvLeft,
            UvTop = GuiHitNumbers.Instance.UvTop,
            UvWidth = GuiHitNumbers.Instance.UvWidth,
            UvHeight = GuiHitNumbers.Instance.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiHitNumbers.Instance.ScreenLeft, GuiHitNumbers.Instance.ScreenBottom),GuiHitNumbers.Instance.ScreenWidth, GuiHitNumbers.Instance.ScreenHeight, 9, GuiHitNumbers.Instance.UvLeft, GuiHitNumbers.Instance.UvTop, GuiHitNumbers.Instance.UvWidth, GuiHitNumbers.Instance.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiHitNumbers.Instance.ScreenLeft - GuiHitNumbers.Instance.ScreenWidth , GuiHitNumbers.Instance.ScreenBottom),GuiHitNumbers.Instance.ScreenWidth, GuiHitNumbers.Instance.ScreenHeight, 9, GuiHitNumbers.Instance.UvLeft, GuiHitNumbers.Instance.UvTop, GuiHitNumbers.Instance.UvWidth, GuiHitNumbers.Instance.UvHeight),
            }
        };

        SpriteComboMessage = DefaultSpriteUI.AddElement(new Vector2(GuiCombatProgress.CMScreenLeft, GuiCombatProgress.CMScreenBottom), GuiCombatProgress.CMScreenWidth, GuiCombatProgress.CMScreenHeight, 9, GuiCombatProgress.CMUvLeft[0], GuiCombatProgress.CMUvTop[0], GuiCombatProgress.CMUvWidth, GuiCombatProgress.CMUvHeight);

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            SpriteMoneyNumbers = new GuiNumbers()
            {
                UvLeft = GuiMoneyNumbers.Instance.UvLeft,
                UvTop = GuiMoneyNumbers.Instance.UvTop,
                UvWidth = GuiMoneyNumbers.Instance.UvWidth,
                UvHeight = GuiMoneyNumbers.Instance.UvHeight,
                Sprites = new Sprite[]{
                    DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.Instance.ScreenLeft, GuiMoneyNumbers.Instance.ScreenBottom),GuiMoneyNumbers.Instance.ScreenWidth, GuiMoneyNumbers.Instance.ScreenHeight, 9, GuiMoneyNumbers.Instance.UvLeft, GuiMoneyNumbers.Instance.UvTop, GuiMoneyNumbers.Instance.UvWidth, GuiMoneyNumbers.Instance.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.Instance.ScreenLeft - GuiMoneyNumbers.Instance.ScreenWidth, GuiMoneyNumbers.Instance.ScreenBottom),GuiMoneyNumbers.Instance.ScreenWidth, GuiMoneyNumbers.Instance.ScreenHeight, 9, GuiMoneyNumbers.Instance.UvLeft, GuiMoneyNumbers.Instance.UvTop, GuiMoneyNumbers.Instance.UvWidth, GuiMoneyNumbers.Instance.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.Instance.ScreenLeft - GuiMoneyNumbers.Instance.ScreenWidth * 2, GuiMoneyNumbers.Instance.ScreenBottom),GuiMoneyNumbers.Instance.ScreenWidth, GuiMoneyNumbers.Instance.ScreenHeight, 9, GuiMoneyNumbers.Instance.UvLeft, GuiMoneyNumbers.Instance.UvTop, GuiMoneyNumbers.Instance.UvWidth, GuiMoneyNumbers.Instance.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.Instance.ScreenLeft - GuiMoneyNumbers.Instance.ScreenWidth * 3, GuiMoneyNumbers.Instance.ScreenBottom),GuiMoneyNumbers.Instance.ScreenWidth, GuiMoneyNumbers.Instance.ScreenHeight, 9, GuiMoneyNumbers.Instance.UvLeft, GuiMoneyNumbers.Instance.UvTop, GuiMoneyNumbers.Instance.UvWidth, GuiMoneyNumbers.Instance.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.Instance.ScreenLeft - GuiMoneyNumbers.Instance.ScreenWidth * 4, GuiMoneyNumbers.Instance.ScreenBottom),GuiMoneyNumbers.Instance.ScreenWidth, GuiMoneyNumbers.Instance.ScreenHeight, 9, GuiMoneyNumbers.Instance.UvLeft, GuiMoneyNumbers.Instance.UvTop, GuiMoneyNumbers.Instance.UvWidth, GuiMoneyNumbers.Instance.UvHeight)
                }
            };
        }

        /*SpriteScoreNumbers = new GuiNumbers()
        {
            UvLeft = GuiScoreNumbers.UvLeft[HardwareIndex],
            UvTop = GuiScoreNumbers.UvTop[HardwareIndex],
            UvWidth = GuiScoreNumbers.UvWidth[HardwareIndex],
            UvHeight = GuiScoreNumbers.UvHeight[HardwareIndex],
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreNumbers.ScreenLeft[HardwareIndex], GuiScoreNumbers.ScreenBottom[HardwareIndex]),GuiScoreNumbers.ScreenWidth[HardwareIndex], GuiScoreNumbers.ScreenHeight[HardwareIndex], 9, GuiScoreNumbers.UvLeft[HardwareIndex], GuiScoreNumbers.UvTop[HardwareIndex], GuiScoreNumbers.UvWidth[HardwareIndex], GuiScoreNumbers.UvHeight[HardwareIndex]),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreNumbers.ScreenLeft[HardwareIndex] - GuiScoreNumbers.ScreenWidth[HardwareIndex], GuiScoreNumbers.ScreenBottom[HardwareIndex]),GuiScoreNumbers.ScreenWidth[HardwareIndex], GuiScoreNumbers.ScreenHeight[HardwareIndex], 9, GuiScoreNumbers.UvLeft[HardwareIndex], GuiScoreNumbers.UvTop[HardwareIndex], GuiScoreNumbers.UvWidth[HardwareIndex], GuiScoreNumbers.UvHeight[HardwareIndex]),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreNumbers.ScreenLeft[HardwareIndex] - GuiScoreNumbers.ScreenWidth[HardwareIndex] * 2, GuiScoreNumbers.ScreenBottom[HardwareIndex]),GuiScoreNumbers.ScreenWidth[HardwareIndex], GuiScoreNumbers.ScreenHeight[HardwareIndex], 9, GuiScoreNumbers.UvLeft[HardwareIndex], GuiScoreNumbers.UvTop[HardwareIndex], GuiScoreNumbers.UvWidth[HardwareIndex], GuiScoreNumbers.UvHeight[HardwareIndex]),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreNumbers.ScreenLeft[HardwareIndex] - GuiScoreNumbers.ScreenWidth[HardwareIndex] * 3, GuiScoreNumbers.ScreenBottom[HardwareIndex]),GuiScoreNumbers.ScreenWidth[HardwareIndex], GuiScoreNumbers.ScreenHeight[HardwareIndex], 9, GuiScoreNumbers.UvLeft[HardwareIndex], GuiScoreNumbers.UvTop[HardwareIndex], GuiScoreNumbers.UvWidth[HardwareIndex], GuiScoreNumbers.UvHeight[HardwareIndex]),
                DefaultSpriteUI.AddElement(new Vector2(GuiScoreNumbers.ScreenLeft[HardwareIndex] - GuiScoreNumbers.ScreenWidth[HardwareIndex] * 4, GuiScoreNumbers.ScreenBottom[HardwareIndex]),GuiScoreNumbers.ScreenWidth[HardwareIndex], GuiScoreNumbers.ScreenHeight[HardwareIndex], 9, GuiScoreNumbers.UvLeft[HardwareIndex], GuiScoreNumbers.UvTop[HardwareIndex], GuiScoreNumbers.UvWidth[HardwareIndex], GuiScoreNumbers.UvHeight[HardwareIndex])
            }
        };*/
                
        SpriteComboMessage.clientTransform.Rotate(SpriteComboMessage.clientTransform.forward, 5.0f);

        GuiSaving.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiSaving.Instance.ScreenLeft, GuiSaving.Instance.ScreenBottom), GuiSaving.Instance.ScreenWidth, GuiSaving.Instance.ScreenHeight, 9, GuiSaving.Instance.UvLeft, GuiSaving.Instance.UvTop, GuiSaving.Instance.UvWidth, GuiSaving.Instance.UvHeight);

        Sprite s;
        for (int i = 0; i < 16; i++)
        {
            s = DefaultSpriteUI.AddElement(new Vector2(0, 0), 0, 0, 7, 0, 0, 0, 0);
            DefaultSpriteUI.HideSprite(s);
            BloodSplashes.GuiSprites.Add(s);
        }

        Hide();
	}

	public void Update()
	{
	}

	public void Reset()
	{
		while (BloodSplashes.GuiSpritesInUse.Count > 0)
		{
			Sprite s = BloodSplashes.GuiSpritesInUse[0];
			DefaultSpriteUI.HideSprite(s);
			BloodSplashes.GuiSprites.Add(s);
			BloodSplashes.GuiSpritesInUse.Remove(s);
		}

        ComboMessageOn = false;
		SetHitsCount(0);

        Hide();
	}

    public void Show()
    {
        On = true;

        DefaultSpriteUI.ShowSprite(SpriteHealthBackground);
        DefaultSpriteUI.ShowSprite(SpriteHealth);
        DefaultSpriteUI.ShowSprite(SpriteHealthBar);

        DefaultSpriteUI.HideSprite(SpriteComboMessage);
        DefaultSpriteUI.HideSprite(SpriteHitsMessage);
        DefaultSpriteUI.HideSprite(SpriteHitNumbers.Sprites[0]);
        DefaultSpriteUI.HideSprite(SpriteHitNumbers.Sprites[1]);

        if (SpriteMoneyNumbers != null)
        {
            DefaultSpriteUI.ShowSprite(SpriteMoneyNumbers.Sprites[0]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[1]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[2]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[3]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[4]);
        }

        /*DefaultSpriteUI.ShowSprite(SpriteScoreNumbers.Sprites[0]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[1]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[2]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[3]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[4]);*/

        DefaultSpriteUI.HideSprite(GuiSaving.Instance.Sprite);

        SetMoney(Game.Instance.Money, Game.Instance.Money);

        //SetScore(Game.Instance.Score, Game.Instance.Score);
    }

    public void Hide()
    {

        DefaultSpriteUI.HideSprite(SpriteHealthBackground);
        DefaultSpriteUI.HideSprite(SpriteHealth);
        DefaultSpriteUI.HideSprite(SpriteHealthBar);

        DefaultSpriteUI.HideSprite(SpriteComboMessage);
        DefaultSpriteUI.HideSprite(SpriteHitsMessage);
        DefaultSpriteUI.HideSprite(SpriteHitNumbers.Sprites[0]);
        DefaultSpriteUI.HideSprite(SpriteHitNumbers.Sprites[1]);

        if (SpriteMoneyNumbers != null)
        {
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[0]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[1]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[2]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[3]);
            DefaultSpriteUI.HideSprite(SpriteMoneyNumbers.Sprites[4]);
        }

        /*DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[0]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[1]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[2]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[3]);
        DefaultSpriteUI.HideSprite(SpriteScoreNumbers.Sprites[4]);*/

        while (BloodSplashes.GuiSpritesInUse.Count > 0)
        {
            Sprite s = BloodSplashes.GuiSpritesInUse[0];
            DefaultSpriteUI.HideSprite(s);
            BloodSplashes.GuiSprites.Add(s);
            BloodSplashes.GuiSpritesInUse.Remove(s);
        }

        DefaultSpriteUI.HideSprite(GuiSaving.Instance.Sprite);

        On = false;
    }


    public void ShowBloodSplash()
    {
        if (On == false)
            return;

        for (int i = 0; i < 16; i++)
        {
           if (Random.Range(0, 100) < 25)
               GuiManager.StartCoroutine(ShowBlood(new Vector2(GuiBlood.BloodLeft(i) - GuiBlood.BloodSize * 0.5f, GuiBlood.BloodBottom(i) - GuiBlood.BloodSize * 0.5f), UnityEngine.Random.Range(0, BloodEffectsUI.MaxBloodTypes - 1)));
        }
    }

    public void ShowSaveProgress()
    {
        GuiManager.StartCoroutine(_ShowSaving());
    }

    IEnumerator _ShowSaving()
    {
        Sprite s = GuiSaving.Instance.Sprite;

        DefaultSpriteUI.ShowSprite(s);
        Color c = Color.white;
        c.a = 0;
        s.SetColor(c);

        while (s.color.a < 1)
        {
            c.a = Mathf.Min(1.0f, c.a + 2 * Time.deltaTime);
            s.SetColor(c);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);

        while(s.color.a > 0)
        {
            c.a = Mathf.Max(0.0f, c.a - 2 * Time.deltaTime);
            s.SetColor(c);
            yield return new WaitForEndOfFrame();
        }

        DefaultSpriteUI.HideSprite(s);
    }

    public void SetMoney(int old, int experience)
    {
        if (On == false || SpriteMoneyNumbers == null)
            return;

       // Debug.Log("SetExperience " + experience + " " + old);

        //Debug.Log(old + " " + experience);
        if (experience == 0 || old == experience)
            GuiManager.ShowNumbers(SpriteMoneyNumbers, experience, 99999);
        else
            GuiManager.StartCoroutine(ShowNumbers(SpriteMoneyNumbers,old, experience, 0.3f));
    }

    /*public void SetScore(int old, int experience)
    {
        if (On == false)
            return;

        //Debug.Log("SetExperience " + experience + " " + old);

        //Debug.Log(old + " " + experience);
        if (experience == 0 || old == experience)
            GuiManager.ShowNumbers(SpriteScoreNumbers, experience, 99999);
        else
            GuiManager.StartCoroutine(ShowNumbers(SpriteScoreNumbers, old, experience, 0.5f));
    }*/


	public void SetHealthPercent(float currentHealth, float maxHealth)
	{
        if (On == false)
            return;

        float maxWidth = GuiHealthBar.HScreenWidth;
        if (Game.Instance.HealthLevel == E_HealthLevel.One)
            maxWidth *= 0.5f;
        else if (Game.Instance.HealthLevel == E_HealthLevel.Two)
            maxWidth *= 0.75f;

        
        float width = (currentHealth / maxHealth) *  maxWidth;

        //Debug.Log("SetHealthPercent " + currentHealth + " " + maxHealth + " " + width + " " + maxWidth);

		if (currentHealth == 0)
			DefaultSpriteUI.HideSprite(SpriteHealth);
		else
		{
            DefaultSpriteUI.ShowSprite(SpriteHealth);
            DefaultSpriteUI.UpdateSpriteSize(SpriteHealth, new Vector2(GuiHealthBar.HScreenLeft, GuiHealthBar.HScreenBottom), width, GuiHealthBar.HScreenHeight);
		}

        DefaultSpriteUI.UpdateSpriteSize(SpriteHealthBar, new Vector2(GuiHealthBar.HBScreenLeft, GuiHealthBar.HBScreenBottom), maxWidth, GuiHealthBar.HBScreenHeight);

	}

	public void SetHitsCount(int hits, bool blink = true)
	{
        if (On == false || ComboMessageOn == true)
            return;

		if (hits == 0)
		{
            GuiManager.StopCoroutine("BlinkSprite");

			if (SpriteHitsMessage.hidden == false)
                GuiManager.StartCoroutine(DisappearSprite(SpriteHitsMessage, 0.2f));

            if (SpriteHitNumbers.Sprites[0].hidden == false)
               GuiManager.StartCoroutine(DisappearSprite(SpriteHitNumbers.Sprites[0], 0.2f));

            if (SpriteHitNumbers.Sprites[1].hidden == false)
                GuiManager.StartCoroutine(DisappearSprite(SpriteHitNumbers.Sprites[1], 0.2f));
        }
		else
		{
			// of	unlockAchievement
			if(hits == 10) {
				Achievements.UnlockAchievement(11);
			}else if(hits == 50) {
				Achievements.UnlockAchievement(12);
			}else if(hits == 100) {
				Achievements.UnlockAchievement(13);
			}
			if (hits > 99)
				hits = 99;

            //Debug.Log("show sprites");

            if (SpriteHitsMessage.hidden)
                GuiManager.StartCoroutine(ShowSprite(SpriteHitsMessage, 0.2f));

            if (blink)
                GuiManager.StartCoroutine(BlinkSprite(SpriteHitsMessage, 1.8f, 10, 1, 0.0f, GuiCombatProgress.HScreenWidth, GuiCombatProgress.HScreenHeight));

			int number = hits % 10;
			int tents = hits / 10;

            if (SpriteHitNumbers.Sprites[0].hidden)
                GuiManager.StartCoroutine(ShowSprite(SpriteHitNumbers.Sprites[0], 0.2f));

            SpriteHitNumbers.Sprites[0].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiHitNumbers.Instance.UvLeft + GuiHitNumbers.Instance.UvWidth * number, GuiHitNumbers.Instance.UvTop);
            
            if (blink)
                GuiManager.StartCoroutine(BlinkSprite(SpriteHitNumbers.Sprites[0], 1.6f, 6, 4, 0.1f, GuiHitNumbers.Instance.ScreenWidth, GuiHitNumbers.Instance.ScreenHeight));

			if (hits > 9)
			{
                if (SpriteHitNumbers.Sprites[1].hidden)
                    GuiManager.StartCoroutine(ShowSprite(SpriteHitNumbers.Sprites[1], 0.2f));

                SpriteHitNumbers.Sprites[1].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiHitNumbers.Instance.UvLeft + GuiHitNumbers.Instance.UvWidth * tents, GuiHitNumbers.Instance.UvTop);

                if (blink)
                    GuiManager.StartCoroutine(BlinkSprite(SpriteHitNumbers.Sprites[1], 1.6f, 6, 4, 0.1f, GuiHitNumbers.Instance.ScreenWidth, GuiHitNumbers.Instance.ScreenHeight));
			}
		}
	}

    public void ShowComboProgress(List<E_AttackType> comboProgress)
    {
        for (int i = 0; i < SpriteComboProgress.Length; i++)
            DefaultSpriteUI.HideSprite(SpriteComboProgress[i]);

        if (comboProgress.Count == 0)
           return;

        //Debug.Log("show combo progress " + comboProgress.Count);
        int spriteIndex = 0;

        DefaultSpriteUI.ShowSprite(SpriteComboProgress[spriteIndex]);
        SpriteComboProgress[spriteIndex].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiCombatProgress.LUvLeft, GuiCombatProgress.LUvTop);

        spriteIndex++;
        for (int i = comboProgress.Count - 1; i >= 0 ; i--, spriteIndex++)
        {
            DefaultSpriteUI.ShowSprite(SpriteComboProgress[spriteIndex]);
            if(comboProgress[i] == E_AttackType.X)
                SpriteComboProgress[spriteIndex].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiCombatProgress.XUvLeft, GuiCombatProgress.XUvTop);
            else
                SpriteComboProgress[spriteIndex].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiCombatProgress.OUvLeft, GuiCombatProgress.OUvTop);
        }

        DefaultSpriteUI.ShowSprite(SpriteComboProgress[spriteIndex]);
        SpriteComboProgress[spriteIndex].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiCombatProgress.FUvLeft, GuiCombatProgress.FUvTop);
    }

    public void ShowComboMessage(int ComboIndex)
    {
        SpriteComboMessage.lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiCombatProgress.CMUvLeft[ComboIndex], GuiCombatProgress.CMUvTop[ComboIndex]);
        GuiManager.StopCoroutine("BlinkMessage");

        
        GuiManager.StartCoroutine(BlinkComboMessage(SpriteComboMessage, 1.6f, 2, 1, 0.1f, GuiCombatProgress.CMUvWidth, GuiCombatProgress.CMUvHeight));
    }

    IEnumerator BlinkComboMessage(Sprite s, float scale, float speed, int count, float decreaseScale, float basewidth, float baseHeight)
    {
        ComboMessageOn = true;

        if (SpriteHitsMessage.hidden == false)
            GuiManager.StartCoroutine(DisappearSprite(SpriteHitsMessage, 0.2f));

        for (int i = 0; i < SpriteHitNumbers.Sprites.Length; i++)
        {
            if (SpriteHitNumbers.Sprites[i].hidden == false)
                GuiManager.StartCoroutine(DisappearSprite(SpriteHitNumbers.Sprites[i], 0.2f));
        }

        yield return new WaitForSeconds(0.1f);

        GuiManager.StartCoroutine(ShowSprite(SpriteComboMessage, 0.2f));

        float newWidth;
        float newHeight;
        Color c = Color.white;

        for (int i = 0; i < count; i++)
        {

            float progress = 0;

            while (progress < 2)
            {
                progress += speed * Time.deltaTime;

                if (progress > 2)
                    progress = 2;

                newWidth = Mathfx.Sinerp(basewidth, basewidth * scale, progress);
                newHeight = Mathfx.Sinerp(baseHeight, baseHeight * scale, progress);
                s.SetSizeXY(newWidth, newHeight);

                if(progress > 1)
                {
                    c.a = 2 - progress;
                    s.SetColor(c);
                }
                yield return new WaitForEndOfFrame();
            }
            scale -= decreaseScale;
        }

        DefaultSpriteUI.HideSprite(s);

        ComboMessageOn = false;
        SetHitsCount(Game.Instance.Hits, false);
    }

    IEnumerator BlinkSprite(Sprite s, float scale, float speed, int count, float decreaseScale, float basewidth, float baseHeight)
    {
        float newWidth;
        float newHeight;

        for (int i = 0; i < count; i++)
        {
            float progress = 0;

            while (progress < 2)
            {
                progress += speed * Time.deltaTime;

                if (progress > 2)
                    progress = 2;

                newWidth = Mathfx.Sinerp(basewidth, basewidth * scale, progress);
                newHeight = Mathfx.Sinerp(baseHeight, baseHeight * scale, progress);
                s.SetSizeXY(newWidth, newHeight);

                yield return new WaitForEndOfFrame();
            }

            scale -= decreaseScale;
        }
    }
   
    IEnumerator ShowSprite(Sprite s, float time)
    {
        DefaultSpriteUI.ShowSprite(s);

        float step = 1 / time;
        Color c = Color.white;

        c.a = 0;
        s.SetColor(c);

        while (s.color.a < 1)
        {
            c.a = Mathf.Min(1.0f, c.a + step * Time.deltaTime);

            s.SetColor(c);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DisappearSprite(Sprite s, float time)
    {
        float step =  1/time;
        Color c = Color.white;

        while(s.color.a > 0)
        {
            c.a = Mathf.Max(0.0f, c.a - step * Time.deltaTime);
            s.SetColor(c);
            yield return new WaitForEndOfFrame();
        }

        DefaultSpriteUI.HideSprite(s);
    }

    IEnumerator ShowNumbers(GuiNumbers numbers, int from, int to, float time)
    {
        float step = (to - from)/time;

        if (from < to)
        {
            while(from < to)
            {
                from = Mathf.CeilToInt(Mathf.Min(from + step * Time.deltaTime, to));
                GuiManager.ShowNumbers(numbers, from, 99999);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (from > to)
            {
                from = Mathf.CeilToInt(Mathf.Min(from - step * Time.deltaTime, to));
                GuiManager.ShowNumbers(numbers, from, 99999);
                yield return new WaitForEndOfFrame();
            }
        }

    }

    IEnumerator ShowBlood(Vector2 pos, int type)
	{
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 0.2f));

        if (BloodSplashes.GuiSprites.Count == 0)
            yield break;

        Sprite s = BloodSplashes.GuiSprites[0];
		BloodSplashes.GuiSprites.Remove(s);
		BloodSplashes.GuiSpritesInUse.Add(s);

		Color alphaColor = new Color(1,1,1);
		const float speed = 5;
		const float speedFade = 0.25f;
		
        float size = Random.Range(0.8f, 1.0f);
        s.clientTransform.eulerAngles = new Vector3(0,0,UnityEngine.Random.Range(0, 360));

        DefaultSpriteUI.UpdateSpriteSize(s, pos, GuiBlood.UvWidth * size, GuiBlood.UvHeight * size,
            GuiBlood.UvLeft[type], GuiBlood.UvTop[type], GuiBlood.UvWidth, GuiBlood.UvHeight);
		DefaultSpriteUI.ShowSprite(s);

		while (alphaColor.a < 1)
		{
			alphaColor.a += Time.deltaTime * speed;
			if (alphaColor.a > 1)
				alphaColor.a = 1;

			s.SetColor(alphaColor);
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.8f, 1.5f));

		while (alphaColor.a > 0)
		{
			alphaColor.a -= Time.deltaTime * speedFade;
			if (alphaColor.a < 0)
				alphaColor.a = 0;

			s.SetColor(alphaColor);
			yield return new WaitForEndOfFrame();
		}
		DefaultSpriteUI.HideSprite(s);
		BloodSplashes.GuiSprites.Add(s);
		BloodSplashes.GuiSpritesInUse.Remove(s);
	}

}

