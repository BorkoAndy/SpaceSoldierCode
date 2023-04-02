using UnityEngine;


public class GameEndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _looseText;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += LooseGame;
        Boss.OnBossDeath += WinGame;

    }
    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= LooseGame;
        Boss.OnBossDeath -= WinGame;
    }
    private void Start()
    {
        _looseText.SetActive(false);
        _winText.SetActive(false);
    }
    private void LooseGame() => _looseText.SetActive(true);
    private void WinGame() => _winText.SetActive(true);
}
