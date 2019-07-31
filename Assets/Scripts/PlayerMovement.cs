using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{

    private CharacterController character_Controller;
    public VariableJoystick variableJoystick;
    public Vector3 move_Direction;
    public bool isJoyStick;
    public float speed,slideSpeed = 5f;
    public float gravity = 20f;
    public Toggle toggle;
    public float jump_Force = 10f;
    public float vertical_Velocity;
    bool isSprint,isMove, isSlide;
    float yPos;
    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        isJoyStick = toggle.isOn;

        MoveThePlayer();
    }

    void MoveThePlayer()
    {

       // print(variableJoystick.Horizontal);
        if (isJoyStick)
        {
            isMove = true;
            move_Direction = new Vector3(variableJoystick.Horizontal , 0f,
                                       variableJoystick.Vertical );
        }
        else
        {
            isMove = false;
            move_Direction = new Vector3(Input.GetAxis(Axis.Horizontal), 0f,
                                    Input.GetAxis(Axis.Vertical));
        }

        move_Direction = transform.TransformDirection(move_Direction);

        if(isSlide)
        {
            move_Direction *= slideSpeed * Time.deltaTime;
            isSlide = false;
        }
        else
        {
            move_Direction *= speed * Time.deltaTime;
        }
        

        ApplyGravity();

        character_Controller.Move(move_Direction);

        if (isSprint)
        {
            PlayerAnimationController.instance.Setanimation("Walk", false);
            PlayerAnimationController.instance.Setanimation("Backward", false);
            PlayerAnimationController.instance.Setanimation("Sprint", true);
        }
        // print(character_Controller.velocity.z >0);
        if (character_Controller.velocity.z == 0 )
        {
            PlayerAnimationController.instance.Setanimation("Walk", false);
            PlayerAnimationController.instance.Setanimation("Backward", false);
            PlayerAnimationController.instance.Setanimation("Sprint", false);
        }
        else if ((Input.GetKey(KeyCode.W) || isMove) && !isSprint)
        {
            PlayerAnimationController.instance.Setanimation("Backward", false);
            PlayerAnimationController.instance.Setanimation("Sprint", false);
            PlayerAnimationController.instance.Setanimation("Walk", true);
           
        }
        else if ((Input.GetKey(KeyCode.S)|| isMove) && !isSprint)
        {
            PlayerAnimationController.instance.Setanimation("Walk", false);
            PlayerAnimationController.instance.Setanimation("Sprint", false);
            PlayerAnimationController.instance.Setanimation("Backward", true);
        }
      

    } // move player

    public void Sprint()
    {
        if (!isSprint)
        {
            isSprint = true;
            speed = 20;
        }
        else
        {
            isSprint = false;
            PlayerAnimationController.instance.Setanimation("Sprint", false);
            speed = 5;
        }

    } // Sprint

    public void Slide()
    {
        isSlide = true;
        PlayerAnimationController.instance.SetTriggerAnimation("Slide");
    }// Slide

    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;
        PlayerJump();
        move_Direction.y = vertical_Velocity * Time.deltaTime;


    } // apply gravity

    void PlayerJump()
    {

        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAnimationController.instance.SetTriggerAnimation("Jump");
            yPos = transform.localPosition.y;
            vertical_Velocity = jump_Force;

           
        }

    } // PlayerJump


}
