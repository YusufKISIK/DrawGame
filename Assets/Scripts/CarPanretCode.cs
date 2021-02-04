using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarPanretCode : MonoBehaviour
{
    
    [SerializeField] GameObject wheelPrefab;
    [SerializeField] LineRenderer lineRendererPrefab;
    [SerializeField] CinemachineVirtualCamera camera;
    
    float movementSpeed = 10f;
    private LineRenderer _lineRenderer;
    private List<Vector3> _linePositions;
    private GameObject _wheelLeft;
    private GameObject _wheelRight;
    private Vector3 _previousPosition;
    private GameObject _checkPoint;
    private float _timer = 0f;
    public int direction = 1;
    public float gravityScale = 1.0f;


 
    void Start()
    {
        _previousPosition = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        if (_wheelLeft != null)
        {
            _previousPosition = _wheelLeft.transform.position;
            MoveCar();
            Timer();
        }
    }
    


    public void DrawingCar(List<Vector3> linePositions)
    {
        Destroy(_wheelLeft);
        Destroy(_wheelRight);
        if (_lineRenderer != null)
        {
            Destroy(_lineRenderer.gameObject);
        }

        float distanceX = _previousPosition.x - linePositions[0].x;
        float distanceY = _previousPosition.y - linePositions[0].y;
        float smallestValue = 100f;
        foreach (Vector3 position in linePositions)
        {
            if (position.y < smallestValue)
            {
                smallestValue = position.y;
            }
        } 
        smallestValue = linePositions[0].y - smallestValue;

        List<Vector3> newLinePositions = new List<Vector3>();
        for (int i = 0; i < linePositions.Count-1; i+=3)
        {
            Vector3 newPosition = new Vector3(linePositions[i].x + distanceX, linePositions[i].y + distanceY+ smallestValue);
            newLinePositions.Add(newPosition);
        }

        InstantiateArrays(newLinePositions);
        AddCollider(newLinePositions);
    }
    
    void InstantiateArrays(List<Vector3> newLinePositions)
    {
        _lineRenderer = Instantiate(lineRendererPrefab, new Vector3(0f,0f,0f), Quaternion.identity, gameObject.transform);
        _lineRenderer.positionCount = newLinePositions.Count;
        _lineRenderer.SetPositions(newLinePositions.ToArray());

        _wheelLeft = Instantiate(wheelPrefab, newLinePositions[0], Quaternion.Euler(new Vector3(0f, 90f, 0f)), gameObject.transform);
        _wheelLeft.GetComponent<FixedJoint>().connectedBody = _lineRenderer.GetComponent<Rigidbody>();

        _wheelRight = Instantiate(wheelPrefab, newLinePositions[newLinePositions.Count - 1], Quaternion.Euler(new Vector3(0f, 90f, 0f)), gameObject.transform);
        _wheelRight.GetComponent<FixedJoint>().connectedBody = _lineRenderer.GetComponent<Rigidbody>();
        
        camera.Follow = _wheelLeft.transform;
        camera.LookAt = _wheelLeft.transform;
    }
    void AddCollider(List<Vector3> newLinePositions)
    {
        for (int i = 2; i < newLinePositions.Count; i++ )
        {
            BoxCollider boxCollider = _lineRenderer.gameObject.AddComponent<BoxCollider>();
            boxCollider.center = newLinePositions[i];
            boxCollider.size = new Vector3(0.5f, 0.5f, 0.1f);
        }
    }
    void MoveCar()
    {
        if (_wheelLeft != null && _wheelRight != null)
        {
            _wheelLeft.GetComponent<Rigidbody>().AddForce(new Vector3(movementSpeed * direction * 100, movementSpeed * gravityScale) * Time.deltaTime);
            _wheelRight.GetComponent<Rigidbody>().AddForce(new Vector3(movementSpeed * direction * 100, movementSpeed * gravityScale) * Time.deltaTime);
        }
    }
    
    
    
    public void SaveToCheckPoint(GameObject checkpointPosition)
    {
        _checkPoint = checkpointPosition;
    }

    public void InstantiateCheckpoint()
    {
        Destroy(_wheelLeft);
        Destroy(_wheelRight);
        Destroy(_lineRenderer.gameObject);
        _previousPosition = _checkPoint.transform.position;
        camera.Follow = _checkPoint.transform;
        camera.LookAt = _checkPoint.transform;
    }
    
    public int Direction
    {
        set { direction = value;
            if(value == 1)
            {
                camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(70.0f, 20.0f, -30.0f);
            }
            else if(value == -1)
            {
                camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(70.0f, 20.0f, 30.0f);
            }
        }
    }
    
    void Timer()
    {
        if(Mathf.Abs(_wheelLeft.GetComponent<Rigidbody>().velocity.x) <= 0.5f || Mathf.Abs(_wheelRight.GetComponent<Rigidbody>().velocity.x) <= 0.5f)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0f;
        }
        if(_timer >= 10f)
        {
            InstantiateCheckpoint();
            _timer = 0f;
        }
    }

}
