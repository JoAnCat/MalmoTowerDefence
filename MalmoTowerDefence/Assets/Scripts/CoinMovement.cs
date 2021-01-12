using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TreeEditor;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
   public delegate void Coin2Bank();

   public static event Coin2Bank OnCoin2Bank;
   private Vector3 destinationPosition;
   //public int value;
   private float acceptableDistance2Target = 0.1f;
   [SerializeField] private float speed = 2f;
   private Transform thisTransform;
   private void Start()
   {
      thisTransform = transform;
      destinationPosition = GameObject.FindWithTag("CoinDestination").transform.position;
   }

   private void Update()
   {
      thisTransform.position = Vector3.Slerp(thisTransform.position, destinationPosition, Time.deltaTime * speed);
      if (Vector3.SqrMagnitude(thisTransform.position - destinationPosition) < 0.3f)
         SendCoin2Bank();
   }

   private void SendCoin2Bank()
   {
      print("coins are close");
      OnCoin2Bank?.Invoke();
      MoneyPool.ReturnCoinFromPool(gameObject);
   }
}
