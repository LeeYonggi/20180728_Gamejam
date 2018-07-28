using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ANIMATION_STATE
{
    IDLE,
    JUMP,
    ATTACK1
}
public class CharacterAction : MonoBehaviour {

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    public int speed;

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
        if(Input.GetKeyDown(KeyCode.A) && Animation_state == ANIMATION_STATE.IDLE)
        {
            StartCoroutine("Attack1");
            
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

    IEnumerator Attack1()
    {
        m_Animator.SetInteger("Anime_State", (int)ANIMATION_STATE.JUMP);
        Animation_state = ANIMATION_STATE.JUMP;

        yield return new WaitForSeconds(0.5f);

        m_Rigidbody.AddForce(new Vector3(0, 50 * speed));

        m_Animator.SetInteger("Anime_State", (int)ANIMATION_STATE.ATTACK1);
    }
}
