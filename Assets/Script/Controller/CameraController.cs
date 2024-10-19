using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 15;

    Vector3 playerDistance = new Vector3();

    float hitDistance = 0; // 히트 했을 때 줌 효과
    public float zoomDistance = -1.25f;
    private void Awake()
    {
        playerDistance = transform.position - target.position;
    }
    private void Update()
    {
        Vector3 destPos = target.position + playerDistance + (transform.forward * hitDistance);
        // Lerp(A, B, C) : A와 B사이의 값에서 C비율의 값을 추출
        // ex) mathf.Lerp(0, 10, 0.5) => 5
        transform.position = Vector3.Lerp(transform.position, destPos, followSpeed * Time.deltaTime);
    }
    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f);

        hitDistance = 0;
    }
}
