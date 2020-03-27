using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlayerControl : MonoBehaviour
{

    public delegate void PickItem();
    public event PickItem OnPick;


    Animator m_Animator;
    Transform m_Transform;
    MyGame.Interaction interaction;

    public UpdateProgressBar cooldownBar;

    [SerializeField]
    private bool working = false;
    private Vector3 m_Velocity;
    private float speed = 1.75f;
    private float acceleration = 50f;
    private const float actionLength = 2f;
    public float actionTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Transform = GetComponent<Transform>();
        interaction = GetComponent<MyGame.Interaction>();
        cooldownBar = GameObject.FindWithTag("Cooldown").GetComponent<UpdateProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (working == false)
        {
            HandleMovementInput();
            Move();
        }
        HandleActions();
    }

    void HandleMovementInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0f)
        {
            m_Velocity.z += acceleration * Input.GetAxis("Vertical") * Time.deltaTime;
        }
        else if (m_Velocity.z != 0f)
        {
            if (m_Velocity.z > 0f)
            {
                m_Velocity.z -= acceleration * Time.deltaTime;
                if (m_Velocity.z < 0f)
                {
                    m_Velocity.z = 0f;
                }
            }
            else
            {
                m_Velocity.z += acceleration * Time.deltaTime;
                if (m_Velocity.z > 0f)
                {
                    m_Velocity.z = 0f;
                }
            }
        }

        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            m_Velocity.x += acceleration * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (m_Velocity.x != 0f)
        {
            if (m_Velocity.x > 0f)
            {
                m_Velocity.x -= acceleration * Time.deltaTime;
                if (m_Velocity.x < 0f)
                {
                    m_Velocity.x = 0f;
                }
            }
            else
            {
                m_Velocity.x += acceleration * Time.deltaTime;
                if (m_Velocity.x > 0f)
                {
                    m_Velocity.x = 0f;
                }
            }
        }

        if (m_Velocity.sqrMagnitude > (speed * speed))
        {
            m_Velocity.Normalize();
            m_Velocity *= speed;
        }
    }

    void Move()
    {
        if (m_Velocity.sqrMagnitude != 0f)
        {
            m_Animator.SetInteger("State", 1);
            var speed = m_Velocity * Time.deltaTime;
            m_Transform.Translate(speed, Space.World);
            m_Transform.LookAt(m_Transform.position + speed);
        }
        else
        {
            m_Animator.SetInteger("State", 0);
        }
    }

    void HandleActions()
    {
        if (Input.GetButton("Submit") && m_Velocity.sqrMagnitude == 0f && interaction.hasTarget)
        {
            if (working == false)
            {
                working = true;
                m_Animator.SetInteger("State", 3);
            }
            else
            {
                actionTimer += Time.deltaTime;
                if (actionTimer >= actionLength)
                {
                    working = false;
                    interaction.hasTarget = false;
                    actionTimer = 0f;
                    m_Animator.SetInteger("State", 0);
                    
                    // Destroy target and increase points
                    interaction.target.SetActive(false);
                    if (OnPick != null) {
                        OnPick();
                    }
                }
                cooldownBar.progress = actionTimer / actionLength * 100f;
            }
        }
        else if (working)
        {
            working = false;
            actionTimer = 0f;
            cooldownBar.progress = 0f;
            m_Animator.SetInteger("State", 0);
        }
    }
}
