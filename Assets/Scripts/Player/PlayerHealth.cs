using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject _deathText;

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currentHealth;
    private bool _beenHit = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletController l_bullet))
        {
            if(!_beenHit)
                ChangePlayerHealth();
        }
    }

    public void ChangePlayerHealth()
    {
        _currentHealth--;
        _beenHit = true;
        StartCoroutine(HitCooldown());
        if (_currentHealth <= 0)
        {
            _deathText.SetActive(true);
        }
    }

    private IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(1);
        _beenHit = false;
    }
}
