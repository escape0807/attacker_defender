using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefOk : MonoBehaviour
{
    public Button button;
    public Defender defender;
    // Start is called before the first frame update
    void Start()
    {
        defender = GameObject.Find("Defender").GetComponent<Defender>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        //Debug.Log("-----");
        defender.setParams(setType:0);
        defender.defend();
        //Debug.Log("----");
        //Debug.Log(attacker.chooseToAttack + " " + attacker.chooseUtil);
    }
}
