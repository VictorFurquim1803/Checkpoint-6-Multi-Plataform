using UnityEngine;
using Fusion;

[RequireComponent(typeof(CharacterController))]
public class Player : NetworkBehaviour
{
    public float speed = 6f;
    public float gravity = -20f;

    private CharacterController controller;
    private float verticalVelocity;

    [Networked]
    public int Score { get; set; }

    public override void Spawned()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Runner.DeltaTime;

        Vector3 movement = direction * speed + Vector3.up * verticalVelocity;

        controller.Move(movement * Runner.DeltaTime);

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.forward = direction;
        }
    }

    public void AddPoint()
    {
        if (Object.HasStateAuthority)
        {
            Score++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Object.HasStateAuthority)
            return;

        CrystalPickup crystal = other.GetComponentInParent<CrystalPickup>();

        if (crystal != null)
        {
            crystal.TryCollect(this);
        }
    }

    public bool HasWon()
    {
        return Score >= 5;
    }
}
