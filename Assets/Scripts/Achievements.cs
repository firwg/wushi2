using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour {
	
	private static int[] achievements = new int[]{
		1073560142, 1073560152, 1073560162, 1073560172, 1073560182,
		1073560192, 1073560202, 1073560212, 1073560222, 1073560232,
		1073560242, 1073560252, 1073560262, 1073560272, 1073560282,
		1073560292, 1073560302, 1073560312, 1073560322, 1073560332,
		1073560342, 1073560352, 1073560362, 1073560372
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void UnlockAchievement(int index) {
		MainMenu.openFeint.UnlockAchievement(achievements[index]);
	}
}
