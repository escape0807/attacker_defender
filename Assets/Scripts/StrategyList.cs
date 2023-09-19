using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StrategyList : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public EnvSet envSet;

    private void Start()
    {
        envSet = GameObject.Find("Environment").GetComponent<EnvSet>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                envSet.stradegy = 0;
                break;
            case 1:
                envSet.stradegy = 1;
                break;
            case 2:
                envSet.stradegy = 2;
                break;
            case 3:
                envSet.stradegy = 3;
                break;
        }
        envSet.Init();
        dropdown.interactable = false;
    }
}
