using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("# 골 확인 #")]
    public static bool canPressKey = true;

    [Header("# 큐브 이동 #")]
    public float moveSpeed = 3;
    Vector3 dir = new Vector3(); // 방향
    public Vector3 destPos = new Vector3(); // 위치

    [Header("# 큐브 회전 #")]
    public float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    [Header("# 큐브 들썩이는 정도 #")]
    public float recoilPosY = 0.25f;
    public float recoilSpeed = 1.5f;

    bool canMove = true;

    [Header("# 큐브 회전 오브젝트 #")]
    public Transform fakeCube; // 페이크 큐브가 먼저 돌고 그 값을 destRot값으로 전환
    public Transform realCube;

    TimeManager timeManager;
    CameraController cameraController;
    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        cameraController = FindObjectOfType<CameraController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            //                            상하 키 + 좌우 키 동시 입력 방지
            if (canMove && !(Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0) 
                                                               && canPressKey)
            {
                Calc();
                if (timeManager.CheckTiming())
                {
                    StartAction();
                }
            }
        }
    }
    void Calc()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        // 이동 목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // 회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        // RotateAround : 공전 대상, 회전 축, 회전 값을 이용한 회전 구현
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;
    }
    void StartAction()
    {
        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
        StartCoroutine(cameraController.ZoomCam());
    }
    IEnumerator MoveCo()
    {
        canMove = false;
        // Distance : SqrMagnitude - SqrMagnitude가 좀 더 가벼움(연산)
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            // MoveTowards : Vector3 함수, 한 위치에서 다른 위치로 일정한 속도로 이동
            // MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;  
        }
        transform.position = destPos;
        canMove = true;
    }
    IEnumerator SpinCo()
    {
        // Angle : 두 회전값의 차
        while(Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }
        realCube.rotation = destRot;
    }
    IEnumerator RecoilCo()
    {
        while(realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        while(realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        realCube.localPosition = Vector3.zero;
    }
}
