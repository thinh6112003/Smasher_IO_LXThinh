using UnityEngine;

public class FrustumCullingExample : MonoBehaviour
{
    private Camera cam;
    private Renderer _renderer;
    private void Awake()
    {

    //    // Lấy camera hiện tại
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = false;
        cam = MainCamera.Instance._mainCamera;
    }

    void Update()
    {
        // Kiểm tra nếu đối tượng này nằm trong vùng nhìn của camera
        if (IsInFrustum())
        {
            // Nếu nằm trong frustum, bật render
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            // Nếu không nằm trong frustum, tắt render
            GetComponent<Renderer>().enabled = false;
        }
    }

    bool IsInFrustum()
    {
        // Kiểm tra xem bounding box có nằm trong frustum không
        if (cam == null || _renderer == null) return false;
        return GeometryUtility.TestPlanesAABB(
            GeometryUtility.CalculateFrustumPlanes(cam),
            _renderer.bounds
        );
    }
}
