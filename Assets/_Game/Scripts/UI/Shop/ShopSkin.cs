using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkin : Shop
{
    [SerializeField] private GameObject skinBase;
    [SerializeField] private SkinShopSO skinShopSO;
    private RectTransform shopPanel;  // RectTransform của shop panel
    private Vector2 originalAnchorMin;  // Lưu lại giá trị anchorMin ban đầu
    private Vector2 originalAnchorMax;  // Lưu lại giá trị anchorMax ban đầu
    private Vector2 originalPivot; // Lưu lại giá trị pivot ban đầu
    private Vector2 originalPosition;  // Lưu lại vị trí ban đầu của shopPanel
    private float originalWith;
    private float originalHeight;

    protected override void Start()
    {
        base.Start();
        buttonBack.onClick.AddListener(CloseShopPanel);
    }
    protected override void Init()
    {
        for (int i = 0; i < skinShopSO.skinShopItems.Count; i++)
        {
            shopItems.Add(skinShopSO.skinShopItems[i]);
        }
        Debug.Log("shopskin");
        currentIDSelect = DataRuntimeManager.Instance.dynamicData.GetIdSkin();
        base.Init();
    }
    protected override void OnclickButtonEquip()
    {
        base.OnclickButtonEquip();

        dynamicData.SetIdSkin(currentIDSelect);
        Observer.Noti(constr.CHANGESKIN);
    }
    public override void ChangeSelectByID(int id)
    {
        base.ChangeSelectByID(id);
        if (skinShopSO.skinShopItems[id].style == ShopStyle.HEROA)
            skinBase.SetActive(true);
        else
            skinBase.SetActive(false);
    }
    protected override void SetOpen()
    {
        DataRuntimeManager.Instance.shopData.SetOpenSkin(currentIDSelect);
    }
    public override void AddItemShopWithType(int i, ref ShopUnit newShopItemUnit)
    {
        newShopItemUnit.Init(shopItems[i], DataRuntimeManager.Instance.shopData.GetSkinStatus(i), i, this, currentIDEquip == i);
    }
    protected override void OnEnable()
    {
        base.OnEnable(); 
        shopPanel = GetComponent<RectTransform>();
        // Lưu lại giá trị anchor ban đầu (Stretch-Stretch)
        originalAnchorMin = shopPanel.anchorMin;
        originalAnchorMax = shopPanel.anchorMax;
        originalPosition = shopPanel.localPosition;
        originalPivot = shopPanel.pivot;
        originalWith = shopPanel.rect.width;
        originalHeight = shopPanel.rect.height;
        OpenShopPanel();
        Debug.Log("open shop panel");
    }

    void OpenShopPanel()
    {
        shopPanel.anchorMin = new Vector2(0.5f, 0.5f);
        shopPanel.anchorMax = new Vector2(0.5f, 0.5f);
        shopPanel.pivot = new Vector2(0.5f, 0.5f);
        shopPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWith);
        shopPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalHeight);
        // Đặt vị trí ban đầu ra ngoài màn hình (bên phai)
        shopPanel.localPosition = new Vector3(Screen.width, 0, 0);

        // Di chuyển shop panel từ ngoài màn hình vào giữa màn hình
        shopPanel.DOLocalMove(Vector3.zero, 0.6f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            // Sau khi animation xong, có thể set lại anchor preset về Stretch-Stretch nếu cần
            ResetAnchorPreset();
        });
    }
    void CloseShopPanel()
    {
        shopPanel.anchorMin = new Vector2(0.5f, 0.5f);
        shopPanel.anchorMax = new Vector2(0.5f, 0.5f);
        shopPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWith);
        shopPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalHeight);

        // Di chuyển shop panel ra ngoai man hinh
        shopPanel.DOLocalMove(new Vector3(Screen.width, 0, 0), 0.6f)
            .SetEase(Ease.OutCubic)
            .SetDelay(0.2f)
            .OnComplete(() =>
            {
                // Sau khi animation xong, có thể set lại anchor preset về Stretch-Stretch nếu cần
                ResetAnchorPreset();
                UIManager.Instance.SetUIScene(UIManager.SceneUIType.Home);
            });
    }
    void ResetAnchorPreset()
    {
        // Set lại anchor preset ban đầu (Stretch-Stretch)
        shopPanel.anchorMin = originalAnchorMin;
        shopPanel.anchorMax = originalAnchorMax;
        shopPanel.pivot = originalPivot;
        shopPanel.localPosition = originalPosition;
        shopPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWith);
        shopPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalHeight);
    }
}