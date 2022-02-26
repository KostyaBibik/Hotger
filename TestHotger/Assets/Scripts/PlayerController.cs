using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speedY = 2f;
    public event Action DiePlayer;
    
    private Vector3 _direction;

    private void Start()
    {
        SetDirectionMove(Vector3.down);
        DiePlayer += () =>
        {
            Destroy(gameObject);
        };
    }

    public void SetDirectionMove(Vector3 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        transform.Translate(_direction * (speedY * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DiePlayer?.Invoke();
    }
}
