using UnityEngine;
using System;
using System.Collections;

public class OpenFeintFacade : IDisposable {
	private bool calledInit = false;
	
	private AndroidJavaClass cls_OpenFeintFacade = new AndroidJavaClass("com.unity3d.Plugin.OpenFeintFacade");
	private AndroidJavaClass cls_OpenFeint = new AndroidJavaClass("com.openfeint.api.OpenFeint");
	private AndroidJavaClass cls_Dashboard = new AndroidJavaClass("com.openfeint.api.ui.Dashboard");
	private AndroidJavaClass cls_ofInternal = new AndroidJavaClass("com.openfeint.internal.OpenFeintInternal");
	
	// 设置渠道标识（例如中国移动应用商城：mm，联通wo商店：wostore，电信天翼空间：estore，如果不设置渠道标识，默认标识为：default）
	public void setAppstore(string appstore)
	{
		cls_ofInternal.SetStatic("initAppstore", appstore);
	}
	
	// 初始化九城游戏中心（参数依次为：游戏名，游戏key， 游戏secret， 游戏id）
	public void Init(string name, string key, string secret, string id) {
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				using (AndroidJavaObject obj_OpenFeintSettings = new AndroidJavaObject("com.openfeint.api.OpenFeintSettings", name, key, secret, id)) {
					cls_OpenFeintFacade.CallStatic("Init", obj_Activity, obj_OpenFeintSettings);
				}
			}
		}
		
		calledInit = true;
	}
	
	// 检查九城游戏中心是否已初始化
	public bool isInitialized() {
		return calledInit;
	}
	
	// 检查用户是否已登录九城游戏中心
	public bool isUserLoggedIn() {
		bool isUserLoggedIn = cls_OpenFeint.CallStatic<bool>("isUserLoggedIn");
		
		if (AndroidJNI.ExceptionOccurred() != IntPtr.Zero) {	// will return NullPointerException if not logged in
			AndroidJNI.ExceptionClear();
			return false;
		}
		
		return isUserLoggedIn;
	}
	
	// 打开九城游戏中心
	public void OpenDashboard() {
		cls_Dashboard.CallStatic("open");
	}
	
	// 打开九城游戏中心
	public void LaunchDashboard() {
		cls_OpenFeintFacade.CallStatic("LaunchDashboard");
	}
	
	// 打开排行榜界面
	public void LaunchLeaderboards() {
        cls_OpenFeintFacade.CallStatic("LaunchLeaderboards");
	}
	
	// 打开成就界面
    public void LaunchAchievements() {
        cls_OpenFeintFacade.CallStatic("LaunchAchievements");
    }
	
	// 提交高分（参数依次为：高分，分数显示内容，排行榜id，是否要去除通知）
	public void SubmitHighScore(long highscore, string displayText, string leaderBoardId, bool silent) {
		cls_OpenFeintFacade.CallStatic("SubmitHighScore", highscore,displayText,leaderBoardId,silent);
	}
	
	// 显示指定的排行榜
	public void LaunchDashboardWithLeaderboardID(string leaderboard) {
		cls_Dashboard.CallStatic("openLeaderboard", leaderboard);
	}
	
	// 解锁成就
	public void UnlockAchievement(int achievementID) {
		cls_OpenFeintFacade.CallStatic("UnlockAchievement", achievementID);
	}
	
	// 解锁成就（百分比）
	public void UpdateAchievement(int achievementID, float percentage) {
		cls_OpenFeintFacade.CallStatic("UpdateAchievement", achievementID, percentage);
	}
	
	// 向玩家发送一个带图标的社交通知
	public void SendSocialNotification(string text, string imageName) {
		cls_OpenFeintFacade.CallStatic("SendSocialNotification", text, imageName);
	}
	
	// 向玩家发送一个带图标的游戏通知
	public void InGameNotification(string text, string imageName) {
		cls_OpenFeintFacade.CallStatic("InGameNotification", text, imageName);
	}
	
	// 提交分数
	public void SubmitScore(string leaderboard, int score) {
		cls_OpenFeintFacade.CallStatic("SubmitScore", leaderboard, score);
	}
	
	// 解锁成就
	public void SubmitAchievement(int achievementID) {
		cls_OpenFeintFacade.CallStatic("SubmitAchievement", achievementID);
	}
	
	public void Dispose() {
		cls_OpenFeintFacade.Dispose();
		cls_OpenFeint.Dispose();
		cls_Dashboard.Dispose();
	}
}
