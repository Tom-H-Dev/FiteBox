using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.MyCompany.MyGame
{
    public class PlayerHealth : MonoBehaviourPunCallbacks
    {
        [Tooltip("The current Health of our player")]

        public int Health = 3;

        private void OnCollisionEnter(Collision collision)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out BulletController l_bullet))
            {
                ChangeHealth(1);
            }
        }

        public void ChangeHealth(int l_healthChange)
        {
            if (photonView.IsMine)
            {
                if (Health <= l_healthChange)
                {
                    Debug.Log("Dead");
                    //GameManager.instance.LeaveRoom();
                }
                else if (Health > l_healthChange)
                {
                    Health += l_healthChange;
                }
            }
        }
    }
}
