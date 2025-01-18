using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    private Button button;
    public static bool isButtonClicked = false;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        button.gameObject.SetActive(true);
    }
    void Update()
    {
    }
    void OnButtonClick()
    {
        if (!isButtonClicked)
        {
            isButtonClicked = true;
            button.gameObject.SetActive(false);
        }
    }
    public void ShowButton()
    {
        button.gameObject.SetActive(true);
        isButtonClicked = false;
    }
}
