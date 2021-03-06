﻿using UnityEngine;
using System.Collections;

public class SkillSanji : Skill {

	public GameObject[] kicks;
	
	private int attackDamage;

	public override void Init(int level, int damage, BossManager bossManager) {
		this.bossManager = bossManager;
		this.attackDamage = damage;
		int smallDamage = (int)(damage / level);
		for (int i = 0; i < level; i++) {
			if (i == level - 1 && level > 1) {
				StartCoroutine(SpawnParticle(i, i * 0.15f, (damage - ((level - 1) * smallDamage))));
			} else {
				StartCoroutine(SpawnParticle(i, i * 0.15f, smallDamage));
			}
		}
		Invoke("Destroy", 2f);
	}
	
	IEnumerator SpawnParticle(int index, float delay, int smallDamage) {
		yield return new WaitForSeconds(delay);
		kicks[index].SetActive(true);
		bossManager.Shake();
		bossManager.GetHit(smallDamage);
	}
}
