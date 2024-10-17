using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Unity.VisualScripting;
using System;
using JetBrains.Annotations;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset walk;
    public string CurrentState;
    public float movementX;
    public float movementY;
    private Rigidbody2D rb;
    public string CurrentAnimation;
    public PlayerStats playerStart;
    public bool isFliped;

    
  

    void Start()
    { 
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        rb = GetComponent<Rigidbody2D>();
        CurrentState = "Idle";;
    }

    void Update()
    {       
        Move();
        
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop,float timeScale)
    {   
        if(animation.name.Equals(CurrentAnimation)) {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        CurrentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle")) {
            SetAnimation(idle, true, 1f);
        }else if (state.Equals("Walking"))
        {
            SetAnimation(walk,true, 1.5f);
        }
    }

    public void Move()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");
        
        
        Vector2 movement = new Vector2(movementX , movementY) ;
        movement = movement.normalized * playerStart.MovementSpeed ;
        rb.velocity = movement * playerStart.MovementSpeed;

        if(movementX != 0 || movementY != 0)
        {
            SetCharacterState("Walking");
            if(movementX > 0  )
            {
                skeletonAnimation.skeleton.ScaleX = 1;
                isFliped = false;

           

            }
            else if(movementX < 0)
            {
                skeletonAnimation.skeleton.ScaleX= -1;
                isFliped = true;
           
            }
            
        }
        else 
        {
            SetCharacterState("Idle");
        }
        
        
        
    }

 }
    
        
    

