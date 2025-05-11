using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3 initialPosition;
    private float currentShakeTime = 0f;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeTime > 0)
        {
            transform.localPosition = initialPosition + (Vector3)(Random.insideUnitCircle * shakeMagnitude);
            currentShakeTime -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }

    public void Shake()
    {
        currentShakeTime = shakeDuration;
    }
    public void ShakeImpact(float duration , float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
