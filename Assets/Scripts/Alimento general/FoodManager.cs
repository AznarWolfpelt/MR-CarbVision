using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject infoPanel;

    [Header("UI")]
    public TMP_Text titleText;
    public Image foodImage;
    public TMP_Text infoText;
    public TMP_Text recommendedText;
    public Image infoPanelBackground;

    [Header("Current Food")]
    public FoodData currentFood;

    [Header("Colores de Estado")]
    public Color recommendedColor = Color.green;
    public Color acceptableColor = new Color(0.4f, 0.8f, 1f);
    public Color moderateColor = Color.yellow;
    public Color notRecommendedColor = Color.red;
    
    public Transform spawnPoint;
    private GameObject currentFoodInstance;
    private int currentPortion = 0;

    public void ClearCurrentFood()
    {
        if (currentFoodInstance != null)
        {
            Destroy(currentFoodInstance);
            currentFoodInstance = null;
        }
    }

    public void SelectFood(FoodData food)
    {
        currentFood = food;

        if (infoPanel != null)
        infoPanel.SetActive(false);

        SpawnFood();
    }

    public void ShowPortion(int portionIndex)
    {
        infoPanel.SetActive(true);
        currentPortion = portionIndex;

        if (currentFoodInstance != null)
        {
            Destroy(currentFoodInstance);
        }

        currentFoodInstance = Instantiate(currentFood.foodPrefab, spawnPoint);

        PortionInfo[] portions =
            currentFoodInstance.GetComponentsInChildren<PortionInfo>();

        foreach (PortionInfo p in portions)
        {
            p.Initialize(this, currentFood);
        }

        // Actualizar UI
        if(titleText != null)
        {
            titleText.text = currentFood.foodName;
        }

        if(foodImage != null)
        {
            foodImage.sprite = currentFood.foodImage;
        }

        if(infoText != null)
        {
            infoText.text = currentFood.portionInfo[portionIndex];
        }

        switch (currentFood.portionLevels[portionIndex])
        {
            case FoodData.PortionLevel.Recommended:

                if (recommendedText != null)
                {
                    recommendedText.text = "Recomendado";
                }

                if (infoPanelBackground != null)
                {
                    infoPanelBackground.color = recommendedColor;
                }

                break;

            case FoodData.PortionLevel.Acceptable:

                if (recommendedText != null)
                {
                    recommendedText.text = "Aceptable";
                }

                if (infoPanelBackground != null)
                {
                    infoPanelBackground.color = acceptableColor;
                }

                break;

            case FoodData.PortionLevel.Moderate:

                if (recommendedText != null)
                {
                    recommendedText.text = "Moderado";
                }

                if (infoPanelBackground != null)
                {
                    infoPanelBackground.color = moderateColor;
                }

                break;

            case FoodData.PortionLevel.NotRecommended:

                if (recommendedText != null)
                {
                    recommendedText.text = "No recomendado";
                }

                if (infoPanelBackground != null)
                {
                    infoPanelBackground.color = notRecommendedColor;
                }

                break;
        }
    }
        public void SpawnFood()
    {
        if (currentFoodInstance != null)
            Destroy(currentFoodInstance);

        currentFoodInstance =
            Instantiate(currentFood.foodPrefab, spawnPoint);

        PortionInfo[] portions =
            currentFoodInstance.GetComponentsInChildren<PortionInfo>();

        foreach (PortionInfo p in portions)
        {
            p.Initialize(this, currentFood);
        }
    }
}