using UnityEngine;

public class CameraFollowBoundary : MonoBehaviour
{
    [SerializeField] float _cameraSpeed;
    [SerializeField] Transform _playerTrans;

    [Header("Camera Min Clamps")]
    [SerializeField] Vector2 _minClamp;

    [Header("Camera Max Clamps")]
    [SerializeField] Vector2 _maxClamp;



    private bool _canFollow;

    private void Start()
    {
        _canFollow = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canFollow = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canFollow = false;
        }
    }

    private void Update()
    {
        if (_canFollow || _playerTrans.position.x != transform.position.x || _playerTrans.position.y != transform.position.y)
        {
            Vector3 _targetPos = new Vector3(_playerTrans.position.x,_playerTrans.position.y,transform.position.z);  
            transform.position = Vector3.Lerp(transform.position, _targetPos, _cameraSpeed * Time.deltaTime);
        }

        float _targetX = Mathf.Clamp(transform.position.x, _minClamp.x, _maxClamp.x);
        float _targetY = Mathf.Clamp(transform.position.y, _minClamp.y, _maxClamp.y);

        transform.position = new Vector3(_targetX,_targetY,transform.position.z);
    }

}
