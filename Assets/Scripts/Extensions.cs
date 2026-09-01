using DG.Tweening;
using UnityEngine;

public static class Extensions
{
      public static void MoveTo(this Transform target, Vector3 pos)
    {
        target.DOMove(pos, 0.5f).SetEase(Ease.InOutSine);
    }
}