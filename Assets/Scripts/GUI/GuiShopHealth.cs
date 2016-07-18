using UnityEngine;
using System.Collections;
using System;

/*
 * 
        Numbers = new GuiNumbers()
        {
            UvLeft = GuiShopNumbers.UvLeft[hardware],
            UvTop = GuiShopNumbers.UvTop[hardware],
            UvWidth = GuiShopNumbers.UvWidth[hardware],
            UvHeight = GuiShopNumbers.UvHeight[hardware],
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.ScreenLeft[hardware], GuiMoneyNumbers.ScreenBottom[hardware]),GuiMoneyNumbers.ScreenWidth[hardware], GuiMoneyNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
                DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.ScreenLeft[hardware] - GuiMoneyNumbers.ScreenWidth[hardware] , GuiMoneyNumbers.ScreenBottom[hardware]),GuiMoneyNumbers.ScreenWidth[hardware], GuiMoneyNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
                DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.ScreenLeft[hardware] - GuiMoneyNumbers.ScreenWidth[hardware] * 2, GuiMoneyNumbers.ScreenBottom[hardware]),GuiMoneyNumbers.ScreenWidth[hardware], GuiMoneyNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
                DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.ScreenLeft[hardware] - GuiMoneyNumbers.ScreenWidth[hardware] * 3, GuiMoneyNumbers.ScreenBottom[hardware]),GuiMoneyNumbers.ScreenWidth[hardware], GuiMoneyNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
                DefaultSpriteUI.AddElement(new Vector2(GuiMoneyNumbers.ScreenLeft[hardware] - GuiMoneyNumbers.ScreenWidth[hardware] * 4, GuiMoneyNumbers.ScreenBottom[hardware]),GuiMoneyNumbers.ScreenWidth[hardware], GuiMoneyNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
            }
        };
*/

public class GuiShopHealth
{
    private GuiManager Gui;
    private SpriteUI DefaultSpriteUI;
    private Sprite SpriteB;
    private Sprite SpriteC;
    private GuiNumbers Level;
    private GuiNumbers Money;

    public void Start(GuiManager manager)
    {
        Gui = manager;
        DefaultSpriteUI = manager.DefaultSpriteUI;

        SpriteB = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.BScreenLeft(0), GuiShopComboButtons.BScreenBottom(0)), GuiShopComboButtons.BScreenWidth(0), GuiShopComboButtons.BScreenHeight(0), 9, GuiShopComboButtons.BUvLeft, GuiShopComboButtons.BUvBottom, GuiShopComboButtons.BUvWidth, GuiShopComboButtons.BUvHeight);
        SpriteC = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.CScreenLeft(0), GuiShopComboButtons.CScreenBottom(0)), GuiShopComboButtons.CScreenWidth(), GuiShopComboButtons.CScreenHeight(), 9, GuiShopComboButtons.CUvLeft[0], GuiShopComboButtons.CUvBottom[0], GuiShopComboButtons.CUvWidth, GuiShopComboButtons.CUvHeight);

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            Money = new GuiNumbers()
            {
                UvLeft = GuiShopNumbers.UvLeft,
                UvTop = GuiShopNumbers.UvTop,
                UvWidth = GuiShopNumbers.UvWidth,
                UvHeight = GuiShopNumbers.UvHeight,
                Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiHealthNumbers.ScreenLeft + GuiHealthNumbers.ScreenWidth * 3, GuiHealthNumbers.ScreenBottom),GuiHealthNumbers.ScreenWidth, GuiHealthNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiHealthNumbers.ScreenLeft + GuiHealthNumbers.ScreenWidth * 2, GuiHealthNumbers.ScreenBottom),GuiHealthNumbers.ScreenWidth, GuiHealthNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiHealthNumbers.ScreenLeft + GuiHealthNumbers.ScreenWidth , GuiHealthNumbers.ScreenBottom),GuiHealthNumbers.ScreenWidth, GuiHealthNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiHealthNumbers.ScreenLeft, GuiHealthNumbers.ScreenBottom),GuiHealthNumbers.ScreenWidth, GuiHealthNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
               // DefaultSpriteUI.AddElement(new Vector2(GuiHealthNumbers.ScreenLeft[hardware] + GuiHealthNumbers.ScreenWidth[hardware] * 4, GuiHealthNumbers.ScreenBottom[hardware]),GuiHealthNumbers.ScreenWidth[hardware], GuiHealthNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
            }
            };
        }

        Level = new GuiNumbers()
        {
            UvLeft = GuiShopNumbers.UvLeft,
            UvTop = GuiShopNumbers.UvTop,
            UvWidth = GuiShopNumbers.UvWidth,
            UvHeight = GuiShopNumbers.UvHeight,
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiHealthLevel.ScreenLeft, GuiHealthLevel.ScreenBottom),GuiHealthLevel.ScreenWidth, GuiHealthLevel.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
            }
        };

        //Hide();
		
		// of	unlockAchievement
		if(Game.Instance.HealthLevel == E_HealthLevel.Max) {
			Achievements.UnlockAchievement(22);
		}
	}

    public void Reset()
    {
        Hide();
    }

    public void Show()
    {
        DefaultSpriteUI.ShowSprite(SpriteB);
        DefaultSpriteUI.ShowSprite(SpriteC);

        Gui.ShowNumbers(Level, (int)Game.Instance.HealthLevel, 5);


        if (Game.Instance.HealthLevelMaxed() == false && Money != null)
        {
            Gui.ShowNumbers(Money, (int)Game.PriceHealth[(int)Game.Instance.HealthLevel], 9999);

            if (Game.Instance.CouldBuyHealthLevel())
                SetColor(Money.Sprites, Color.white);
            else
                SetColor(Money.Sprites, GuiManager.DisabledColor);
        }
    }

    public void Hide()
    {
        DefaultSpriteUI.HideSprite(SpriteB);
        DefaultSpriteUI.HideSprite(SpriteC);

        if (Money != null)
        {
            for (int i = 0; i < Money.Sprites.Length; i++)
                DefaultSpriteUI.HideSprite(Money.Sprites[i]);
        }

        DefaultSpriteUI.HideSprite(Level.Sprites[0]);
    }

    void SetColor(Sprite[] Sprites, Color color)
    {

        for (int i = 0; i < Sprites.Length; i++)
            Sprites[i].SetColor(color);
            
    }
}

