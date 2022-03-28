using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovePlatform : MonoBehaviour
    {
        private BoxCollider2D collider;
        private bool turned;
        private int timer;
        private Vector2 velocity;
        private void Update()
        {
            if (turned)
            {
                timer++;
                velocity = Vector3.right * 1.5f * Time.deltaTime; 
            }
            else
            {
                timer--;
                velocity = Vector3.left * 1.5f * Time.deltaTime; 
            }

            CheckTurn();
            transform.position += (Vector3) velocity;
        }

        void CheckTurn()
        {
            if (timer < 0)
            {
                turned = true;
            }

            if (timer > 360)
            {
                turned = false;
            }
        }
    }
}