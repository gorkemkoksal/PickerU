using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thrower : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    [SerializeField] GameObject _wing;
    [SerializeField] float _numberOfThrows;
    [SerializeField] Vector3 _RotateVector;
    [SerializeField] float _deltaTimeBtwThrows = 0.15f;

    private ObjectPool _objectPool;
    private Animator _animator;

    private readonly int FlyHash1 = Animator.StringToHash("Fly1");
    private readonly int FlyHash2 = Animator.StringToHash("Fly2");
    private readonly int FlyHash3 = Animator.StringToHash("Fly3");
    private const float CrossFadeDuration = 0.1f;

    void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
        _animator = GetComponent<Animator>();

    }
    private void Update()
    {
        _wing.transform.Rotate(_RotateVector * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }

        if (SceneManager.GetActiveScene().name == "1")
        {
            _animator.CrossFadeInFixedTime(FlyHash1, CrossFadeDuration);
        }
        else if (SceneManager.GetActiveScene().name == "2")
        {
            _animator.CrossFadeInFixedTime(FlyHash2, CrossFadeDuration);
        }
        else
        {
            _animator.CrossFadeInFixedTime(FlyHash3, CrossFadeDuration);
        }
        StartCoroutine(ThrowingShapes());
    }
    IEnumerator ThrowingShapes()
    {
        for (int i = 0; i < _numberOfThrows; i++)
        {
            GameObject obj = _objectPool?.GetPooledObject();
            if (obj != null)
            {
                obj.transform.position = _spawnPoint.position;
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(_deltaTimeBtwThrows);
        }
    }
}
