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
    BoxCollider2D collider; // spelarens 2dcollider
    float acceleration; // spelarens acceleration
    private float colliderMargin = 0.05f; // hur långt det måste vara mellan spelaren och andra colliders
    private float groundCheckDistance = 0.1f; // hur långt ner man kollar ifall man är grounded
    [SerializeField] LayerMask collisionMask; // vilket layer spelaren ska kollidera med
    [SerializeField] private float gravity = 0.05f;
    private Vector2 velocity; // hastighet
    private float maxSpeed = 0.08f; // maxspeed

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        acceleration = 1.0f; // sätter till önskad movementhastighet
    }

    void Update()
    {
        Vector2 downMovement = (Vector3) Vector2.down * gravity * Time.deltaTime;

        velocity += downMovement; // adderar gravitation till vektorn
        if (Input.GetKeyDown(KeyCode.Space) &&
            Grounded()) // om spelaren klickar mellanslag och är tillräckligt nära marken (groundCheckDistance)
        {
            velocity += Vector2.up * 0.2f; // adderar potentiellt hopp till vektorn
        }

        HandleInput(); // ser till att rasmus äter ägg
        UpdateVelocity();
        MovePlayer();
    }


    /**
     * Hanterar användarens input i X-led.
     * Accelererar ifall inputs magnitud är större än float.Epsilon, annars deaccelererar man
     */
    void HandleInput()
    {
        Vector2 input = Vector2.right * Input.GetAxisRaw("Horizontal");
        if (input.magnitude > float.Epsilon)
        {
            Accelerate(input);
        }
        else
        {
            if (Grounded())
            {
                Decelerate();
            }
        }
    }

    /**
     * Accelererar i den riktning spelaren har klickat i X-led
     * Kommer att sätta en standardiserad hastighet ifall maxSpeed har nåtts.
     */
    void Accelerate(Vector2 input)
    {
        velocity += input * acceleration * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
    }

    /**
     * Deaccelererar i det fall man inte hittar någon input från spelaren i X-led
     */
    void Decelerate()
    {
        float deceleration = 0.04f;
        Vector2 projection = new Vector2(velocity.x, 0.0f).normalized;

        if (deceleration > Math.Abs(velocity.x))
        {
            velocity.x = 0;
        }
        else
            velocity -= projection * deceleration * Time.deltaTime;
    }

    /**
     * Räknar ut en vector för hur spelarobjektet ska röra sig efter att alla rörelseinputs har lagts till.
     * 
     * @param movement innehåller totala påverkan av rörelseinputs
     * @returns en vector innehållandes resultatet på det som skickats in och hur spelarobjektet ska förflyttas
     */
    void UpdateVelocity()
    {
        RaycastHit2D hit = Physics2D.BoxCast( // kikar efter en kollision
            transform.position,
            collider.size,
            0.0f,
            velocity.normalized,
            velocity.magnitude + colliderMargin,
            collisionMask);
        if (!hit)
        {
            return;
        }

        do // kör så länge en kollision upptäcks 
        {
            if (velocity.magnitude < 0.00005f) // returnerar att movement är 0 om den nästan är 0
            {
                break;
            }

            hit = Physics2D.BoxCast(
                transform.position,
                collider.size,
                0.0f,
                velocity.normalized,
                velocity.magnitude + colliderMargin,
                collisionMask);

            RaycastHit2D normalHit = Physics2D.BoxCast(
                transform.position,
                collider.size,
                0.0f,
                -hit.normal,
                velocity.magnitude + colliderMargin,
                collisionMask);


            if (hit)
            {
                // flyttar spelarobjektet baserat på normalHit från ytan
                Vector2 counterMovement = -(Vector3) normalHit.normal *
                                          (normalHit.distance - colliderMargin);

                transform.position += (Vector3) counterMovement * Time.deltaTime;

                //räknar ut normalkraften och lägger till den i movement.
                Vector2 normalForce = (Normalforce.Calculatenf(velocity,
                    hit.normal));
                AddFriction(normalForce.magnitude);
                velocity += normalForce;
            }
        } while (hit);
    }

    void AddFriction(float normalForce)
    {
        print(velocity.magnitude);
        if (velocity.magnitude <
            normalForce * 0.4f)
        {
            velocity = Vector2.zero;
        }
        else
        {
            velocity -= velocity.normalized * normalForce *
                        0.3f;
        }
    }

    /**
     * Returnerar ifall spelaren är groundCheckDistance från marken.
     */
    bool Grounded()
    {
        return Physics2D.BoxCast(
            transform.position,
            collider.size,
            0.0f,
            Vector2.down,
            groundCheckDistance,
            collisionMask);
    }

    void MovePlayer()
    {
        transform.position += (Vector3) velocity;
    }
}