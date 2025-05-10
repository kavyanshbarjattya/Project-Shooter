using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 10f;

    private float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }
}
