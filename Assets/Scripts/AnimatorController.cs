using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    JoystickPlayerExample joyStick;

    [SerializeField]
    FireManager fireManager;
    private void Update()
    {
        animator.SetFloat("Speed X", joyStick.direction.x);
        
        animator.SetFloat("Speed Z", joyStick.direction.z);
      
        if (joyStick.direction.x != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        if (fireManager.isActiceFire)
        {
            animator.SetBool("isActiveFire", true);
        }
        else
        {
            animator.SetBool("isActiveFire", false);
        }




    }
}
