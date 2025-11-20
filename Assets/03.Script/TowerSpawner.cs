using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    ContactFilter2D filter; //Raycast 용 파라미터
    List<RaycastHit2D> rcList;  //Raycast 결과 저장 리스트
    void Start()
    {
        filter = new ContactFilter2D(); //파라미터생성
        rcList = new List<RaycastHit2D>();  //리스트 생성
    }

    
    void Update()
    {
        //마우스 왼쪽버튼 클릭하면
        if(Input.GetMouseButtonDown(0))
        {            
            //리스트를 클리어하고
            rcList.Clear();
            //월드포지션값을 구해서
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            //ray를 쏴서 검출되는 오브젝트 찾기
            Physics2D.Raycast(worldPosition, Vector2.zero, filter, rcList);

            //리스트를 돌면서
            foreach(var item in rcList)
            {
                if(item.transform.CompareTag("TOWER"))
                //"TOWER" 태그인 아이템이 있으면
                {
                    return;
                    //타워가 이미 있으므로 여기서 리턴
                }
            }
            
            //다시 리스트를 돌면서
            foreach(var item in rcList)
            {                
                //"TILE" 태그인 아이템이 있으면
                if(item.transform.CompareTag("TILE"))
                {
                    //그곳에 타워 건설
                    SpawnTower(item.transform);
                }
            }
        }
    }

    void SpawnTower(Transform tileTr)
    {        
        //타워프리펩으로 타워 생성
        GameObject clone = Instantiate(towerPrefab, tileTr.position, Quaternion.identity, transform);
        //타워 무기 초기화
        clone.GetComponent<TowerWeapon>().Init();

    }
}
