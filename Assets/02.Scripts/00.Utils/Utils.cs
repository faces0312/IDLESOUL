using UnityEngine;

public static class Utils
{
    // 각종 유틸 메서드 정리

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    public static readonly int POOL_KEY_PLAYERPROJECTILE = 10;

    public static float Percent(float current, float max)
    {
        return current != 0 && max != 0 ? current / max : 0;
    }

}
