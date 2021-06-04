using System.Collections.Generic;
using ArribleTest.Core;
using UnityEngine;

namespace ArribleTest.Core
{
    public class RobotsListController : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> m_Robots;
        [SerializeField]
        List<GameObject> m_RobotHeads;

        public GameObject RobotHeadPoint { get; private set; }

        public GameObject ChooseRobot(RobotType type)
        {
            var robotIndex = (int)type;
            GameObject robot = null;

            if (m_Robots.Count <= robotIndex)
                return null;

            for (var i = 0; i < m_Robots.Count; i++)
            {
                if (i == robotIndex)
                {
                    robot = m_Robots[i];
                    RobotHeadPoint = m_RobotHeads[i];
                }
                else
                {
                    Destroy(m_Robots[i]);
                    Destroy(m_RobotHeads[i]);
                }
            }

            RobotInit(robot);

            return robot;
        }

        void RobotInit(GameObject robot)
        {
            if (robot == null)
                return;

            var controller = robot.GetComponent<RobotController>();
            controller.SetHeadPoint(RobotHeadPoint);
        }
    }
}
