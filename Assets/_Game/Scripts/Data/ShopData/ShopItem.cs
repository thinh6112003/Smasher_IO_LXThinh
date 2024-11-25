using System;
using UnityEngine;

[Serializable]
public abstract class ShopItem
{
    public Sprite spriteLock;
    public Sprite spriteOpen;
    public ShopType type; //common, rare, epic
}
