using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingCharacterScript : MonoBehaviour
{   
    
    public GameObject characterOBJ;
    Animator controllerANIM;

    public bool isBlocking;

    // Start is called before the first frame update
    void Start()
    {
        controllerANIM = characterOBJ.GetComponent<Animator>();
        isBlocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    [ContextMenu("Attack")]
    public void Attack()
    {
        controllerANIM.SetTrigger("Punching");
    }
       
    public void SetBlocking()
    {

    }

  }
    

    

    
    
    
    

    

