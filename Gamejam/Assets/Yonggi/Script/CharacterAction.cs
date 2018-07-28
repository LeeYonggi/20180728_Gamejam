using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ANIMATION_STATE
{
    IDLE,
    JUMP,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    ATTACK4
}
public class CharacterAction : MonoBehaviour {

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    public int speed;
    private Vector3 p_Velocity;

    internal ANIMATION_STATE Animation_state { get; set; }

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Animation_state = ANIMATION_STATE.IDLE;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && Animation_state == ANIMATION_STATE.IDLE)
        {
            StartCoroutine("Jump");
        }
        if (Input.GetKeyDown(KeyCode.A) && Animation_state != ANIMATION_STATE.IDLE)
        {
            switch (Animation_state)
            {
                case ANIMATION_STATE.JUMP:
                    Attack(ANIMATION_STATE.ATTACK1);
                    break;
                case ANIMATION_STATE.ATTACK1:
                    Attack(ANIMATION_STATE.ATTACK2);
                    break;
                case ANIMATION_STATE.ATTACK2:
                    Attack(ANIMATION_STATE.ATTACK3);
                    break;
                case ANIMATION_STATE.ATTACK3:
                    Attack(ANIMATION_STATE.ATTACK4);
                    break;
                default:
                    break;
            }
            StartCoroutine("AttackDelay");
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Idle();
        }
    }

    IEnumerator Jump()
    {
        m_Animator.SetInteger("Anime_State", (int)ANIMATION_STATE.JUMP);
        Animation_state = ANIMATION_STATE.JUMP;

        yield return new WaitForSeconds(0.5f);

        m_Rigidbody.AddForce(new Vector3(0, 50 * speed));
    }

    void Idle()
    {
        m_Animator.SetInteger("Anime_State", (int)ANIMATION_STATE.IDLE);
        Animation_state = ANIMATION_STATE.IDLE;
    }

    void Attack(ANIMATION_STATE state)
    {
        m_Animator.SetInteger("Anime_State", (int)state);
        Animation_state = state;
    }

    IEnumerator AttackDelay()
    {
        p_Velocity = m_Rigidbody.velocity;
        m_Rigidbody.useGravity = false;
        m_Rigidbody.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.5f);

        m_Rigidbody.velocity = p_Velocity;
        m_Rigidbody.useGravity = true;
    }
}
