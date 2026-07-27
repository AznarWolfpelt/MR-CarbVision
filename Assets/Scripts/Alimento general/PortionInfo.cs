using UnityEngine;

public class PortionInfo : MonoBehaviour
{
    public int portionIndex;

    private FoodManager manager;
    private FoodData food;

    public void Initialize(FoodManager foodManager, FoodData foodData)
    {
        manager = foodManager;
        food = foodData;
    }

    public void ShowInfo()
    {
        manager.currentFood = food;
        manager.ShowPortion(portionIndex);
    }
}