using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = _joystick.Horizontal;
        float Vertical = _joystick.Vertical;
        float Movement = new Vector2(Horizontal, Vertical).magnitude;
        
        transform.rotation = Quaternion.Euler(Vector3.forward * Movement * _rotationSpeed * Time.deltaTime);
    }
}
