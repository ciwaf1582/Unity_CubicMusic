using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("# �� Ȯ�� #")]
    public static bool canPressKey = true;

    [Header("# ť�� �̵� #")]
    public float moveSpeed = 3;
    Vector3 dir = new Vector3(); // ����
    public Vector3 destPos = new Vector3(); // ��ġ
    Vector3 originPos = new Vector3(); // ������ ��ġ

    [Header("# ť�� ȸ�� #")]
    public float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    [Header("# ť�� ����̴� ���� #")]
    public float recoilPosY = 0.25f;
    public float recoilSpeed = 1.5f;

    bool canMove = true;
    bool isFalling = false;

    [Header("# ť�� ȸ�� ������Ʈ #")]
    public Transform fakeCube; // ����ũ ť�갡 ���� ���� �� ���� destRot������ ��ȯ
    public Transform realCube;

    TimeManager timeManager;
    CameraController cameraController;
    StatusManager statusManager;
    Rigidbody rigid;
    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        cameraController = FindObjectOfType<CameraController>();
        statusManager = FindObjectOfType<StatusManager>();
        rigid = GetComponentInChildren<Rigidbody>(); // �ڽ� ��ü�� ����
        originPos = transform.position;
    }
    public void Initialized()
    {
        transform.position = Vector3.zero;
        destPos = Vector3.zero;
        realCube.localPosition = Vector3.zero;
        canMove = true;
        canPressKey = true;
        isFalling = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;
    }
    private void Update()
    {
        if (GameManager.instance.isStartGame)
        {
            CheckFalling();
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                //                            ���� Ű + �¿� Ű ���� �Է� ����
                if (!isFalling && canMove && !(Input.GetAxisRaw("Vertical") != 0 && 
                                               Input.GetAxisRaw("Horizontal") != 0) && 
                                               canPressKey)
                {
                    Calc();
                    if (timeManager.CheckTiming())
                    {
                        StartAction();
                    }
                }
            }
        }
    }
    void Calc()
    {
        // ���� ���
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        // �̵� ��ǥ�� ���
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // ȸ�� ��ǥ�� ���
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        // RotateAround : ���� ���, ȸ�� ��, ȸ�� ���� �̿��� ȸ�� ����
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
        // Distance : SqrMagnitude - SqrMagnitude�� �� �� ������(����)
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            // MoveTowards : Vector3 �Լ�, �� ��ġ���� �ٸ� ��ġ�� ������ �ӵ��� �̵�
            // MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;  
        }
        transform.position = destPos;
        canMove = true;
    }
    IEnumerator SpinCo()
    {
        // Angle : �� ȸ������ ��
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
    void CheckFalling()
    {
        if (!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Falling();
            }
        }
    }
    void Falling()
    {
        isFalling = true;
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }
    public void ResetFalling()
    {
        statusManager.DecreaseHp(1);
        AudioManager.instance.PlayerSFX("Falling");

        if (!statusManager.IsDead())
        {
            isFalling = false;
            rigid.useGravity = false;
            rigid.isKinematic = true;

            // rigid�� ���� �θ� ��ü�� �������� ����, �ڽ� ��ü(�׷���)�� �߶�
            transform.position = originPos;
            realCube.localPosition = new Vector3(0, 0, 0);
        }
    }
}
