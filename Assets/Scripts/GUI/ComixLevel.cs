using UnityEngine;
using System.Collections;

public class ComixLevel : MonoBehaviour 
{
    public string NextLevel;
    public GameObject Background;
    public GameObject BackgroundButtons;
    public GameObject ButtonGame;
    public GameObject ButtonNext;
    public GameObject ButtonPrev;
    public GameObject ButtonRewind;
    public GameObject Counter;
    public GameObject Counter8;
    public GameObject Buy;
    public int MaxPages = 8;

	int CurrentPage = 0;

	public AudioClip[] NextPage = new AudioClip[1];
    public AudioClip ClickSound { get { return NextPage[Random.Range(0, NextPage.Length)]; } }

	const float MaxMusicVolume = 0.4f;
    private AudioSource Music;
	private Material MatPrev;
    private Material MatNext;
    private Material MatRew;

    private const int BuyID = 24;
    private const int GameID = 23;
    private const int NextID = 22;
    private const int PrevID = 21;
    private const int RewID = 20;

    public string[] BuyLink;
    string[] AnimForward = { "ComixPag1to2", "ComixPag2to3", "ComixPag3to4", "ComixPag4to5", "ComixPag5to6", "ComixPag6to7", "ComixPag7to8" };
    string[] AnimBackward = { "ComixPag2to1", "ComixPag3to2", "ComixPag4to3", "ComixPag5to4", "ComixPag6to5", "ComixPag7to6", "ComixPag8to7" };

    string[] CountForward = { "CountPag1to2", "CountPag2to3", "CountPag3to4", "CountPag4to5", "CountPag5to6", "CountPag6to7", "CountPag7to8" };
    string[] CountBackward = { "CountPag2to1", "CountPag3to2", "CountPag4to3", "CountPag5to4", "CountPag6to5", "CountPag7to6", "CountPag8to7" };  

	// Use this for initialization
	void Awake()
	{
		//Animation anims = animation;
	}
	void Start () 
    {
        if(Game.Instance && Game.Instance.GameType == E_GameType.SinglePlayer)
            Game.Instance.Save_Save();

        /*if (iPhoneSettings.generation == iPhoneGeneration.iPad1Gen)
        {
            Camera.mainCamera.orthographicSize = 1.28f;
            ButtonGame.transform.parent.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            ButtonGame.transform.parent.transform.position = new Vector3(0, -0.4f, 0);
        }*/

		Music = GetComponent<AudioSource>(); 
		StartCoroutine(FadeInMusic(4));

        MatPrev = ButtonPrev.GetComponentInChildren<SkinnedMeshRenderer>().material;
        MatNext = ButtonNext.GetComponentInChildren<SkinnedMeshRenderer>().material;
        MatRew = ButtonRewind.GetComponentInChildren<SkinnedMeshRenderer>().material;

        MatPrev.SetColor("_Color",  new Color(1,1,1,0.2f));
        MatRew.SetColor("_Color",  new Color(1,1,1,0.2f));

      //  Counter.animation.Play("CounterIdle");

        Background.GetComponent<Animation>().Play();

        BackgroundButtons.GetComponent<Animation>().Play();
        ButtonGame.GetComponent<Animation>().Play();
        ButtonNext.GetComponent<Animation>().Play();
        ButtonPrev.GetComponent<Animation>().Play();
        ButtonRewind.GetComponent<Animation>().Play();
        Counter.GetComponent<Animation>().Play();
        Counter8.GetComponent<Animation>().Play();
		// of	unlockAchievement
		if(Game.Instance.CurrentLevel == "comics08") {
			if(Game.Instance.GameDifficulty == E_GameDifficulty.Easy) {
				PlayerPrefs.SetInt("Easy", 1);
				Achievements.UnlockAchievement(9);
			} else if(Game.Instance.GameDifficulty == E_GameDifficulty.Hard) {
				PlayerPrefs.SetInt("Hard", 1);
				Achievements.UnlockAchievement(10);
			}else {
				PlayerPrefs.SetInt("Normal", 1);
			}
			if( PlayerPrefs.GetInt("Easy", 0) + PlayerPrefs.GetInt("Normal", 0) + PlayerPrefs.GetInt("Hard", 0) == 3) {
				Achievements.UnlockAchievement(23);
			}
		}
		if(Game.Instance.GameDifficulty == E_GameDifficulty.Normal && Game.Instance.GameType == E_GameType.SinglePlayer) {
			switch(Application.loadedLevelName){
			case "Comics02" :
				Achievements.UnlockAchievement(15);
				break;
			case "Comics03":
				Achievements.UnlockAchievement(16);
				break;
			case "Comics04":
				Achievements.UnlockAchievement(17);
				break;
			case "Comics05":
				Achievements.UnlockAchievement(18);
				break;
			case "Comics06":
				Achievements.UnlockAchievement(19);
				break;
			case "Comics07":
				Achievements.UnlockAchievement(20);
				break;
			case "Comics08":
				Achievements.UnlockAchievement(21);
				break;
			}
		}
	}

