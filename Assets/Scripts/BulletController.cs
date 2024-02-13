using Com.MyCompany.MyGame;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float _lifeTime = 10;
    [Header("Particles")]
    [SerializeField] private GameObject _propultionParticles;
    [SerializeField] private GameObject _explosionParticles;
    [SerializeField] private List<Renderer> _renderers = new List<Renderer>();
    private bool _isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DeathTimer());
        _propultionParticles.SetActive(true);
        _explosionParticles.SetActive(false);
    }

    void Update()
    {
        if (!_isDead)
            rb.velocity = transform.forward * 30;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _explosionParticles.SetActive(true);
        _propultionParticles.SetActive(false);
        rb.isKinematic = true;
        _isDead = true;
        for (int i = 0; i < _renderers.Count; i++)
            _renderers[i].enabled = false;  
        Destroy(gameObject, 0.65f);
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
