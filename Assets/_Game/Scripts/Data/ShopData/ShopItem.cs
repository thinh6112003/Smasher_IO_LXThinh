using System;
using UnityEngine;

[Serializable]
public abstract class ShopItem
{
    public Sprite spriteLock;
    public Sprite spriteOpen;
    public Type type; //common, rare, epic
}
