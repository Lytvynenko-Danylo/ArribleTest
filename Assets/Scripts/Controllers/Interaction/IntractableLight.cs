using System.Collections.Generic;
using UnityEngine;

namespace ArribleTest.Core
{
    public class IntractableLight : MonoBehaviour, IIntractable
    {
        [SerializeField] private List<GameObject> m_lamps;
        
        const string k_On = "On";
        const string k_Off = "Off";
        public bool State { get; private set; }
        

        public string GetMessage()
        {
            return $"Press to {(State ? k_On : k_Off)} light";
        }

        public void ChangeState()
        {
            foreach (var lamp in m_lamps)
            {
                lamp.SetActive(State);
            }

            State = !State;
        }
    }
}
