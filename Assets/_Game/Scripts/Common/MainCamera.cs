using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Threading.Tasks;

public class MainCamera : Singleton<MainCamera>
{
    [SerializeField] private Character playerInstance;
    public Camera _mainCamera;
    [SerializeField] private float paramForScale = 70f;
    [SerializeField] private Vector3 offsetOrigin = new Vector3(0,10,15);
    [SerializeField] private bool isBlendingPrevios = false;
    [SerializeField] private CinemachineBrain currentCameraBrain;
    [SerializeField] private CinemachineVirtualCamera currentCamera;
    [SerializeField] private CinemachineVirtualCamera cameraPlay;
    [SerializeField] private CinemachineVirtualCamera camera_End_Loby;
    [SerializeField] private CinemachineTransposer cameraPlay_Composer;
    [SerializeField] private CinemachineTransposer camera_End_Loby_Composer;
    [SerializeField] private CinemachineTransposer currentCamera_Composer;
    [SerializeField] private Camera_Type currentCameraType;
    private Vector3 playCameraOffsetOrigin= new Vector3(0, 12.2299995f, -11.4899998f);
    private Vector3 endLobyCameraOffsetOrigin= new Vector3(0, 12.2299995f, -11.4899998f);

    private void Start()
    {
        SetRefCamera(ref camera_End_Loby_Composer, camera_End_Loby, ref endLobyCameraOffsetOrigin);
        SetRefCamera(ref cameraPlay_Composer, cameraPlay, ref playCameraOffsetOrigin);
        SetCamera(Camera_Type.Loby_End);
        Observer.AddListener(constr.CHANGE_CAMERA_PLAY, SetCamera_Play);
        Observer.AddListener(constr.LEVELUP, UpdateCamera);
        Observer.AddListener(constr.WINGAME, SetCamera_Loby_End);
        Observer.AddListener(constr.DONELOADLEVEL, SetCamera_Loby_End);
    }
    private void Update()
    {
        listenerDoneChangeCam();
    }
    private void SetRefCamera(ref CinemachineTransposer cameraComposer, CinemachineVirtualCamera cinemachineVirtualCamera, ref Vector3 mDistanceOrigin)
    {
        cameraComposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        mDistanceOrigin = cameraComposer.m_FollowOffset;
    }
    private void listenerDoneChangeCam()
    {
        if (currentCameraBrain.IsBlending == false && isBlendingPrevios == true && currentCameraType == Camera_Type.Play)
        {
            cameraPlay_Composer.m_FollowOffset = playCameraOffsetOrigin;
            Observer.Noti(constr.DONECHANGECAM);
            
        }
        isBlendingPrevios = currentCameraBrain.IsBlending;
    }
    public void SetCamera_Play()
    {
        cameraPlay_Composer.m_FollowOffset = playCameraOffsetOrigin;
        SetCamera(Camera_Type.Play);
    }
    public async void SetCamera_Loby_End()
    {
        await Task.Delay(600);
        camera_End_Loby_Composer.m_FollowOffset = endLobyCameraOffsetOrigin *( ((float)playerInstance.level - 1) / paramForScale + 1f);
        SetCamera(Camera_Type.Loby_End);
        await Task.Delay(400);
        offsetTerm = currentCamera_Composer.m_FollowOffset;
        float scaleDistanceCamera = 1;
        DOTween.To(() => offsetTerm, x => offsetTerm = x, offsetOrigin * scaleDistanceCamera, 1.5f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentCamera_Composer.m_FollowOffset = offsetTerm;
            })
            .SetUpdate(UpdateType.Fixed);
    }

    public Vector3 offsetTerm= new Vector3(0, 0, 0);
    public void UpdateCamera()
    {
        offsetTerm = currentCamera_Composer.m_FollowOffset;
        float scaleDistanceCamera = ((float)playerInstance.level - 1) / paramForScale + 1f;
        DOTween.To(() => offsetTerm, x => offsetTerm = x, offsetOrigin * scaleDistanceCamera, 0.2f)
            .OnUpdate(() =>
            {
                currentCamera_Composer.m_FollowOffset = offsetTerm;
            })
            .SetUpdate(UpdateType.Fixed);
    }
    public void ChangeCamera(CinemachineVirtualCamera cameraSet, CinemachineTransposer cameraSetComposer, Vector3 originOffsetSet)
    {
        if (currentCamera != cameraSet)
        {
            if (currentCamera != null) currentCamera.Priority = 0;
            offsetOrigin = originOffsetSet;
            currentCamera = cameraSet;
            currentCamera_Composer = cameraSetComposer;
            currentCamera.Priority = 1;
        }
    }
    public void SetCamera(Camera_Type cameraType)
    {
        switch (cameraType)
        {
            case Camera_Type.Play:
                currentCameraType = Camera_Type.Play;
                ChangeCamera(cameraPlay, cameraPlay_Composer, playCameraOffsetOrigin);
                break;
            case Camera_Type.Loby_End:
                currentCameraType = Camera_Type.Loby_End;
                ChangeCamera(camera_End_Loby, camera_End_Loby_Composer, endLobyCameraOffsetOrigin);
                break;
        }
    }

}
public enum Camera_Type
{
    Play,
    Loby_End,
}