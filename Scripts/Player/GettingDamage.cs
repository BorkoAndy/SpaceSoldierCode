using UnityEngine;
using UnityEngine.UI;

public class GettingDamage : MonoBehaviour
{
    [SerializeField] private Image _damageImage;
    private float _maxAlpha = 0.6f;

    private void OnEnable() => Weapon.OnEnemyAttack += ShowDamageScreen;
    private void OnDisable() => Weapon.OnEnemyAttack -= ShowDamageScreen;
    private void Update()
    {
        if (_damageImage.color.a > 0)
        {
            var color = _damageImage.color;
            color.a -= 0.01f;
            _damageImage.color = color;
        }
    }
    private void ShowDamageScreen(float damage)
    {       
        Debug.Log(_damageImage.color.a.ToString());
       var color = _damageImage.color;
        color.a = _maxAlpha;
        _damageImage.color = color;       
    }
}
