using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    //Quaternion(四元数)是一种存储旋转的方式;
    Quaternion m_Rotation = Quaternion.identity;



    // Start is called before the first frame update
    void Start()
    {
        //初始化时获取Animator
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //由Update修改为FixedUpdate
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //set空间向量
        m_Movement.Set(horizontal, 0f, vertical);
        //归化向量方向    
        m_Movement.Normalize();

        //设置一个hasHorizontalInput(bool) 
        //Mathf类它需要两个float参数并返回一个bool  
        //如果两个浮点数大致相等则为true，否则为false。
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //将Animator组件的引用调用SetBool方法。
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);

    }
}
