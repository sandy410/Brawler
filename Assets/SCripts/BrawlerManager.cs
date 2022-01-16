using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCamera;
using Invector.vCharacterController;
using Invector.vCharacterController.vActions;
using Invector.vCharacterController.AI;

public class BrawlerManager : MonoBehaviour
{
   
   public enum CameraAngle {RightAngle, LeftAngle };
    CameraAngle CurrentCameraSide;
  
    public vThirdPersonInput tpInput;
    vLockOn lockonmanager;
    Animator anim;
    vThirdPersonController cc;
    public Collider GrabCollider;
    public string GrabAttackAnim;
    public string GrabVictimAnim;
    public int GrabDamage=20;
    GameObject enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<vThirdPersonController>();
        enemyAI = GameObject.FindGameObjectWithTag("Enemy");
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
    private void OnDrawGizmos()
    {
        Vector3 spheretransform = transform.position + transform.forward * 1 + transform.up * 1;
        Debug.DrawLine(transform.position, spheretransform);
    }

    public void StartGrab()
    {
        GrabCollider.enabled = true;

    }



    public void PerformThrow(Collider other)
    {
        print(other.name);
        //GetComponent<vGenericAction>().TriggerActionEvents();
        //GetComponent<vGenericAction>().TriggerAnimation();
        anim.CrossFadeInFixedTime("GrabAttack", 0.1f);
        other.GetComponent<Animator>().CrossFadeInFixedTime("GrabVictim", 0.1f);

    }

    public void EndGrab()
    {
        GrabCollider.enabled = false;
        GrabCollider.GetComponent<GrabTrigger>().Cangrab = true;
        

    }

    public void DamageEnemy()
    {
        print("damage");
        var damage = new Invector.vDamage();
        damage.damageValue = GrabDamage;
        enemyAI.GetComponent<vSimpleMeleeAI_Controller>().TakeDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
       

        if(Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        {
            print("grab");
            
            anim.SetTrigger("Grab");
          //  anim.CrossFadeInFixedTime("Grab", 0.1f);
        }


        if (Input.GetKeyDown(KeyCode.F))
            ChangeCameraAngle();

        if (Input.GetKeyDown(KeyCode.B))
            GetComponent<Animator>().SetTrigger("Grab");
        if(cc.IsAnimatorTag("Taunt"))
        {
            GetComponent<vMeleeCombatInput>().lockInput = true;
        }


    }
}
