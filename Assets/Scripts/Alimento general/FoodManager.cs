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

    [Header("Spawn")]
    public Transform spawnPoint;

    // Guarda la instancia actual del grupo de porciones.
    private GameObject currentFoodInstance;

    // Guarda qué porción fue colocada en el plato.
    private int currentPortion = 0;


    // Elimina el grupo de porciones actual.
    public void ClearCurrentFood()
    {
        if (currentFoodInstance != null)
        {
            Destroy(currentFoodInstance);
            currentFoodInstance = null;
        }
    }


    // Se llama al tocar un alimento, por ejemplo Arroz.
    public void SelectFood(FoodData food)
    {
        if (food == null)
        {
            Debug.LogWarning("FoodManager: se intentó seleccionar un FoodData vacío.");
            return;
        }

        currentFood = food;

        // El panel de información empieza oculto.
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }

        // Elimina las porciones del alimento anterior.
        ClearCurrentFood();

        // Crea las cuatro porciones del alimento seleccionado.
        SpawnFood();
    }


    // Se llama cuando una porción se coloca en el plato.
    // IMPORTANTE: este método ya NO crea objetos.
    // Solo muestra la información de la porción seleccionada.
    public void ShowPortion(int portionIndex)
    {
        if (currentFood == null)
        {
            Debug.LogWarning("FoodManager: no hay un alimento seleccionado.");
            return;
        }

        // Comprobar que el índice exista en los datos.
        if (currentFood.portionInfo == null ||
            portionIndex < 0 ||
            portionIndex >= currentFood.portionInfo.Length)
        {
            Debug.LogWarning(
                "FoodManager: el índice de porción " +
                portionIndex +
                " no existe en " +
                currentFood.foodName
            );

            return;
        }

        currentPortion = portionIndex;

        // Mostrar el panel solamente después de colocar la porción.
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }

        // Actualizar nombre.
        if (titleText != null)
        {
            titleText.text = currentFood.foodName;
        }

        // Actualizar imagen.
        if (foodImage != null)
        {
            foodImage.sprite = currentFood.foodImage;
        }

        // Actualizar información de la porción.
        if (infoText != null)
        {
            infoText.text = currentFood.portionInfo[portionIndex];
        }

        // Comprobar que existan los niveles de recomendación.
        if (currentFood.portionLevels == null ||
            portionIndex >= currentFood.portionLevels.Length)
        {
            Debug.LogWarning(
                "FoodManager: falta el nivel de recomendación de la porción " +
                portionIndex +
                " en " +
                currentFood.foodName
            );

            return;
        }

        // Actualizar texto y color según el nivel.
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


    // Crea el grupo de cuatro porciones.
    public void SpawnFood()
    {
        // Evitar crear otro grupo si ya existe uno.
        if (currentFoodInstance != null)
        {
            return;
        }

        // Comprobar referencias.
        if (currentFood == null)
        {
            Debug.LogWarning("FoodManager: no hay un alimento seleccionado.");
            return;
        }

        if (currentFood.foodPrefab == null)
        {
            Debug.LogWarning(
                "FoodManager: el alimento " +
                currentFood.foodName +
                " no tiene un prefab asignado."
            );

            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogWarning(
                "FoodManager: no se asignó el Spawn Point."
            );

            return;
        }

        // Crear el prefab padre que contiene las cuatro porciones.
        currentFoodInstance =
            Instantiate(
                currentFood.foodPrefab,
                spawnPoint.position,
                spawnPoint.rotation,
                spawnPoint
            );

        // Buscar todas las porciones dentro del prefab.
        PortionInfo[] portions =
            currentFoodInstance.GetComponentsInChildren<PortionInfo>(true);

        // Dar a cada porción acceso al FoodManager y al FoodData.
        foreach (PortionInfo p in portions)
        {
            p.Initialize(this, currentFood);
        }
    }
}