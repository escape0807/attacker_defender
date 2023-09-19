using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AckOk : MonoBehaviour
{
    public Button button;
    public Attacker attacker;
    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        attacker = GameObject.Find("Attacker").GetComponent<Attacker>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        if (!isPressed)
        {
            //Debug.Log("-----");
            isPressed = true;
            //attacker.setParams();
            attacker.attack();
            //Debug.Log("----");
            //Debug.Log(attacker.chooseToAttack + " " + attacker.chooseUtil);
        }

    }
}
