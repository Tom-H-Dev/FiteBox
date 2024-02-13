using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject _deathText;

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletController l_bullet))
        {
            ChangePlayerHealth();
        }
    }

    public void ChangePlayerHealth()
    {
        if(_currentHealth <= 1)
            _deathText.SetActive(true);
        else _currentHealth--;
        
    }
}
