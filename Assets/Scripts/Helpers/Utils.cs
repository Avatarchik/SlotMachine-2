﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Util;

public class Utils : MonoBehaviour {
  
	public static DateTime time1970 = new DateTime(1970, 1, 1);
	
	public static long UTCNowMiliseconds() {
		return (long)(DateTime.UtcNow - Utils.time1970).TotalMilliseconds;
	}
	
  public static bool IsTablet() {
		#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return SystemInfo.deviceModel.IndexOf("ipad", StringComparison.OrdinalIgnoreCase) > -1;
			}
		#endif
		#if UNITY_ANDROID
			if (Application.platform == RuntimePlatform.Android) {
				DisplayMetricsAndroid.Init();
				return DisplayMetricsAndroid.IsTablet();
			}
		#endif
		return true;
	}
  
  public static bool IsUHD() {
    if (Screen.width >= 2048) {
      return true;
    } else {
      return false;
    }
  }
  
  public static bool IsSD() {
    if (Screen.width <= 480) {
      return true;
    } else {
      return false;
    }
  }

  public static ByteArray ToByteArray(string stringInput) {
    return new ByteArray(Encoding.UTF8.GetBytes(stringInput));
  }
  
  public static string FromByteArray(ByteArray byteArray) {
    return Encoding.UTF8.GetString(byteArray.Bytes);
  }
  
  public static string GetSortCharacters(int num) {
		if (num < 10) {
			return "00" + num;
		} else if (num >= 10 && num < 100){
			return "0" + num;
		} else {
			return num + "";
		}
	}
	
	public static DateTime CurrentTime() {
	  return DateTime.UtcNow;
	}
	
	public static string ChatEscape(string text) {
	  return Uri.EscapeDataString(text);
	}
	
	public static string ChatUnescape(string text) {
	  return Uri.UnescapeDataString(text);
	}
	
	public static bool IsOdd(int value) {
  	return value % 2 != 0;
  }
	
	public static void SetActive(GameObject go, bool state) {
	  NGUITools.SetActive(go, state);
	}
	
	private static string chars = "abcdefghijklmnopqrstuvwxyz_";
	private static System.Random random = new System.Random();
	
	public static string RandomUsername(int length) {
	  string username = "";
	  for (int i = 0; i < length; i++) {
      username += chars[random.Next(chars.Length)];
    }
    return username;
	}
	
	public static string ArrIntToString(int[] arr) {
	  string log = "[";
	  for (int i = 0; i < arr.Length; i++) {
	    log += arr[i] + ((i == arr.Length - 1) ? "" : ",");
	  }
	  log += "]";
	  return log;
	}
	
	public static void StringToIntArray(string input, int[] fillArr) {
		string[] split = input.Trim().Split(',');
		for (int i = 0; i < fillArr.Length; i++) {
			if (split.Length - 1 >= i) {
				fillArr[i] = int.Parse(split[i].Trim());
			}
		}
		split = null;
	}
	
	public static string CurrencyToStringShort(int numb) {
		// billion
		if (numb >= 1000000000) {
			return Math.Round(numb / 1000000000f, 3) + "B";
		}
		if (numb >= 1000000) {
			return Math.Round(numb / 1000000f, 3) + "M";
		}
		if (numb >= 1000) {
			return Math.Round(numb / 1000f, 3) + "K";
		}
		return numb.ToString();
	}
	
	public static string CurrencyToStringShort(float numb) {
		// billion
		if (numb >= 1000000000) {
			return Math.Round(numb / 1000000000f, 3) + "B";
		}
		if (numb >= 1000000) {
			return Math.Round(numb / 1000000f, 3) + "M";
		}
		if (numb >= 1000) {
			return Math.Round(numb / 1000f, 3) + "K";
		}
		return numb.ToString();
	}
	
	// Input seconds - format 1:20:30
	public static string GetTimeString(int timer) {
		if (timer <= 0) {
			return "00:00:00";
		}
		int tempTime = 0;
		string timeString = string.Empty;
		if (timer > 3600) {
			tempTime = (int)Mathf.Ceil(timer / 3600);
			timeString += tempTime + ":";
			timer -= tempTime * 3600;
		} else {
			timeString += "00:";
		}
		
		if (timer >= 60) {
			tempTime = (int)Mathf.Ceil(timer / 60);
			timeString += tempTime + ":";
			timer -= tempTime * 60;
		} else {
			timeString += "00:";
		}
		
		if (timer < 10) {
			timeString += "0" + timer;
		} else {
			timeString += timer;
		}
			
		return timeString;
	}
	
	// timer = seconds
	public static string GetTimePassed(int timer) {
		if (timer <= 0) {
			return string.Empty;
		}
		int tempTime = 0;
		string timeString = string.Empty;
		
		if (timer >= 86400) {
			tempTime = (int)Mathf.Round(timer / 86400);
			timeString += tempTime + Localization.Get("Time_Day_Short") + " ";
			timer -= tempTime * 86400;
		}
		 
		if (timer >= 3600) {
			tempTime = (int)Mathf.Round(timer / 3600);
			timeString += tempTime + Localization.Get("Time_Hour_Short") + " ";
			timer -= tempTime * 3600;
		}
		
		if (timer >= 60) {
			tempTime = (int)Mathf.Round(timer / 60);
			timeString += tempTime + Localization.Get("Time_Minute_Short") + " ";
			timer -= tempTime * 60;
		}
		if (timeString != string.Empty && timeString != null) {
			timeString = timeString + " " + Localization.Get("Time_Ago");
		}
		
		return timeString;
	}
	
	public static void Log(object message) {
		Debug.Log(message);
	}
}

static class MyExtensions
{
	// shuffle a list
	public static void Shuffle<T>(this IList<T> list)  {
    int n = list.Count;  
    while (n > 1) {  
      n--;  
      int k = Global.systemRandom.Next(n + 1);  
      T value = list[k];  
      list[k] = list[n];  
      list[n] = value;  
    }  
  }
}