using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;
    [SerializeField]
    private VariableJoystick variableJoystickRotate;
    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool Can_Rotate = true;

    [SerializeField]
    private float sensivity;

    [SerializeField]
    private Vector2 default_LookLimits = new Vector2(-70f, 80f);

    public float speed;
    private Vector2 look_Angle;

    private Vector2 current_MouseLook;
    private Vector2 smooth_Move;


    void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LockNadUnlockCursor();
        LookAround();
    } // Update


    void LockNadUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }// LockNadUnlockCursor


    void LookAround()
    {

        if (Cursor.lockState == CursorLockMode.Locked)
        { 
            current_MouseLook = new Vector2(Input.GetAxis(Axis.MouseY), Input.GetAxis(Axis.MouseX));
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            current_MouseLook = new Vector2(variableJoystickRotate.Vertical * speed, variableJoystickRotate.Horizontal * speed);
        }

        look_Angle.x += current_MouseLook.x * sensivity * (invert ? 1f : -1f);// -1;
        look_Angle.y += current_MouseLook.y * sensivity ;
        look_Angle.x =Mathf.Clamp(look_Angle.x,default_LookLimits.x,default_LookLimits.y);

       
        lookRoot.localRotation = Quaternion.Euler(look_Angle.x, 0, 0);
        playerRoot.localRotation = Quaternion.Euler(0, look_Angle.y, 0);
    } // LookAround



}// class
