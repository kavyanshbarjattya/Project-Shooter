using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _movementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float Horizontal = _joystick.Horizontal;
        float Vertical = _joystick.Vertical;

        if (Horizontal != 0 || Vertical != 0)
        {
            transform.Translate(new Vector2(Horizontal, Vertical) * _movementSpeed * Time.deltaTime);     
        }
    }
}
