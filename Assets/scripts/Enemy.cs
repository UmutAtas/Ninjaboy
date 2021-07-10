using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
   private IEnemyState currentState;
   public GameObject Target { get; set; }
   public float meleeRange;

   public bool InMeleeRange
   {
      get
      {
         if (Target != null)
         {
            return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
         }

         return false;
      }
   }

   public override void Start()
   {
      base.Start();
      ChangeState((new IdleState()));
   }

   private void LookAtTarget()
   {
      if (Target != null)
      {
         float xDirection = Target.transform.position.x - transform.position.x;
         if (xDirection < 0 && facingRight || xDirection >0 && !facingRight)
         {
            ChangeDirection();
         }
      }
   }

   private void Update()
   {
      currentState.Execute();
      LookAtTarget();
   }

   public void ChangeState(IEnemyState newState)
   {
      if (currentState != null)
      {
         currentState.Exit();
      }
      currentState = newState;
      currentState.Enter(this);
   }

   public void Move()
   {
      if (!Attack)
      {
         MyAnimator.SetFloat("speed",1);
         transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
      }
   }

   public Vector2 GetDirection()
   {
      return facingRight ? Vector2.right : Vector2.left;
   }
}
