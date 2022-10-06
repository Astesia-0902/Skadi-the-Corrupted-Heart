using System;
using UnityEngine;

namespace Res.Scripts
{
    public class LightRotation : MonoBehaviour
    {
        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.down, 78f * Time.deltaTime);
        }
    }
}
