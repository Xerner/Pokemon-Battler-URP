using UnityEngine;
using KevinCastejon.EditorToolbox;

public class TestHeaderPlusAttribute : MonoBehaviour
{
    [HeaderPlus("Assets/KevinCastejon/EditorToolbox/Demos/Attributes/HeaderPlus/Icons/greencrossicon.png", "Health", (int)HeaderPlusColor.green)]
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    [HeaderPlus("Assets/KevinCastejon/EditorToolbox/Demos/Attributes/HeaderPlus/Icons/atk.png", "Attack", (int)HeaderPlusColor.red)]
    [SerializeField] private int _damagePoints;
    [SerializeField] private int _attackSpeed;
}
