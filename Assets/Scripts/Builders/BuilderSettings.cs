using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuilderSettings
{

    public static float[] percentBehavior = new float[] { 25, 25, 25, 25 };

    /// <summary>
    /// Получаем случайное поведение на основании заданных процентов
    /// </summary>
    /// <returns></returns>
    public static FigureBehavior GetRandomBehavior()
    {
        float value = Random.Range(0, 100);
        for (int i = 0; i < percentBehavior.Length; i++)
        {
            value -= percentBehavior[i];
            if (value <= 0)
                return (FigureBehavior)i;
        }
        return (FigureBehavior)percentBehavior.Length - 1;
    }

    /// <summary>
    /// При изменении одного процента меняем остальные, чтобы всегда сохранялось 100%
    /// </summary>
    public static void ChangePercent(int index, float value)
    {
        float delta = value - percentBehavior[index];
        percentBehavior[index] = value;
        float sum = 0;
        for (int i = 0; i < percentBehavior.Length; i++)
            if (i != index)
                sum += percentBehavior[i];
        for (int i = 0; i < percentBehavior.Length; i++)
            if (i != index)
                percentBehavior[i] -= delta * (percentBehavior[i] / sum);
    }
}