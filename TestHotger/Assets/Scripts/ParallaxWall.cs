using UnityEngine;

public class ParallaxWall : MonoBehaviour
{
    [SerializeField] private float speedParallax = 5f;
    [SerializeField] private float startX;
    [SerializeField] private float endX;
    
    private float _startSpeed;

    private void Start()
    {
        _startSpeed = speedParallax;
    }

    public void SetDefaultValues()
    {
        if(_startSpeed != 0)
        {
            speedParallax = _startSpeed;
        }
    }

    public void AddSpeedParallax(float speed)
    {
        speedParallax += speed;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.left * (speedParallax * Time.deltaTime));

        if (transform.position.x <= endX)
        {
            var position = transform.position;
            transform.position = new Vector3(startX, position.y, position.z);
        }
    }
}
