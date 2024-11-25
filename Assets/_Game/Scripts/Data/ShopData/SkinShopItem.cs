using System;
[Serializable]
public class SkinShopItem : ShopItem
{
    public ShopStyle style;// heroA, heroB, amongus
}
public enum ShopType
{
    COMMON,
    RARE,
    EPIC
}
public enum ShopStyle
{
    HEROA,
    HEROB,
    AMONGUS
}
