

using UnityEngine;

public class Jump : State
{
    private PlayerController controller;
    private bool hasJumped;
    private float cooldown;

    public Jump(PlayerController controller) : base("Idle")
    {
        this.controller = controller;
    }
    

    public override void Enter()
    {
        base.Enter();

        hasJumped = false;
        cooldown = 0.5f;
        
        controller.thisAnimator.SetBool("bJumping", true);
    }

    public override void Exit()
    {
        base.Exit();
        controller.thisAnimator.SetBool("bJumping", false);
    }

    public override void Update()
    {
        base.Update();

        cooldown -= Time.deltaTime;

        if (hasJumped && controller.isGrounded)
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }
        
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!hasJumped)
        {
            hasJumped = true;
            Applyimpulse();
        }
        Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        walkVector = controller.GetForward() * walkVector;
        walkVector *= controller.movementSpeed * controller.jumpMovementFactor;

        
        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

        controller.RotateBodyToFaceInput();
    }

        
    

    private void Applyimpulse() {
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.thisRigidbody.AddForce(forceVector, ForceMode.Impulse);
    }


}
