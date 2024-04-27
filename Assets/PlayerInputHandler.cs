// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerInputHandler : MonoBehaviour
// {
//     [SerializeField] Fighter player;
//     // [SerializeField] float minXPosition = -7; // Minimum X position allowed
//     // [SerializeField] float maxXPosition = 7; // Maximum X position allowed

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Vector3 input = Vector3.zero;

//         if(Input.GetKey(KeyCode.A))
//         {
//             input.x = -1;
//         }

//         if(Input.GetKey(KeyCode.D))
//         {
//             input.x = 1;
//         }

//         if(Input.GetKeyDown(KeyCode.Space))
//         {
//             player.Jump();
//         }

//         player.MovePlayer(input);

//         // Vector3 clampedPosition = player.transform.position;
//         // clampedPosition.x = Mathf.Clamp(clampedPosition.x, minXPosition, maxXPosition);
//         // player.transform.position = clampedPosition;
//     }
// }
