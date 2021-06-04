using ArribleTest.UI;
using UnityEngine;

namespace ArribleTest.Core
{
    public class SceneController : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField]
        UiController m_UiController;
        [SerializeField]
        RobotsListController m_RobotsListController;
        [SerializeField]
        CameraController m_CameraController;

        GameObject m_currentRobot;
        RobotController m_currentRobotController;

        void Awake()
        {
            m_UiController.OnChosenRobot += OnChosenRobotHandler;
        }

        void OnChosenRobotHandler(RobotType type)
        {
            m_currentRobot = m_RobotsListController.ChooseRobot(type);
            if (m_currentRobot != null)
            {
                m_currentRobotController = m_currentRobot.GetComponent<RobotController>();
                m_currentRobotController.OnInteractionZone += OnInteractionZoneHandler;
                m_CameraController.SetTarget(m_RobotsListController.RobotHeadPoint);
                m_CameraController.ShowCinemachineFreeLook(true);
            }
        }

        void OnInteractionZoneHandler(string message)
        {
            m_UiController.SetInteractionTipText(message);
        }
    }
}
