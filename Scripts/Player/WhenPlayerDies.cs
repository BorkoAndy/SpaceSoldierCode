
using UnityEngine;

public class WhenPlayerDies : MonoBehaviour
{
    private void OnEnable() => PlayerStats.OnPlayerDeath += Inactivate;
    private void OnDisable() => PlayerStats.OnPlayerDeath -= Inactivate;
    private void Inactivate() => gameObject.SetActive(false);
}