    void Update()
    {
		if(BasicDialog.state == BasicDialog.State.Show) {
			return;
		}
		
        if (Input.touchCount == 0)
            return;

        Touch t = Input.touches[0];

        if (t.phase != TouchPhase.Ended)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(t.position), out hit, Mathf.Infinity, Physics.AllLayers) == false)
            return;

        int id = hit.collider.gameObject.layer;

        if(CurrentPage == 0 && (id == PrevID  ||  id == RewID))
            return;

        if (CurrentPage == MaxPages - 1 && id == NextID)
            return;


        if (id == RewID)
        {
            Music.PlayOneShot(ClickSound);
            CurrentPage = 0;
            ButtonRewind.GetComponent<Animation>().Play("ComixButtonPress");
            Background.GetComponent<Animation>().CrossFade("ComixRewind", 0.1f);
            Counter.GetComponent<Animation>().Play("CounterIdle");
            
            MatPrev.SetColor("_Color", new Color(1, 1, 1, 0.2f));
            MatRew.SetColor("_Color", new Color(1, 1, 1, 0.2f));
            MatNext.SetColor("_Color", new Color(1, 1, 1, 1));
            return;
        }

        if (id == NextID)
        {
            Music.PlayOneShot(ClickSound);
            ButtonNext.GetComponent<Animation>().Play("ComixButtonPress");
            Background.GetComponent<Animation>().CrossFade(AnimForward[CurrentPage], 0.1f);
            Counter.GetComponent<Animation>().CrossFade(CountForward[CurrentPage], 0.1f);
            CurrentPage++;

            if (CurrentPage == 1)
            {
                MatPrev.SetColor("_Color", new Color(1, 1, 1, 1));
                MatRew.SetColor("_Color", new Color(1, 1, 1, 1));
            }

            if (CurrentPage == MaxPages -1)
            {
                MatNext.SetColor("_Color", new Color(1, 1, 1, 0.2f));
            }
            return;
        }

        if (id == PrevID)
        {
            Music.PlayOneShot(ClickSound);
            CurrentPage--;
            ButtonPrev.GetComponent<Animation>().Play("ComixButtonPress");
            Background.GetComponent<Animation>().CrossFade(AnimBackward[CurrentPage], 0.1f);
            Counter.GetComponent<Animation>().CrossFade(CountBackward[CurrentPage], 0.1f);

            if (CurrentPage == 0)
            {
                MatPrev.SetColor("_Color", new Color(1, 1, 1, 0.2f));
                MatRew.SetColor("_Color", new Color(1, 1, 1, 0.2f));
            }

            MatNext.SetColor("_Color", new Color(1, 1, 1, 1));

            return;
        }

        if (id == GameID)
        {
            Music.PlayOneShot(ClickSound);
            ButtonGame.GetComponent<Animation>().Play("ComixButtonPress");
            StartCoroutine(LoadNextLevel());
            return;
        }

        if (id == BuyID)
        {
            Music.PlayOneShot(ClickSound);
            Application.OpenURL(BuyLink[CurrentPage]);
        }
    }
	

	IEnumerator LoadNextLevel()
	{
        FadeOutMusic(4);

        yield return new WaitForSeconds(0.2f);

        ButtonGame.GetComponent<Animation>().Play("ComixButtonOut");
        ButtonPrev.GetComponent<Animation>().Play("ComixButtonOut");
        ButtonNext.GetComponent<Animation>().Play("ComixButtonOut");
        ButtonRewind.GetComponent<Animation>().Play("ComixButtonOut");
        BackgroundButtons.GetComponent<Animation>().Play("ButtonLineOut");
        Counter.SetActiveRecursively(false);
        Counter8.SetActiveRecursively(false);

        if(Buy != null)
            Buy.SetActiveRecursively(false);
        yield return new WaitForSeconds(0.1f);

        Background.GetComponent<Animation>().Play("ComixPag8toLoad");

        yield return new WaitForSeconds(0.3f);

//        Debug.Log("Comix load next level  " + NextLevel + " " + Game.Instance.GameType);
        if (Game.Instance.GameType ==  E_GameType.Tutorial)
            Game.Instance.LoadMainMenu();
        else if(Game.Instance.GameType == E_GameType.FirstTimeTutorial)
            Game.Instance.StartNewGame(Game.Instance.GameDifficulty);
        else 
            Game.Instance.LoadNextLevel(NextLevel, 0);
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
	}

}
