using UnityEngine;
using System;
using System.Collections;

public class The9PaymentFacade : IDisposable {
	
	private AndroidJavaClass cls_The9Payment = new AndroidJavaClass("cn.yahoo.asxhl2007.the9payment4unity.The9PaymentFacade");
	
	
	public  void showPayDialog(String payID, String name, String description, String order_id, float unit_price, int quantity){
		cls_The9Payment.CallStatic("showPaydialog", payID, name, description, order_id, unit_price, quantity);
	}
	
	public bool getPayFlag(String payID){
		return cls_The9Payment.CallStatic<bool>("getPayFlag", payID);
	}
	
	public void setPayFlag(String payID, bool flag){
		cls_The9Payment.CallStatic("setPayFlag", payID, flag);
	}
	
	public void Dispose() {
		cls_The9Payment.Dispose();
	}
}
