using UnityEngine;
using System.Collections;

public class OFTest : MonoBehaviour {

	// 游戏名
	public string ofname;
	// 游戏id
	public string ofid;
	// 游戏key
	public string ofkey;
	// 游戏secret
	public string ofsecret;
	// 渠道标识
	public string appstore;

	private OpenFeintFacade mOpenFeint;
	// Use this for initialization
	void Start () {
		if(mOpenFeint == null){
			// 创建脚本接口对象
			mOpenFeint = new OpenFeintFacade();
			// 设置渠道标识（一定要在初始化之前设置，如果不需要渠道标识，可以不用设置，其默认值为"default"）
			mOpenFeint.setAppstore(appstore);
			// 初始化九城游戏中心
			mOpenFeint.Init(ofname, ofkey, ofsecret, ofid);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		if(GUI.Button(new Rect(100, 20, 200, 100), "open")){
			// 打开九城游戏中心
			mOpenFeint.OpenDashboard();
		}
		
		if(GUI.Button(new Rect(100, 120, 200, 100), "submit score")){
			// 上传分数
			mOpenFeint.SubmitScore("916953092", 5000);
		}
		
		if(GUI.Button(new Rect(100, 220, 200, 100), "unlock achievement")){
			// 解锁成就
			mOpenFeint.UnlockAchievement(1073557512);
		}
		
		if(GUI.Button(new Rect(100, 320, 200, 100), "quit")){
			Application.Quit();
		}
	}
}
