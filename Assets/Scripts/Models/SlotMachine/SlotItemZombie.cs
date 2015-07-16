﻿using UnityEngine;
using System.Collections;

public class SlotItemZombie : SlotItem {
	
	public const int ITEM_WILD = 0;
	public const int ITEM_CHOPPER = 1;
	public const int ITEM_USOOP = 2;
	public const int ITEM_NAMI = 3;
	public const int ITEM_PISTOL = 4;
	public const int ITEM_BROOK = 5;
	public const int ITEM_NICO = 6;
	public const int ITEM_MACHINEGUN = 7;
	public const int ITEM_ZORO = 8;
	public const int ITEM_LUFFY = 9;
	public const int ITEM_RALLY = 10;

  private string[] spriteNames = new string[11] {"character-13", "9-02", "3-02", "5-02", "2-02", "8-02", "crossBow", "7-02", "1-02", "4-02", "6-02"};
	
  public override string GetSpriteName(int index) {
    return spriteNames[index];
  }
}
