using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTRack : MonoBehaviour
{
    public bool ikActive = false;
    Animator anim;
    public Transform objTransform;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
      //  objTransform = GameObject.FindGameObjectWithTag("LookAt").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK()
    {
        if(ikActive)
        {
            if(objTransform !=null)
            {
                anim.SetLookAtWeight(1);
                anim.SetLookAtPosition(objTransform.position);

            }
        }
        else
        {
            anim.SetLookAtWeight(0);
        }
    }
}
