using UnityEngine;

namespace ArribleTest.Core
{
    public class IntractableDoor : MonoBehaviour, IIntractable
    {
        [SerializeField]
        GameObject m_door;
        [SerializeField]
        Vector3 m_closePosition;
        [SerializeField]
        Vector3 m_openPosition;
        [SerializeField]
        Vector3 m_currentPosition;
        
        const float k_speed = 1;
        const string k_Open = "Open";
        const string k_Close = "Close";
        
        bool m_move;

        void Awake()
        {
            var tran = m_door.transform.position;
        }

        void Update()
        {
            if (m_move)
            {
                var step = k_speed * Time.deltaTime;
                m_door.transform.position = Vector3.MoveTowards(m_door.transform.position, m_currentPosition, step);
                if (Vector3.Distance(m_door.transform.position, m_openPosition) < 0.001f || Vector3.Distance(m_door.transform.position, m_closePosition) < 0.001f)
                {
                    m_move = false;
                }
            }
        }

        public bool State { get; private set; } = false;

        public string GetMessage()
        {
            return $"Press to {(State ? k_Close : k_Open)}";
        }

        public void ChangeState()
        {
            State = !State;
            m_move = true;
            m_currentPosition = State ? m_openPosition : m_closePosition;
        }
    }
}
