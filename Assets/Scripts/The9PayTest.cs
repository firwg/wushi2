using UnityEngine;
using System.Collections;

public class The9PayTest : MonoBehaviour {
	
	private The9PaymentFacade tpf;
	private bool isShow = false;

	// Use this for initialization
	void Start () {
		
		if(tpf == null) {
			tpf = new The9PaymentFacade();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// GUI
	void OnGUI() {
		
		if(!isShow){
			if(!tpf.getPayFlag("level02")){
				tpf.showPayDialog("level02", "关卡解锁", "只需15元，您就可以将更加精彩的后续关卡全部解锁，更炫的技能，更丰富的场景等着您！", "level01", 15.0f, 1);
				Game.Instance.LoadMainMenu();
				//PlayerPrefs.SetInt("level02_purchased", 1);
				isShow = true;
			}
		}
//		if(GUI.Button(new Rect(100, 20, 200, 100), "Pay")){
//			if(!tpf.getPayFlag("level1")) {
//				tpf.showPayDialog("level1", "haha", "description", "order_id", 1.0f, 2);
//			}
//		}
//		
//		if(GUI.Button(new Rect(100, 320, 200, 100), "quit")){
//			Application.Quit();
//		}
	}
}
