using System;

public class Boss : EnemyBehaviour
{
    public static Action OnBossDeath;
    private void OnDestroy() => OnBossDeath?.Invoke();
}
