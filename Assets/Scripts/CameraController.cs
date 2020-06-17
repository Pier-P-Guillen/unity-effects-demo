using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController : MonoBehaviour
{

    public static CameraController Instance { get; private set; }

    private float _totalTime = 0f;
    private float _remaingTime = 0f;
    private float _initialIntensity = 0;
    private CinemachineVirtualCamera _virtualCamera = null;


    private void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        var multiChannelPerlin =
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _initialIntensity = intensity;
        multiChannelPerlin.m_AmplitudeGain = intensity;

        _remaingTime = time;
        _totalTime = time;
    }

    private void Update()
    {
        _remaingTime -= Time.deltaTime;
        if (_remaingTime > 0f)
        {
            var multiChannelPerlin =
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0, _initialIntensity, _remaingTime / _totalTime);
        }
    }
}
