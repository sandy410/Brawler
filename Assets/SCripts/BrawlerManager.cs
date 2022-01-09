using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCamera;
using Invector.vCharacterController;

public class BrawlerManager : MonoBehaviour
{
   
   public enum CameraAngle {RightAngle, LeftAngle };
    CameraAngle CurrentCameraSide;
  
    public vThirdPersonInput tpInput;
    vLockOn lockonmanager;
    Animator anim;
    vThirdPersonController cc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<vThirdPersonController>();
        if (tpInput == null)
            tpInput = GetComponent<vThirdPersonInput>();
        lockonmanager = GetComponent<vLockOn>();
        CurrentCameraSide = CameraAngle.RightAngle;
        Invoke("LockoN", 1.0F);
        
      
    }

    public void LockoN()
    {
        lockonmanager.StartLockOn();
    }

    public void ChangeCameraAngle()
    {
        if (CurrentCameraSide == CameraAngle.RightAngle)
        {
            tpInput.ChangeCameraState("LeftStrafing");
            CurrentCameraSide = CameraAngle.LeftAngle;

        }
        else
        {
            tpInput.ResetCameraState();
            CurrentCameraSide = CameraAngle.RightAngle;
        }


    }

    public void Exhausted()                 // When your char runs out of stamina
    {
        anim.SetTrigger("Tired");
    }
    

    public void LockInput()
    {
        print("LockInpiut");
        GetComponent<vMeleeCombatInput>().SetLockMeleeInput(true);
    }
    public void UnLockInput()
    {
        print("Unlock");
        GetComponent<vMeleeCombatInput>().SetLockMeleeInput(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        {
            print("grab");
            anim.SetTrigger("Tired");
        }


        if (Input.GetKeyDown(KeyCode.F))
            ChangeCameraAngle();

        if (Input.GetKeyDown(KeyCode.B))
            GetComponent<Animator>().SetTrigger("Tired");
        if(cc.IsAnimatorTag("Taunt"))
        {
            GetComponent<vMeleeCombatInput>().lockInput = true;
        }


    }
}
