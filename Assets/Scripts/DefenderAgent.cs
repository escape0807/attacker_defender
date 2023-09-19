using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DefenderAgent : Agent
{
    public Resource[] resources;

    public Defender defender;
    public Attacker attacker;
    public int a;

    public int deTimes;
    public int goodChoiceTime;

    public float[][] layers_info;
    public bool canOK;

    public EnvSet envSet;
    //public AttackerAgent attackerAgent;


    public float turn;
    // Start is called before the first frame update
    public void Awake()
    {
        //获取资源
        resources = new Resource[5];
        resources[0] = GameObject.Find("Resource1").GetComponent<Resource>();
        resources[1] = GameObject.Find("Resource2").GetComponent<Resource>();
        resources[2] = GameObject.Find("Resource3").GetComponent<Resource>();
        resources[3] = GameObject.Find("Resource4").GetComponent<Resource>();
        resources[4] = GameObject.Find("Resource5").GetComponent<Resource>();
        envSet = GameObject.Find("Environment").GetComponent<EnvSet>();
        defender = GameObject.Find("Defender").GetComponent<Defender>();
        canOK = false;
        //Debug.Log(4);
        attacker = GameObject.Find("Attacker").GetComponent<Attacker>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    //进入新一轮时调用的函数
    public override void OnEpisodeBegin()
    {
        //turn = 1f;
        a = 0;
        deTimes = 0;
        goodChoiceTime = 0;
        //canOK = false;
        //Debug.Log("1asdsaf");
        //base.OnEpisodeBegin();
    }

    //收集观察的结果
    public override void CollectObservations(VectorSensor sensor)
    {
        //1.资源的UserLevel和Level 20维
        //sensor.AddObservation(attacker.goodChoice);
        layers_info = new float[5][];
        for (int i = 0; i < 5; i++)
        {
            layers_info[i] = new float[4];
            for (int j = 0; j < 4 && j < resources[i].en_layers.Length; j++)
            {
                layers_info[i][j] = resources[i].en_layers[j];
            }
        }
        sensor.AddObservation(layers_info[0]);
        sensor.AddObservation(layers_info[1]);
        sensor.AddObservation(layers_info[2]);
        sensor.AddObservation(layers_info[3]);
        sensor.AddObservation(layers_info[4]);

        sensor.AddObservation(resources[0].ackTimes);
        sensor.AddObservation(resources[1].ackTimes);
        sensor.AddObservation(resources[2].ackTimes);
        sensor.AddObservation(resources[3].ackTimes);
        sensor.AddObservation(resources[4].ackTimes);

    }

    //接收动作，是否给予奖励
    public override void OnActionReceived(ActionBuffers actions)
    {

        //0否 1是       
        int isDefend= actions.DiscreteActions[0];
        if (envSet.stradegy != 1 && envSet.stradegy != 0)
        {
            isDefend = 1;
        }
        //int isDefend = 1;
        //resourceId 0 1 2 3 4 
        int resourceId = actions.DiscreteActions[1];
        //被更换的算法id
        //int enId = actions.DiscreteActions[2];
        //更换后的算法id
        int chaToEnId = actions.DiscreteActions[2];
        //Debug.Log(canOK);
        //Debug.Log(isDefend+" "+resourceId+" "+chaToEnId);
        if (canOK)
        {
            //Debug.Log(isDefend + " " + resourceId + " " + chaToEnId);
            //if (resourceId == 0 || chaToEnId == 0)
            //{

            //    return;
            //}
            deTimes += 1;
            if (defender.moneyUseOut)
            {
                canOK = false;
                attacker.attack();
                return;
            }
            //if(isDefend==1 && attacker.goodChoice)
            //{
            //    AddReward(5f);
            //    goodChoiceTime++;
            //    canOK = false;
            //    attacker.attack();
            //    return;
            //}
            //else if(isDefend==0 && !attacker.goodChoice)
            //{
            //    AddReward(5f);
            //    goodChoiceTime++;
            //    canOK = false;
            //    attacker.attack();
            //    return;
            //}

            if (isDefend == 1)
            {
                //AddReward(10f);
                //if (!attacker.goodChoice)
                //{
                //    AddReward(-5f);
                //    canOK = false;
                //    attacker.attack();
                //    return;
                //}
                //goodChoiceTime += 1;
                defender.setParams(setType:envSet.stradegy-1, resourceId,chaToEnId);
                defender.defend();
                if (defender.reDefend)
                {
                    a++;
                    if (a >= 3)
                    {
                        AddReward(-2f);
                        OnEpisodeBegin();
                        canOK = false;
                        attacker.Init();
                        defender.Init();
                        attacker.attack();
                        defender.reDefend = false;
                        return;
                    }
                    defender.reDefend = false;
                    return;
                }
                canOK = false;
                attacker.attack();
            }
            else
            {
                ////选择不切换
                //if (attacker.goodChoice)
                //{
                //    AddReward(-5f);
                //}
                //else
                //{
                //    AddReward(5f);
                //    goodChoiceTime += 1;
                //}
                canOK = false;
                attacker.attack();
               
            }
            
        }

    }

    //是否手动操作智能体
    public override void Heuristic(in ActionBuffers actionsOut)
    {





    }
}
