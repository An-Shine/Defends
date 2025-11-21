using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPosAuto : MonoBehaviour
{
    [SerializeField] Vector3 distance = Vector3.down * 40f; //위치 지정
    Transform targetTransform;  //슬라이더가 붙어야할 대상

    RectTransform rectTransform;    // 슬라이더의 rt

    public void Setup(Transform target)
    {
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        //실제 표시할 좌표 계산
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //지정한 위치만큼 떨어져서 붙이기
        rectTransform.position = screenPosition + distance;
    }
}
