using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvSet : MonoBehaviour
{
    public GameObject resInfo;
    public Resource[] resources;

    //public AttackerAgent attackerAgent;
    public DefenderAgent defenderAgent;
    public Attacker attacker;
    public Defender defender;

    public int gameCnt;
    public int ackSucCnt;
    public int ackMonUseOutCnt;
    public int ackSucAllMonUse;
    public int ackAllMonUse;
    public int ackAllChanTimes;
    public float defAllChanResTimes;
    public float defAllChanTimes;
    public float defAllMonUse; 

    //yxw添加
    public float avgAckSucCnt; 

    public int deTimes;
    public int goodChoiceTimes;
    public bool testMode;
    public GameObject text;
    public int stradegy;

    public float allValue;
    // Start is called before the first frame update
    void Start()
    {
        resInfo = GameObject.Find("ResInfo");
        resources = new Resource[5];
        resources[0] = GameObject.Find("Resource1").GetComponent<Resource>();
        resources[1] = GameObject.Find("Resource2").GetComponent<Resource>();
        resources[2] = GameObject.Find("Resource3").GetComponent<Resource>();
        resources[3] = GameObject.Find("Resource4").GetComponent<Resource>();
        resources[4] = GameObject.Find("Resource5").GetComponent<Resource>();
        //attackerAgent = GameObject.Find("Attacker").GetComponent<AttackerAgent>();
        attacker = GameObject.Find("Attacker").GetComponent<Attacker>();
        defenderAgent = GameObject.Find("Defender").GetComponent<DefenderAgent>();
        defender = GameObject.Find("Defender").GetComponent<Defender>();
        stradegy = 0;
        gameCnt = 0;
        ackSucCnt = 0;
        ackMonUseOutCnt = 0;
        allValue = 0;
        ackSucAllMonUse = 0;
        ackAllMonUse = 0;
        defAllMonUse = 0;
        ackAllChanTimes = 0;
        defAllChanTimes = 0;
        defAllChanResTimes = 0;
        deTimes = 0;
        goodChoiceTimes = 0;

        //yxw添加
        avgAckSucCnt = 0;

        testMode = true;
        if (Util.PortInUse(5004))
        {
            testMode = false;
        }
        
        showResource();
        //Debug.Log(resources[0].en_layers[0]);
        //defenderAgent.Start();
        attacker.Start();
        defender.Start();
        if (testMode)
        {
            GameObject.Find("Mode").GetComponent<TMP_Text>().text = "Test Mode";
        }
        else
        {
            GameObject.Find("Mode").GetComponent<TMP_Text>().text = "Training Mode";
        }
        //attacker.attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        attacker.Init();
        defender.Init();
        defenderAgent.OnEpisodeBegin();
        defenderAgent.canOK = false;
        for (int i = 0; i < 5; i++)
        {
            resources[i].Init();
        }
        attacker.attack();
    }
    public void showResource()
    {
        string s = "";
        for(int i = 0; i < resources.Length; i++)
        {
            s += "Resource";
            s += (i + 1);
            s += resources[i].ToString();
            s += "\n";
        }
        //Debug.Log(Util.PortInUse(5004));
        
        resInfo.GetComponent<TMP_Text>().text = s;
    }

    public void gameOver()
    {
        gameCnt += 1;
        
        if (attacker.success)
        {
            ackSucCnt += 1;
            ackSucAllMonUse += attacker.costMoney;
            defenderAgent.AddReward(-5f);

            //yxw
            avgAckSucCnt += ackSucCnt / (float)gameCnt;

            //allValue += attackerAgent.resourceValue;
            //if (defender.moneyUseOut)
            //{
            //    ackMonUseOutCnt += 1;
            //    defenderAgent.AddReward(-2f);
            //}
        }
        else
        {
            defenderAgent.AddReward(1f);
        }
        ackAllMonUse += attacker.costMoney;
        ackAllChanTimes += attacker.changeTimes;
        defAllMonUse += defender.costMoney;
        defAllChanTimes += defender.changeTimes;
        defAllChanResTimes += defender.chanResTimes;
        deTimes += defenderAgent.deTimes;
        goodChoiceTimes += defenderAgent.goodChoiceTime;
        //Debug.Log("attacker.costMoney:" + attacker.costMoney);
        Debug.Log("game轮数:" + gameCnt + " 攻击成功率:" + ackSucCnt / (float)gameCnt + "平均攻击成功率" + avgAckSucCnt / (float)gameCnt);
        Debug.Log(" ack平均消耗" + ackAllMonUse / (float)gameCnt + " ack平均切换目标次数" + ackAllChanTimes / (float)gameCnt);
        Debug.Log( "def平均消耗能量:" + defAllMonUse / (float)gameCnt+" def平均防御:"+defAllChanTimes/(float)gameCnt + " def平均切换:" + defAllChanResTimes / (float)gameCnt);
        //Debug.Log("def正确选择率:" + goodChoiceTimes / (float)deTimes);
        //attackerAgent.OnEpisodeBegin();
        Init();
        showResource();
    }
}
