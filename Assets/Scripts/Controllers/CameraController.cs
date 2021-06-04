using Cinemachine;
using UnityEngine;

namespace ArribleTest.Core
{
    public class CameraController: MonoBehaviour
    {
        [SerializeField]
        CinemachineFreeLook m_VCamera;

        public void SetTarget(GameObject target)
        {
            m_VCamera.LookAt = target.transform;
            m_VCamera.Follow = target.transform;
        }

        public void ShowCinemachineFreeLook(bool show)
        {
            m_VCamera.gameObject.SetActive(show);
        }
    }
}
