using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected float currentMana;
    protected readonly float maxMana = 10;
    protected List<Unit> towers;
    public float CurrentMana { get => currentMana; set => currentMana = value; }
    public List<Unit> Towers { get => towers; set => towers = value; }

    public abstract void UpdateMana();

    public void MoveIndexToEnd(List<GameObject>units, int selectedIndex)
    {
        if (selectedIndex >= 0 && selectedIndex < units.Count)
        {
            GameObject selectedUnit = units[selectedIndex];
            units.RemoveAt(selectedIndex);
            units.Add(selectedUnit);
        }
    }

    public void DecreaseMana(int unitMana)
    {
        if (currentMana >= unitMana)
        {
            currentMana -= unitMana;
        }
    }

}
