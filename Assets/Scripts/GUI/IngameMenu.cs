using UnityEngine;
using System.Collections;
using System;

public class IngameMenu
{
    enum E_State
    {
        IngameMenu,
        Controls
    }

    enum E_PressedButton
    {
        Resume,
        Achievements,
        Quit,
    }

    private GuiManager GuiManager;
    private SpriteUI DefaultSpriteUI;
    private bool IsOn;
    private E_State State;
    private bool ButtonSelected = false;
    private bool JoystickSelected = false;

	public void Start (GuiManager manager)
	{
        GuiManager = manager;
        DefaultSpriteUI = GuiManager.DefaultSpriteUI;

        GuiInGameMenu.SpriteControlsBackground = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.CBScreenLeft, GuiInGameMenu.CBScreenBottom), GuiInGameMenu.CBScreenWidth, GuiInGameMenu.CBScreenHeight, 9, GuiInGameMenu.CBUvLeft, GuiInGameMenu.CBUvTop, GuiInGameMenu.CBUvWidth, GuiInGameMenu.CBUvHeight);
        GuiInGameMenu.SpriteResume = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.RScreenLeft, GuiInGameMenu.RScreenBottom), GuiInGameMenu.RScreenWidth, GuiInGameMenu.RScreenHeight, 9, GuiInGameMenu.RUvLeft, GuiInGameMenu.RUvTop, GuiInGameMenu.RUvWidth, GuiInGameMenu.RUvHeight);
        GuiInGameMenu.SpriteControls = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.CScreenLeft, GuiInGameMenu.CScreenBottom), GuiInGameMenu.CScreenWidth, GuiInGameMenu.CScreenHeight, 9, GuiInGameMenu.CUvLeft, GuiInGameMenu.CUvTop, GuiInGameMenu.CUvWidth, GuiInGameMenu.CUvHeight);
        GuiInGameMenu.SpriteControlsOk = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.COKScreenLeft, GuiInGameMenu.COKScreenBottom), GuiInGameMenu.COKScreenWidth, GuiInGameMenu.COKScreenHeight, 9, GuiInGameMenu.COKUvLeft, GuiInGameMenu.COKUvTop, GuiInGameMenu.COKUvWidth, GuiInGameMenu.COKUvHeight);
        GuiInGameMenu.SpriteControlsCancel = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.CCScreenLeft, GuiInGameMenu.CCScreenBottom), GuiInGameMenu.CCScreenWidth, GuiInGameMenu.CCScreenHeight, 9, GuiInGameMenu.CCUvLeft, GuiInGameMenu.CCUvTop, GuiInGameMenu.CCUvWidth, GuiInGameMenu.CCUvHeight);
        GuiInGameMenu.SpriteControlsReset = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.CRScreenLeft, GuiInGameMenu.CRScreenBottom), GuiInGameMenu.CRScreenWidth, GuiInGameMenu.CRScreenHeight, 9, GuiInGameMenu.CRUvLeft, GuiInGameMenu.CRUvTop, GuiInGameMenu.CRUvWidth, GuiInGameMenu.CRUvHeight);

        GuiInGameMenu.SpriteQuit = DefaultSpriteUI.AddElement(new Vector2(GuiInGameMenu.QScreenLeft, GuiInGameMenu.QScreenBottom), GuiInGameMenu.QScreenWidth, GuiInGameMenu.QScreenHeight, 9, GuiInGameMenu.QUvLeft, GuiInGameMenu.QUvTop, GuiInGameMenu.QUvWidth, GuiInGameMenu.QUvHeight);
        Hide();
	}

    public void Reset()
    {

    }

    public void Show()
	{
        if (IsOn)
            return;

        State = E_State.IngameMenu;

        IsOn = true;

		Game.Instance.GameState = E_GameState.IngameMenu;
        Player.Instance.StopMove(true);

        GuiManager.StartCoroutine(_Show());
	}

    IEnumerator _Show()
    {
        GuiManager.FadeOut(0.2f, 0.7f);
        AudioListener.volume = 0.3f;

        yield return new WaitForSeconds(0.3f);

        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteControls);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteResume);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteQuit);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsOk);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsCancel);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsReset);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsBackground);

        /*

        UnityEngine.Object[] textures = Resources.FindObjectsOfTypeAll(typeof(Texture));
        UnityEngine.Object[] audioclips = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
        UnityEngine.Object[] animations = Resources.FindObjectsOfTypeAll(typeof(AnimationClip));
        UnityEngine.Object[] meshes = Resources.FindObjectsOfTypeAll(typeof(Mesh));

        Debug.Log("All " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
        Debug.Log("Textures " + textures.Length);
        Debug.Log("AudioClips " + audioclips.Length);
        Debug.Log("Meshes " + meshes.Length);
        Debug.Log("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
        Debug.Log("Animation " + animations.Length);
        Debug.Log("GameObjects " + Resources.FindObjectsOfTypeAll(typeof(GameObject)).Length);
        Debug.Log("Components " + Resources.FindObjectsOfTypeAll(typeof(Component)).Length);

        for (int i = 0; i < textures.Length; i++)
        {
            Texture t = textures[i] as Texture;
            Debug.Log("Texture " + t.name + " - " + t.width +"x" + t.height);
        }

        for (int i = 0; i < audioclips.Length; i++)
        {
            AudioClip t = audioclips[i] as AudioClip;
            Debug.Log("audio " + t.name + " - " + t.length);
        }

        for (int i = 0; i < animations.Length; i++)
        {
            AnimationClip t = animations[i] as AnimationClip;
            Debug.Log("animation " + t.name + " - " + t.length);
        }

        for (int i = 0; i < meshes.Length; i++)
        {
            Mesh t = meshes[i] as Mesh;
            Debug.Log("mesh " + t.name);
        }
        */
        Time.timeScale = 0;
    }

    void ShowControls()
    {
        State = E_State.Controls;

        GuiManager.Instance.SetFadeOut(0);

        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControls);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteResume);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteQuit);

        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteControlsOk);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteControlsCancel);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteControlsReset);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteControlsBackground);

        GuiManager.Instance.ShowControlsForCustomize(true);

        ButtonSelected = false;
        JoystickSelected = false;
    }

    void HideControls(bool save)
    {
        State = E_State.IngameMenu;

        GuiManager.SetFadeOut(0.7f);

        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsOk);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsCancel);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsReset);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsBackground);

        GuiManager.Instance.ShowControlsForCustomize(false);

        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteControls);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteResume);
        DefaultSpriteUI.ShowSprite(GuiInGameMenu.SpriteQuit);

        if (save)
        {
            Game.Instance.SaveControls();
            Player.Instance.UpdateControlsPosition();
        }
        else
        {
            Game.Instance.LoadControls();
        }
    }

    void ResetControls()
    {
        Game.Instance.ControlsJoystickOffset = Game.Instance.ControlsButtonOffset = Vector2.zero;
        GuiManager.Instance.UpdateControlsForCustomize();
    }

	public void Hide(bool resume = true)
	{
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControls);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteResume);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteQuit);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsCancel);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsReset);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsOk);
        DefaultSpriteUI.HideSprite(GuiInGameMenu.SpriteControlsBackground);

        Time.timeScale = 1;
		Game.Instance.GameState = E_GameState.Game;

        if(resume)
            GuiManager.FadeIn();

        IsOn = false;
	}


    public void Update()
    {
        if (IsOn == false)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (State == E_State.IngameMenu)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > GuiInGameMenu.CScreenLeft && touch.position.x < GuiInGameMenu.CScreenLeft + GuiInGameMenu.CScreenWidth &&
                   touch.position.y > GuiInGameMenu.CScreenBottom && touch.position.y < GuiInGameMenu.CScreenBottom + GuiInGameMenu.CScreenHeight)
                {// CONTROLS
                    GuiManager.Instance.PlayButtonSound();
                    ShowControls();
                }
                else if (touch.position.x > GuiInGameMenu.RScreenLeft && touch.position.x < GuiInGameMenu.RScreenLeft + GuiInGameMenu.RScreenWidth &&
                    touch.position.y > GuiInGameMenu.RScreenBottom && touch.position.y < GuiInGameMenu.RScreenBottom + GuiInGameMenu.RScreenHeight)
                {
                    GuiManager.Instance.PlayButtonSound();
                    Resume();
                }
                else if (touch.position.x > GuiInGameMenu.QScreenLeft && touch.position.x < GuiInGameMenu.QScreenLeft + GuiInGameMenu.QScreenWidth &&
                   touch.position.y > GuiInGameMenu.QScreenBottom && touch.position.y < GuiInGameMenu.QScreenBottom + GuiInGameMenu.QScreenHeight)
                {
                    GuiManager.Instance.PlayButtonSound();
                    Quit();
                }
            }
        }
        else
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > GuiInGameMenu.COKScreenLeft && touch.position.x < GuiInGameMenu.COKScreenLeft + GuiInGameMenu.COKScreenWidth &&
                    touch.position.y > GuiInGameMenu.COKScreenBottom - GuiInGameMenu.COKScreenHeight && touch.position.y < GuiInGameMenu.COKScreenBottom + GuiInGameMenu.COKScreenHeight * 2)
                {// SAVE
                    GuiManager.Instance.PlayButtonSound();
                    HideControls(true);
                }
                else if (touch.position.x > GuiInGameMenu.CCScreenLeft && touch.position.x < GuiInGameMenu.CCScreenLeft + GuiInGameMenu.CCScreenWidth &&
                   touch.position.y > GuiInGameMenu.CCScreenBottom - GuiInGameMenu.CCScreenHeight && touch.position.y < GuiInGameMenu.CCScreenBottom + GuiInGameMenu.CCScreenHeight * 2)
                {//CANCEL
                    GuiManager.Instance.PlayButtonSound();
                    HideControls(false);
                }
                else if (touch.position.x > GuiInGameMenu.CRScreenLeft && touch.position.x < GuiInGameMenu.CRScreenLeft + GuiInGameMenu.CRScreenWidth &&
               touch.position.y > GuiInGameMenu.CRScreenBottom - GuiInGameMenu.CRScreenHeight && touch.position.y < GuiInGameMenu.CRScreenBottom + GuiInGameMenu.CRScreenHeight * 2)
                {//RESET
                    GuiManager.Instance.PlayButtonSound();
                    ResetControls();
                }
                else if ((GuiButtonY.Instance.Center + Game.Instance.ControlsButtonOffset - touch.position).magnitude < 200)
                {
                    ButtonSelected = true;
                    JoystickSelected = false;
                }
                else if ((GuiJoystick.Instance.Center + Game.Instance.ControlsButtonOffset - touch.position).magnitude < 100)
                {
                    ButtonSelected = false;
                    JoystickSelected = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (ButtonSelected)
                    Game.Instance.ControlsButtonOffset += touch.deltaPosition;
                else if (JoystickSelected)
                    Game.Instance.ControlsJoystickOffset += touch.deltaPosition;

                GuiManager.Instance.UpdateControlsForCustomize();
            }
            else if (touch.phase == TouchPhase.Canceled || touch.phase ==  TouchPhase.Ended)
            {
                ButtonSelected = false;
                JoystickSelected = false;
            }
        }
    }


	public virtual void OnTouchUp(Sprite sprite, Vector2 pos, int touch)
	{
	/*	if (sprite == OptionsButton && PressedButton == OptionsButton)
		{
			audio.PlayOneShot(ButtonSounds[UnityEngine.Random.Range(0, ButtonSounds.Length)]);
			if (PersistentData.Instance.GameState != PersistentData.E_GameState.E_INGAME_MENU)
				ShowIngameMenu();
			else
				HideIngameMenu();

			PressedButton = null;

			return;
		}

		if (sprite == ComboButton && PressedButton == ComboButton)
		{
            if (PersistentData.Instance.GameState != PersistentData.E_GameState.E_INGAME_MENU)
            {
                audio.PlayOneShot(ButtonSounds[UnityEngine.Random.Range(0, ButtonSounds.Length)]);
                ShowComboUI(true);
            }

			PressedButton = null;
			return;
		}


		if (sprite == IngameMenu.Resume)
		{
			StartCoroutine(Resume());
		}

		if (sprite == IngameMenu.Quit)
		{
			StartCoroutine(Quit());
		}

		*/
	}
       
    void Resume()
	{
        AudioListener.volume = 1;
        Player.Instance.StopMove(false);
        GuiManager.HideIngameMenu();
	}

    void Quit()
    {
        AudioListener.volume = 1;
        GuiManager.StartCoroutine(DoQuit());
    }


	IEnumerator DoQuit()
	{
        Time.timeScale = 1;

        Hide(false);

        Mission.Instance.StartCoroutine(Mission.Instance.FadeOutMusic(1));
        GuiManager.FadeOut();

        yield return new WaitForSeconds(1.1f);

        Mission.Instance = null;
        Game.Instance.LoadMainMenu();
	}
}

