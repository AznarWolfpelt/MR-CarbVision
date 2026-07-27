using UnityEngine;

public class PortionInfo : MonoBehaviour
{
    public int portionIndex;

    private FoodManager manager;
    private FoodData food;
    public FloatingFoodLabel floatingLabel;

    void Awake()
    {
        floatingLabel = GetComponentInChildren<FloatingFoodLabel>();
    }

    public void Initialize(FoodManager foodManager, FoodData foodData)
    {
        manager = foodManager;
        food = foodData;

        if (floatingLabel != null)
        {
            floatingLabel.Setup(
                food.foodName,
                food.portionDisplayNames[portionIndex],
                food.portionWeights[portionIndex]
            );
        }
    }

    public void ShowInfo()
    {
        manager.currentFood = food;
        manager.ShowPortion(portionIndex);
    }
}