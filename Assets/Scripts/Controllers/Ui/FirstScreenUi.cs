using System;
using System.Collections.Generic;
using ArribleTest.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ArribleTest.UI
{
    public class FirstScreenUi : MonoBehaviour
    {
        [SerializeField]
        List<Button> m_Buttons;

        public event Action<RobotType> OnChosenRobot = delegate { };

        void Awake()
        {
            for (var i = 0; i < m_Buttons.Count; i++)
            {
                var type = (RobotType)i;
                m_Buttons[i].onClick.AddListener(() => OnChosenRobot.Invoke(type));
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
