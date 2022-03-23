using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Move : MonoBehaviour
{
    BoxCollider2D collider;
    float movementSpeed;
    private float colliderMargin = 0.05f;
    private float groundCheckDistance = 0.1f;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] private float gravity = 1;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        movementSpeed = 3.0f; // sätter till önskad movementhastighet
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // hämtar horisontell input (A, D)
        Vector2 direction = new Vector2(horizontal, 0.0f); // Vector för vilken riktning spelaren ska gå åt
        float distance = movementSpeed * Time.deltaTime;
        Vector2 movement = (Vector3) direction * distance;
        Vector2 downMovement = (Vector3) Vector2.down * gravity * Time.deltaTime;

        bool grounded = Physics2D.BoxCast(
            transform.position,
            collider.size,
            0.0f,
            Vector2.down,
            groundCheckDistance,
            collisionMask);

        Vector2 velocity = movement; // adderar inputmovement till vektorn
        velocity += downMovement; // adderar gravitation till vektorn
        if (Input.GetKeyDown(KeyCode.Space) &&
            grounded) // om spelaren klickar mellanslag och är tillräckligt nära marken (groundCheckDistance)
        {
            velocity += Vector2.up * 1.2f; // adderar potentiellt hopp till vektorn
        }

        CollisionFunction(velocity);
        //CollisionFunction(velocity);
    }

    /**
     * Räknar ut en vector för hur spelarobjektet ska röra sig efter att alla rörelseinputs har lagts till.
     * 
     * @param movement innehåller totala påverkan av rörelseinputs
     * @returns en vector innehållandes resultatet på det som skickats in och hur spelarobjektet ska förflyttas
     */
    Vector2 CollisionFunction(Vector2 movement)
    {
        RaycastHit2D hit = Physics2D.BoxCast( // kikar efter en kollision
            transform.position,
            collider.size,
            0.0f,
            movement.normalized,
            movement.magnitude + colliderMargin,
            collisionMask);
        if (!hit)
        {
            MovePlayer(movement);
            return movement;
        }

        do // kör så länge en kollision upptäcks 
        {
            if (movement.magnitude < 0.00005f) // returnerar att movement är 0 om den nästan är 0
            {
                return Vector2.zero;
            }

            hit = Physics2D.BoxCast(
                transform.position,
                collider.size,
                0.0f,
                movement.normalized,
                movement.magnitude + colliderMargin,
                collisionMask);

            RaycastHit2D normalHit = Physics2D.BoxCast(
                transform.position,
                collider.size,
                0.0f,
                hit.normal,
                movement.magnitude,
                collisionMask);

            // flyttar spelarobjektet baserat på normalHit från ytan
            transform.position +=
                -(Vector3) normalHit.normal *
                (normalHit.distance - colliderMargin);


            // räknar ut normalkraften och lägger till den i movement.
            movement += Normalforce.Calculatenf(movement,
                hit.normal);

            // Ritar ut movement i unity
            Debug.DrawRay(transform.position, movement, Color.green, 60);
        } while (hit);

        MovePlayer(movement);
        return movement;
    }

    void MovePlayer(Vector2 movement)
    {
        transform.position += (Vector3) movement;
    }
}