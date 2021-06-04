using System;
using UnityEngine;

namespace ArribleTest.Core
{
    public class RobotController : MonoBehaviour
    {
        [SerializeField]
        float m_PlayerSpeed = 2.0f;
        [SerializeField]
        float m_GravityValue = -9.81f;
        GameObject m_HeadPoint;

        CharacterController m_Controller;
        Animator m_Animator;
        IIntractable m_IntractableObject;

        Vector3 m_PlayerVelocity;
        bool m_GroundedPlayer;
        bool m_InitSuccess;
        bool m_InitSuccessHead;

        static readonly int s_Move = Animator.StringToHash("Move");
        static readonly int s_Use = Animator.StringToHash("Use");

        public event Action<string> OnInteractionZone = delegate { };

        void Start()
        {
            if (!gameObject.TryGetComponent(typeof(CharacterController), out var component))
            {
                m_InitSuccess = false;
                return;
            }

            m_Controller = (CharacterController)component;

            if (!gameObject.TryGetComponent(typeof(Animator), out component))
            {
                m_InitSuccess = false;
                return;
            }

            m_Animator = (Animator)component;

            m_InitSuccess = true;
        }

        void Update()
        {
            if (!m_InitSuccess || !m_InitSuccessHead)
                return;

            Interaction();
            Movement();
        }

        void Interaction()
        {
            var animateUse = false;
            if (m_Controller.isGrounded && Input.GetButtonDown("Interaction"))
            {
                animateUse = true;
                m_IntractableObject?.ChangeState();
            }

            m_Animator.SetBool(s_Use, animateUse);
        }

        void Movement()
        {
            m_GroundedPlayer = m_Controller.isGrounded;
            if (m_GroundedPlayer && m_PlayerVelocity.y < 0)
            {
                m_PlayerVelocity.y = 0f;
            }

            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            m_Controller.Move(move * Time.deltaTime * m_PlayerSpeed);

            var animateMove = false;
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                m_HeadPoint.transform.forward = move;
                animateMove = true;
            }

            m_Animator.SetBool(s_Move, animateMove);

            m_PlayerVelocity.y += m_GravityValue * Time.deltaTime;
            m_Controller.Move(m_PlayerVelocity * Time.deltaTime);
            m_HeadPoint.transform.position = transform.position;
        }

        public void SetHeadPoint(GameObject headPoint)
        {
            m_HeadPoint = headPoint;
            if (headPoint != null)
                m_InitSuccessHead = true;
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag($"Door"))
            { 
                m_IntractableObject = other.GetComponent<IIntractable>();
                OnInteractionZone.Invoke(m_IntractableObject.GetMessage());
            }
            else
            {
                m_IntractableObject = null;
                OnInteractionZone.Invoke(null);
            }
        }
    }
}