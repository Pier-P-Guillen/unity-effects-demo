using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSequence : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _sideVirtualCamera = null;
    [SerializeField] private CinemachineVirtualCamera _farVirtualCamera = null;
    [SerializeField] private CinemachineVirtualCamera _closeVirtualCamera = null;

    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        _sideVirtualCamera.Priority = 10;
        _farVirtualCamera.Priority = 1;
        _closeVirtualCamera.Priority = 1;

        yield return new WaitForSeconds(0.5f);

        _sideVirtualCamera.Priority = 1;
        _farVirtualCamera.Priority = 10;
        _closeVirtualCamera.Priority = 1;

        yield return new WaitForSeconds(2f);

        _sideVirtualCamera.Priority = 1;
        _farVirtualCamera.Priority = 1;
        _closeVirtualCamera.Priority = 10;

        yield return new WaitForSeconds(2f);

    }
}
