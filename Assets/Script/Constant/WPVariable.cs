using System.Collections;
using System.Collections.Generic;

public class WPVariable
{
    // 개발버전 여부 초기화 WPGameCommon::Awake()
    public static bool isDevelopment = true;

	// 현재 스크린 사이즈. 기본 1080 * 1920
	public const float screenSizeX = 1080f;
	public const float screenSizeY = 1920f;

	// 현재 가능한 필드 사이즈. WPBackGroundManager 에서 초기화 되어야할것 같다.
	public static float currentFieldSizeX = 18f;
	public static float currentFieldSizeY = 18f;

    // 현재 월드 좌표 상 가능한 필드 사이즈
    public static float currentWorldSizeX = 9f;
    public static float currentWorldSizeY = 15f;

    // WPDateTime.Now가 흐르는 시간 배율
    public static float deltaTime_WPDateTime = 1f;
}
