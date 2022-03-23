using System.Runtime.InteropServices;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Normalforce
    {
        public static Vector2 Calculatenf(Vector2 velocity, Vector2 normal)
        {
            
            if (Vector2.Dot(velocity, normal) > 0f)
            {
                return Vector2.zero;
            }
            Vector2 projection = Vector2.Dot(velocity, normal) * normal;
            
            return -projection;
        }
    }
}