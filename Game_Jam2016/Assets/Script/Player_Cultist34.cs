using UnityEngine;
using System.Collections;

public class Player_Cultist34 : MonoBehaviour, IPlayer
{
    public int immunityTime;
    public int playerMovementSpeed;
    public Transform m_RightImpSpot;

    private const int MAX_HEALTH = 30;
    private int m_CurrentHealth;

    private float m_VerticalInput = 0;
    private float m_HorizontalInput = 0;

    private bool m_HasImp = false;
    private Imp currentImpInHand;

    private Vector3 m_MoveDirection;
    private Animator m_Animator;

    private bool m_IsPlayer3=true;

    void Awake()
    {
       
        m_Animator = GetComponent<Animator>();
        m_MoveDirection = Vector3.zero;
        m_CurrentHealth = MAX_HEALTH;

        if(this.gameObject.name=="Cultist 4")
        {
            SetPlayer3(false);
        }
    }

    public void SetPlayer3(bool player)
    {
        m_IsPlayer3 = player;
    }

    public void TakeDamage(int aAmountOfDamage)
    {
        //TODO: Make the player flash and be immune for several seconds
        m_CurrentHealth -= aAmountOfDamage;
    }

    void Update()
    {
        if (m_CurrentHealth <= 0)
        {
            Game_Controller.GameOver();
            Time.timeScale = 0;
        }

       // Debug.Log(m_CurrentHealth);
        if (m_HasImp)
        {
            playerMovementSpeed = 1;
        }
        else
        {
            playerMovementSpeed = 3;
        }


        UpdateInput();

        m_MoveDirection = new Vector3(m_HorizontalInput, m_VerticalInput, 0);
        transform.position += m_MoveDirection * playerMovementSpeed * Time.deltaTime;
    }

    void UpdateInput()
    {
        m_Animator.SetFloat("HorizontalInput", m_HorizontalInput);
        m_Animator.SetFloat("VerticalInput", m_VerticalInput);

        if (m_IsPlayer3)
        {
            m_VerticalInput = Input.GetAxis("Joystick3Vertical");
            m_HorizontalInput = Input.GetAxis("Joystick3Horizontal");

            if (Input.GetButtonDown("Joystick3FireImp"))
            {
                FireImp();
            }
        }
        else
        {
            m_VerticalInput = Input.GetAxis("Joystick4Vertical");
            m_HorizontalInput = Input.GetAxis("Joystick4Horizontal");

            if (Input.GetButtonDown("Joystick4FireImp"))
            {
                FireImp();
            }
        }
    }


    void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (!m_HasImp)
        {
            if (aCollision.gameObject.GetComponent<Imp>() != null)
            {
                currentImpInHand = aCollision.gameObject.GetComponent<Imp>();
                currentImpInHand.GetComponent<Imp>().enabled = false;
                aCollision.transform.position = m_RightImpSpot.position;
                aCollision.transform.SetParent(transform);
                m_HasImp = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D aCollider2D)
    {
        if (currentImpInHand != null)
        {
            if (aCollider2D.CompareTag("Fire"))
            {
                //Collided with the fire
                Game_Controller.AddToDemonBar(10);
                Destroy(currentImpInHand.gameObject);
                currentImpInHand = null;
                m_HasImp = false;
            }
        }
    }

    void FireImp()
    {
        if (currentImpInHand != null)
        {

            currentImpInHand.transform.SetParent(null);
            currentImpInHand.GetComponent<Imp>().enabled = true;
            currentImpInHand = null;
            m_HasImp = false;
        }
    }


}
