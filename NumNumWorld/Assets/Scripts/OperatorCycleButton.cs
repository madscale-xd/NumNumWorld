using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OperatorCycleButton : MonoBehaviour
{
    [Header("UI References")]
    public Button cycleButton;                  // Assign your Button component here
    public TextMeshProUGUI operatorText;        // Assign the TextMeshProUGUI component here

    private readonly string[] operators = { "+", "-", "×", "÷" };
    private int currentIndex = 0;

    void Start()
    {
        if (cycleButton == null || operatorText == null)
        {
            Debug.LogError("OperatorCycleButton: Button or TextMeshProUGUI reference is missing.");
            return;
        }

        // Initialize the button text
        operatorText.text = operators[currentIndex];

        // Add listener to handle button clicks
        cycleButton.onClick.AddListener(CycleOperator);
    }

    void CycleOperator()
    {
        currentIndex = (currentIndex + 1) % operators.Length;
        operatorText.text = operators[currentIndex];
    }

    public string GetCurrentOperator()
    {
        return operators[currentIndex];
    }

    public void SetOperator(string op)
    {
        for (int i = 0; i < operators.Length; i++)
        {
            if (operators[i] == op)
            {
                currentIndex = i;
                operatorText.text = operators[currentIndex];
                return;
            }
        }

        Debug.LogWarning("OperatorCycleButton: Invalid operator passed to SetOperator: " + op);
    }
}
