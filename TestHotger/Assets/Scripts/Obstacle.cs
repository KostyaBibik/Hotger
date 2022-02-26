using System.Collections;
using QuickPool;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;
    [SerializeField] private float speedParallax = 10f;
    
    void Start()
    {
        StartCoroutine(nameof(WaitTimeToDestroy));
    }

    private IEnumerator WaitTimeToDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        if (gameObject)
        {
            gameObject.Despawn();
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * (speedParallax * Time.deltaTime));
    }
    
    public void AddSpeed(float speed)
    {
        speedParallax += speed;
    }

    public void SetSpeed(float speed)
    {
        speedParallax = speed;
    }

    public float GetSpeed()
    {
        return speedParallax;
    }
}
