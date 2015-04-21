﻿using UnityEngine;
using System.Collections;

public class BigWinPanel : MonoBehaviour {

	private Vector3 minScale = new Vector3(0.5f, 0.5f, 1f);
	private int totalScore = 0;
	
	public GameObject bigWinView;
	public GameObject freeSpinView;
	public UILabel winNumbLabel;
	public UILabel freeSpinNumbLabel;
	public Transform effectPoint1;
	public Transform effectPoint2;
	public Transform effectPoint3;

	// Fade in Big Win
	public void FadeIn(int numb) {
		totalScore = numb;
		NGUITools.SetActive(gameObject, true);
		NGUITools.SetActive(bigWinView, true);
		NGUITools.SetActive(freeSpinView, false);
		winNumbLabel.text = numb.ToString("N0");
		TweenAlpha tween = TweenAlpha.Begin(gameObject, 0.5f, 1);
		tween.from = 0;
    EventDelegate.Add(tween.onFinished, EventFinishFadeIn, true);
		transform.localScale = Vector3.one * 3;
		LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutCubic);
		Invoke("FadeOutBigWin", 2f);
		ScreenManager.Instance.CurrentSlotScreen.PauseSpawnSkill();
		ScreenManager.Instance.CurrentSlotScreen.slotMachine.Wait();
	}

	public void FadeOutBigWin() {
		TweenAlpha tween = TweenAlpha.Begin(gameObject, 0.5f, 0);
		if (ScreenManager.Instance.CurrentSlotScreen.slotMachine.gotFreeSpin) {
			FadeInFreeSpin(ScreenManager.Instance.CurrentSlotScreen.slotMachine.freeSpinLeft, false);
		} else {
	    EventDelegate.Add(tween.onFinished, Hide, true);
		}
		ScreenManager.Instance.CurrentSlotScreen.slotMachine.UpdateScore(totalScore);
	}

	// Fade in Free Spin
	public void FadeInFreeSpin(int numb, bool shouldPause = true) {
		NGUITools.SetActive(gameObject, true);
		NGUITools.SetActive(bigWinView, false);
		NGUITools.SetActive(freeSpinView, true);
		freeSpinNumbLabel.text = numb.ToString("N0") + "\nFREE SPIN";
		TweenAlpha tween = TweenAlpha.Begin(gameObject, 0.5f, 1);
		tween.from = 0;
		transform.localScale = Vector3.one * 3;
		LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutCubic);
		Invoke("FadeOutFreeSpin", 2f);
		if (shouldPause) {
			ScreenManager.Instance.CurrentSlotScreen.PauseSpawnSkill();
			ScreenManager.Instance.CurrentSlotScreen. slotMachine.Wait();
		}
	}

	public void FadeOutFreeSpin() {
		TweenAlpha tween = TweenAlpha.Begin(gameObject, 0.5f, 0);
    EventDelegate.Add(tween.onFinished, Hide, true);
		// ScreenManager.Instance.CurrentSlotScreen.slotMachine.UpdateScore(totalScore);
	}

	void EventFinishFadeIn() {
		StartCoroutine(SpawnEffect());
	}

	IEnumerator SpawnEffect() {
		yield return null;
		Transform hpny1 = MyPoolManager.Instance.Spawn("HappyNewYear", effectPoint1.position, Quaternion.Euler(0, 0, 0), ScreenManager.Instance.CurrentSlotScreen.skillCamera.transform);
		MyPoolManager.Instance.Despawn(hpny1, 2f);
		yield return new WaitForSeconds(0.2f);
		Transform hpny2 = MyPoolManager.Instance.Spawn("HappyNewYear", effectPoint2.position, Quaternion.Euler(0, 0, 0), ScreenManager.Instance.CurrentSlotScreen.skillCamera.transform);
		MyPoolManager.Instance.Despawn(hpny2, 2f);
		yield return new WaitForSeconds(0.1f);
		Transform hpny3 = MyPoolManager.Instance.Spawn("HappyNewYear", effectPoint3.position, Quaternion.Euler(0, 0, 0), ScreenManager.Instance.CurrentSlotScreen.skillCamera.transform);
		MyPoolManager.Instance.Despawn(hpny3, 2f);
	}
	
	public void Hide() {
		NGUITools.SetActive(gameObject, false);
		if (ScreenManager.Instance.CurrentSlotScreen != null) {
			ScreenManager.Instance.CurrentSlotScreen.ResumeSpawnSkill();
		}
	}
}
