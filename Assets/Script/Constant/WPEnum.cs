using System.Collections;
using System.Collections.Generic;

public class WPEnum {

    // 게임데이터 저장 타입
    public enum VaraibleType
    {
        eUserMoney = 0,                 // 돈

        eUserTerritory = 100,           // 영지 개수

        eUserWorkerCount = 200,         // 일꾼 개수
    }

	// 액터 움직입 타입
    public enum ActorMoveType
    {
        eMoveNone = 0,                  // 안 움직이는 상태
        eMoveRoaming = 1,               // 로밍. 무작위로 움직이는 상태
    }
	
	// return value 타입
	public enum rvType
	{
		eTypeSuccess = 0,				// 무난히 성공
		eTypeFail = 1,					// 치명적인 실패
	}

	// 액터키 모음. 추후 이걸 데이터로 빼는작업 필요.
	public enum ActorKey
	{
		eActorWorkerTemp = 100,			// 임시 워커
	}

	// 현재 액터의 상태를 정의해놓음.
	public enum ActorState
	{
		eActorStateNone = 0,            // 아무상태도 아님
		eActorStateIdle = 1,            // 대기상태
		eActorStateMoving = 2,			// 움직이는 상태
	}

    // 계절
    public enum Season
    {
        eSpring = 1,
        eSummer,
        eAutumn,
        eWinter
    }

    public enum Weather
    {
        eSunny,
        eDrought,
        eRain,
        eCold
    }
}
