using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController instance;
    private Animator anim;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, -2, transform.position.z);
        anim = GetComponent<Animator>();
    }

    public void Setanimation(string actionName,bool status)
    {
        anim.SetBool(actionName, status);
    }

    public void SetTriggerAnimation(string actionName)
    {
        anim.SetTrigger(actionName);
    }

}
