using Com.MyCompany.MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DeathTimer());
    }

    void Update()
    {
        rb.velocity = transform.forward * 20;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth l_hitHealth))
        {
            //Change enemy Health
        }
        else
        {
            //Play explosion particle effect
            Destroy(gameObject);
        }
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
