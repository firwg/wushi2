using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class ComixLevelHD : MonoBehaviour 
{
   /* public GameObject Comix;
	public int NextLevel;
	public int MaxPages;

	int CurrentPage =0;
	private AudioSource Music;

	public AudioClip[] NextPage = new AudioClip[1];


	private string[] AnimForward = { "1to2", "2to3" };
	private string[] AnimBack = { "2to1", "3to2" };

	const float MaxMusicVolume = 0.4f;
	// Use this for initialization
	void Awake()
	{
        Comix.animation["loading"].layer = 1;
	}
	void Start () {

        Game.Instance.SaveSingleplayer();
		Music = audio; 
		StartCoroutine(FadeInMusic(4));
	}
	

	void Update()
	{
   /*     if (iPhoneInput.touchCount == 0)
            return;

        iPhoneTouch touch =  iPhoneInput.GetTouch(0);
		if (iPhoneInput.GetTouch(0).phase == iPhoneTouchPhase.Ended)
		{

			if (touch.position.x > 500)
			{
				if (CurrentPage < MaxPages - 1)
				{
					AudioSource.PlayClipAtPoint(NextPage[Random.RandomRange(0, NextPage.Length)], transform.position);
                    Comix.animation.Play(AnimForward[CurrentPage], AnimationPlayMode.Stop);
					CurrentPage++;
				}
				else if (CurrentPage == MaxPages - 1)
				{
					AudioSource.PlayClipAtPoint(NextPage[Random.RandomRange(0, NextPage.Length)], transform.position);
                    Comix.animation.Play("loading");
					StartCoroutine(FadeOutMusic(2));
					Invoke("LoadNextLevel", 1);
					CurrentPage++;
				}
			}
            else if (CurrentPage <= MaxPages && (touch.position.x < 250))
			{
				if (CurrentPage > 0)
				{
					AudioSource.PlayClipAtPoint(NextPage[Random.RandomRange(0, NextPage.Length)], transform.position);
                    Comix.animation.Play(AnimBack[CurrentPage - 1]);
					CurrentPage--;
				}
			}
		}
	}

	void LoadNextLevel()
	{
        if (Game.Instance.GameState ==  E_GameState.Tutorial)
            Game.Instance.LoadMainMenu();
        else if (Game.Instance.GameState == E_GameState.FirstTimeTutorial)
            Game.Instance.StartNewGame();
        else
            Game.Instance.LoadLevel(NextLevel, 0);
	}

	IEnumerator FadeInMusic(float speed)
	{
		float volume = 0;
		while (volume < MaxMusicVolume)
		{
			volume += speed * Time.deltaTime * MaxMusicVolume;
			if (volume > MaxMusicVolume)
				volume = MaxMusicVolume;

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
				volume = 0;

			Music.volume = volume;

			yield return new WaitForEndOfFrame();
		}
	}*/

}
