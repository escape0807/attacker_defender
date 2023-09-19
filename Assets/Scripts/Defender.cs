using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Defender : MonoBehaviour
{
    
    //选择的即将防御的资源编号, 1 2 3 4 5
    public int chooseToDefend;
    public int choEnId;
    public int chaToEnId;
    public Resource resource;

    public int lastResource;

    public EnvSet envSet;
    public GameObject defEnergyInfo;
    public DefenderAgent defenderAgent;
    public Attacker attacker;

    public int deTimes;
    public int goodChoiceTime;

    public float initMoney;
    public float costMoney;
    public float restMoney;
    public float changeTimes;
    public float chanResTimes;
    //public AttackerAgent attackerAgent;
    public bool moneyUseOut;
    public bool reDefend;
    public bool a;
    // Start is called before the first frame update
    public void Start()
    {
        envSet = GameObject.Find("Environment").GetComponent<EnvSet>();
        defEnergyInfo = GameObject.Find("DefEnergyInfo");
        defenderAgent = GameObject.Find("Defender").GetComponent<DefenderAgent>();
        attacker = GameObject.Find("Attacker").GetComponent<Attacker>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        initMoney = 6;
        costMoney = 0;
        restMoney = 6;
        chanResTimes = 0;
        lastResource = -1;
        changeTimes = 0;
        a = false;
        moneyUseOut = false;
        reDefend = false;
        deTimes=0;
        goodChoiceTime=0;
        showEnergy();
    }

    /// <summary>
    /// 设置防御者进行防御的相关参数，包括防御资源的id，用于切换的算法id
    ///  setType表示选择的策略,0,1,2分别为RL，随机，贪心策略
    /// </summary>
    public void setParams(int setType = 0,int resourceId = 0,int chaEnId=0)
    {
        if (setType == 0)
        {
            //if (resourceId != 0 && chaEnId != 0)
            //{
            chooseToDefend = resourceId+1;
            string resName = "Resource" + chooseToDefend;
            resource = GameObject.Find(resName).GetComponent<Resource>();
            choEnId = Util.findFirst(resource.en_layers);
            chaToEnId = chaEnId+1;
        }else if (setType == 1)
        {
            //随机
            chooseToDefend = new Random().Next(1, 6);
            //被更换的算法id
            string resName = "Resource" + chooseToDefend;
            resource = GameObject.Find(resName).GetComponent<Resource>();
            choEnId = Util.findFirst(resource.en_layers);
            do
            {
                
                //更换后的算法id
                chaToEnId = new Random().Next(1, 7);
            } while (choEnId == chaToEnId);
            
        }else if (setType == 2)
        {
            //̰贪心
            chooseToDefend = attacker.chooseToAttack;
            choEnId = attacker.nextCTA;
            chaToEnId = new Random().Next(1, 7);
        }
        
        
    }

    /// <summary>
    ///  防御操作的具体实现
    /// </summary>
    public void defend()
    {
        string resName = "Resource" + chooseToDefend;
        resource = GameObject.Find(resName).GetComponent<Resource>();
        
        if (restMoney <= 0)
        {
            moneyUseOut = true;
            return;
        }
        if (chaToEnId == choEnId)
        {
            reDefend = true;
            return;
        }
        if (chooseToDefend!=lastResource)
        {
            chanResTimes += 1f;
        }
        lastResource = chooseToDefend;
        //if (attacker.goodChoice == false)
        //{
        //    defenderAgent.AddReward(-1f);
        //}
        //切换工具成本
        if (choEnId != chaToEnId)
        {
            costMoney += 2f;
            restMoney -= 2;
            changeTimes += 1f;
        }

        //防御操作

        //if (resource.ackTimes > 0)
        //{
        //    defenderAgent.AddReward(1f);
        //}
        deTimes++;
        if (chooseToDefend == attacker.chooseToAttack)
        {
            defenderAgent.AddReward(1f);
        }
        changeResource();
        envSet.showResource();
        showEnergy();
        if (restMoney == 0)
        {
            //能量消耗完
            moneyUseOut = true;
            //defenderAgent.AddReward(-2f);
            return;
        }
        
    }

    /// <summary>
    /// 切换加密层的算法id
    /// </summary>
    public void changeResource()
    {
        for(int i = 0; i < resource.en_layers.Length; i++)
        {
            if (resource.en_layers[i] == choEnId)
            {
                resource.en_layers[i] = chaToEnId;
                return;
            }
        }
        //if (choEnId != chaToEnId)
        //{
        //    costMoney -= 2;
        //    restMoney += 2;
        //    changeTimes -= 1;
        //}
        return;
    }

    public void showEnergy()
    {
        string s = "E: ";
        s += restMoney;
        defEnergyInfo.GetComponent<TMP_Text>().text = s;
    }
}
