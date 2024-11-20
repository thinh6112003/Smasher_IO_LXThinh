using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class SkinShopItem 
{
    public Sprite spriteLock;
    public Sprite spriteOpen;
    public Type type; //common, rare, epic
    public Style style;// heroA, heroB, amongus
}
public enum Type
{
    COMMON,
    RARE,
    EPIC
}
public enum Style
{
    HEROA,
    HEROB,
    AMONGUS
}
