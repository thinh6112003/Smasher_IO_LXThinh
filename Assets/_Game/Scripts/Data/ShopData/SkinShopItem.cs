using System;
[Serializable]
public class SkinShopItem : ShopItem
{
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
