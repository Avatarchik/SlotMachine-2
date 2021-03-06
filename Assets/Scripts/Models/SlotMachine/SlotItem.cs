﻿using UnityEngine;
using System.Collections;

public class SlotItem : MonoBehaviour {

  public enum Type {
    WILD,               // 0
    TILE_1,              // 1
    TILE_2,         // 2
    TILE_3,             // 3
    TILE_4,           // 4
    TILE_5,           // 5
    TILE_6,        // 6
    TILE_7,       // 7
    TILE_8,           // 8
    TILE_9,       // 9
    TILE_10,       // 10 - JACKPOT ITEM
    TOTAL
  }
  
  public UISprite sprite;
  public UISprite glowSprite;
  
  private static string[] baseSpriteNames = new string[10] {"wild", "apple", "strawberry", "radish", "broccoli", "eggplant", "bell_pepper", "chili_pepper", "mushroom", "fruits"};
  private Type type;
  private int index;
	private TweenAlpha tweenAlpha;

  public virtual string GetSpriteName(int index) {
    return baseSpriteNames[index];
  }

	public static string GetItemSpriteName(BaseSlotMachineScreen.GameType gameType, int index) {
		switch(gameType) {
			case BaseSlotMachineScreen.GameType.SLOT_DRAGON:
				return SlotItemDragon.spriteNames[index];
			break;
			case BaseSlotMachineScreen.GameType.SLOT_PIRATE:
				return SlotItemPirate.spriteNames[index];
			break;
			case BaseSlotMachineScreen.GameType.SLOT_ZOMBIE:
				return SlotItemZombie.spriteNames[index];
			break;
		}
		return null;
	}

  public virtual void Init(Type type, int index) {
    if (sprite == null) {
      sprite = GetComponent<UISprite>();
    }
		if (glowSprite == null) {
			glowSprite = transform.Find("Glow").GetComponent<UISprite>();
			glowSprite.color = Color.red;
			NGUITools.SetActive(glowSprite.gameObject, false);
		}
    this.type = type;
    this.index = index;
    sprite.spriteName = GetSpriteName((int)type);
  }
  
  public bool IsTarget(int targetIndex) {
    return targetIndex == index;
  }
	
	public virtual void Glow() {
		NGUITools.SetActive(glowSprite.gameObject, true);
		sprite.color = Color.red;
		tweenAlpha = TweenAlpha.Begin(gameObject, 0.8f, 0.2f);
		tweenAlpha.from = 1f;
		tweenAlpha.style = UITweener.Style.PingPong;
	}
	
	public virtual void StopGlow() {
		Destroy(tweenAlpha);
		NGUITools.SetActive(glowSprite.gameObject, false);
		sprite.alpha = 1f;
		sprite.color = Color.white;
	}
}
