using System;
using ArribleTest.Core;
using TMPro;
using UnityEngine;

namespace ArribleTest.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField]
        FirstScreenUi m_FirstScreen;
        [SerializeField]
        GameObject m_MovementTip;
        [SerializeField]
        GameObject m_InteractionTip;
        [SerializeField]
        TextMeshProUGUI m_InteractionTipText;
        
        public event Action<RobotType> OnChosenRobot = delegate { };

        bool m_ShowMovementTip;

        private float m_seconds = 0;

        void Awake()
        {
            m_FirstScreen.OnChosenRobot += OnChosenRobotHandler;
            HideAllUI();
            m_FirstScreen.Show();
        }

        void Update()
        {
            m_seconds += Time.deltaTime;
            if (m_seconds > 1)
            {
                m_InteractionTip.gameObject.SetActive(false);
            }
            
            if(!m_ShowMovementTip)
                return;
            
            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (move != Vector3.zero)
            {
                m_ShowMovementTip = false;
                HideAllUI();
            }
        }

        void OnChosenRobotHandler(RobotType type)
        {
            HideAllUI();
            m_MovementTip.gameObject.SetActive(true);
            m_ShowMovementTip = true;
            OnChosenRobot.Invoke(type);
        }


        void HideAllUI()
        {
            m_FirstScreen.Hide();
            m_MovementTip.gameObject.SetActive(false);
            m_InteractionTip.gameObject.SetActive(false);
        }

        public void SetInteractionTipText(string tip)
        {
            m_seconds = 0;
            
            if (string.IsNullOrEmpty(tip) || m_MovementTip.gameObject.activeSelf)
            {
                m_InteractionTip.gameObject.SetActive(false);
                return;
            }
            
            m_InteractionTipText.text = tip;
            m_InteractionTip.gameObject.SetActive(true);
        } 
    }
}
