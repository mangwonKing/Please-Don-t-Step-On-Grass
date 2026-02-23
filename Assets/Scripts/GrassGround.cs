using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrassData
{
    public int hp;
    public int maxHp;
    public int level;
    public bool isSpread;
    public float createTime;
    public float ingTime;

    //ui 표시를 위한 메서드 추가
}


public class GrassGround : MonoBehaviour
{
    bool hasGrass = false;
    GrassData grassData;

    public SpriteRenderer grassSprite;


    void Update()
    {
        if(hasGrass)
        {
            // 시간 업데이트 하기
            if(grassData !=null) //널체크
            {
                grassData.ingTime = Time.time - grassData.createTime;
                //Debug.Log("현재 생존 시간 : " + grassData.ingTime);
            }
        }
    }

    void CreateGrass()
    {
        if(hasGrass) return; //이미 있는 경우 빼기
        
        // 데이터 초기화
        grassData = new GrassData();
        InitGrassData();
        //스프라이트 교체
        grassSprite.sprite = SpriteManager.Instance.GetLVSprite(grassData.level);
        //hasGrass = true;
        hasGrass = true;
    }

    void InitGrassData()
    {
        if (grassData == null) return;
        
        grassData = new GrassData();
        grassData.hp = 50; 
        grassData.maxHp = 50; 
        grassData.level = 1;
        grassData.isSpread = false;
        grassData.createTime = Time.time;
    }

    void SpreadGrass()
    {
        // 인접 잔디구역 탐색

        // 각 구역에 잔디 생성 지시, create에서 알아서 걸러짐
    }

    void DestroyGrass()
    {
        grassData = null;

        hasGrass = false;

        grassSprite.sprite = SpriteManager.Instance.GetLVSprite(0);
    }

    void OnHit() //밟힐 경우
    {
        if (!hasGrass) return; //2중 검사

        grassData.hp -= 20; // hp를 10 감소시킴
        if(grassData.hp <= 0) //잔디 파괴
        {
            DestroyGrass();
        }
    }

    void OnHeal()
    {
        if (!hasGrass) return; // 잔디가 없는 경우 회복 불가

        grassData.hp += 10;
        if (grassData.hp > grassData.maxHp) grassData.hp = grassData.maxHp; // 최대 hp를 초과하지 않도록 제한
    }

    void GrowUPGrass()
    {
        if (!hasGrass) return;

        grassData.level += 1;
        if(grassData.level > 3) grassData.level = 3; // 최대 레벨 제한

        if (grassData.level == 2)
        {
            grassData.hp += 50; // 레벨업 시 hp 증가A
            grassData.maxHp = 100;
        }

        else if (grassData.level == 3)
        {
            grassData.hp += 100; // 레벨업 시 hp 증가A
            grassData.maxHp = 200;
        }
    }


    private void OnMouseDown()
    {
        //test 용 
        if (!hasGrass)
        {
            CreateGrass();
            Debug.Log("잔디가 생성되었습니다.");
            return;
        }
        //if (!hasGrass) return;

        OnHeal();
        Debug.Log("잔디가 회복되었습니다. 현재 HP: " + grassData.hp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // npc 태그 검사
        if(hasGrass && collision.CompareTag("NPC"))
        {
            //onHit() 함수 호출
            OnHit();
            Debug.Log("잔디가 밟혔습니다. 현재 HP: " + grassData.hp);
        }
    }
}
