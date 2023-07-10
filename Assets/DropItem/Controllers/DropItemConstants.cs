//定数のみのクラス

public static class DropItemConstants
{
    //落下物の加算スコア
    public const int STONEPOINT = -10000;
    public const int RAMENPOINT = 8000;
    public const int SENBEIPOINT = 30000;
    public const int SAKABINPOINT = 40000;
    public const int MISOPOINT = 30000;
   
    //落下速
    public const float STONESPEED = 0.4f;
    public const float RAMENSPEED = 0.2f;
    public const float SENBEISPEED = 0.5f;
    public const float SKABINSPEED = 0.005f;

    //加速度
    public const float STONEACC = 1.0f;
    public const float SENBEIACC = 1.0f;
    public const float RAMENACC = 1.008f;
    public const float SAKABINACC = 1.03f;

    //スポーン間隔の最小値
    public const float MINSPAN = 3.0f;
}