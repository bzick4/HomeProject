// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Jump : MonoBehaviour
// {
//   private CHarContr _animator;
//   private int _jump = -1;
//   private int _randomJump;

//   private void Awake()
//   {
//     _animator = GetComponent<CHarContr>();
//   }

//   private void Update()
//   {
//     //PressJump();
//   }

//   private void OnTriggerEnter(Collider other)
//   {
//     if (other.CompareTag("JumpRandom")) 
//     {
//     _randomJump = Random.Range(0, 6);
//     _animator.CharAnimation.SetInteger("JumpID", _randomJump);
//     _animator.CharAnimation.SetTrigger("JumpRand");
//     }
//      //_jump = 0;
    
//     // if (other.CompareTag("Jump2")) _jump = 2;

//     // if (other.CompareTag("Jump3")) _jump = 3;

//     Debug.Log($"{_randomJump}");
//   }

//   private void OnTriggerExit(Collider other)
//   {
//     if (other.CompareTag("JumpRandom") )// ||
//         // other.CompareTag("Jump2") ||
//         // other.CompareTag("Jump3"))
    
//       _jump = -1;
    

//     Debug.Log($"{_jump}");
//   }

//   private void PressJump()
//   {
//     if (Input.GetKeyDown(KeyCode.Space))
//     {
//       switch (_jump)
//       {
//         case 0:
//           _animator.CharAnimation.SetInteger("JumpID", _randomJump);
//           break;
//         // case 1:
//         //   _animator._anim.SetTrigger("Jump1");
//         //   break;
//         // case 2:
//         //   _animator._anim.SetTrigger("Jump2");
//         //   break;
//         // case 3:
//         //   _animator._anim.SetTrigger("Jump3");
//         //   break;
//       }
//     }
//   }
// }
