using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Way : MonoBehaviour
{
     [Range(-1, 1)][SerializeField] int carDirection = 1;
     [SerializeField] bool lifter = false;
     [SerializeField] GameObject LevelUp;
     [SerializeField] CinemachineVirtualCamera camera;
     
     
     private bool carEntered = false;
     private float lifterSpeed = 5f;
     private float timer = 0;
     private CarPanretCode _car;
     
     
     private void Start()
     {
          _car = FindObjectOfType<CarPanretCode>();
     }
     
     private void OnCollisionEnter(Collision collision)
     {
          if(collision.gameObject.tag == "Car")
          {
               _car.Direction = carDirection;
               carEntered = true;
          }
     }
     private void OnCollisionExit(Collision collision)
     {
          if (collision.gameObject.tag == "Car")
          {
               _car.Direction = carDirection;
               carEntered = false;
          }
     }
     
     private void Update()
     {
          if (lifter && carEntered)
          {
               if (timer >= 1)
               {
                    carDirection = 0;
                    camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(70.0f, 20.0f, 0.0f);
                    float step = lifterSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 30.8f), step);
               }
               else
               {
                    timer += Time.deltaTime;
               }
          }

          if (transform.position.y >= 10f && carEntered && lifter)
          {
            
               _car.Direction = -1;
               StartCoroutine(LiftGoBack());
          }
          else if(transform.position.y >= 4f && !carEntered && lifter){
               StartCoroutine(LiftGoBack());
          }
     }

     IEnumerator LiftGoBack()
     {
          yield return new WaitForSeconds(10f);
          timer = 0;
          transform.position = new Vector3(transform.position.x, -15f);
     }
     
}
