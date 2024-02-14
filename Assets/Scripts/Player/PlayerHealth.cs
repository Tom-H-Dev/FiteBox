using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject _deathText;

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currentHealth;
    private bool _beenHit = false;
    [SerializeField] private List<Material> _tankMaterials;
    private List<Color> _oldMats = new List<Color>();


    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletController l_bullet))
        {
            if (!_beenHit)
                ChangePlayerHealth();
        }
    }

    public void ChangePlayerHealth()
    {
        _currentHealth--;
        _beenHit = true;
        StartCoroutine(HitCooldown());
        StartCoroutine(MaterialFlash());
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
    private IEnumerator MaterialFlash()
    {
        for (int i = 0; i < _tankMaterials.Count; i++)
        {
            _oldMats.Add(_tankMaterials[i].color);
        }
        int temp = 0;
        while (temp < 3)
        {
            for (int i = 0; i < _tankMaterials.Count; i++)
            {
                _tankMaterials[i].color = Color.red;

            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < _tankMaterials.Count; i++)
            {
                _tankMaterials[i].color = _oldMats[i];
            }
            yield return new WaitForSeconds(0.1f);
            temp++;
        }
    }
}
