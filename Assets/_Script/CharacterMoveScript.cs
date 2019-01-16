using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CharacterMoveScript : MonoBehaviour, IPointerClickHandler
{
    private Animator _anim;

    private Transform _transform;
    private NavMeshAgent navAgent;
    private bool m_wasGrounded;
    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();
    
    public void OnPointerClick(PointerEventData pED)
    {
        MovementController.instance.SetChara(this);
        Debug.Log("Clicked");
    }

    // Use this for initialization
    private void Start()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        _transform = gameObject.GetComponent<Transform>();
        _anim = gameObject.GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, new Vector3(1,1,1)) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if(validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        } else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }
    // Update is called once per frame
    private void Update()
    {
        _anim.SetBool("Grounded", m_isGrounded);
        _anim.SetFloat("MoveSpeed", Math.Max(navAgent.velocity.magnitude - 0.1f, 0f));
        
        if (!m_wasGrounded && m_isGrounded)
        {
            _anim.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            _anim.SetTrigger("Jump");
        }
        /*if (navAgent.velocity.y > 0.11)
        {
            _anim.SetBool("Grounded", false);
            _anim.SetTrigger("Jump");
        }
        else
        {
            _anim.SetBool("Grounded", true);
            //_anim.SetBool("Jump", false);
        }
        if (_anim.GetFloat("Speed") <= 0.1)
            
            _anim.SetBool("Rest", true);
        else
            _anim.SetBool("Rest", false); */
        m_wasGrounded = m_isGrounded;
    }

    public void Move(Transform target)
    {
        navAgent.Move(target.position);
    }

    public void Skill1()
    {
    }

    public void Skill2()
    {
    }

    public void Skill3()
    {
    }

    public void SetChara()
    {
        MovementController.instance.SetChara(this);
        Debug.Log("Clicked");
    }

    public void Move(Vector3 location)
    {
        navAgent.destination = location;
    }
}