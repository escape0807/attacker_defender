using Assets.Scripts;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;//���������ռ�
using Random = System.Random;
using System.Linq;

public class Attacker : MonoBehaviour
{
    public int initMoney;
    public int costMoney;
    public int restMoney;
     //选择的即将攻击的资源编号, 1 2 3 4 5
    public int chooseToAttack;
    public int chooseUtil;
    public int lastChooseUtil;
    public int lastResource;
    public Resource resource;
    public Resource[] resources;
    //攻击工具
    public int[][] ack_utils;
    public int[] util_cost;
    //
    
    public int changeTimes;
    public EnvSet envSet;
    public GameObject ackEnergyInfo;
    public bool success;
    public int nextCTA;

    public bool goodChoice;
    //public AttackerAgent attackerAgent;

    public DefenderAgent defenderAgent;
    public float resourceValue;
    public bool a;
    
    // Start is called before the first frame update
    public void ResetMe()
    {
        this.Start();

    }
    /// <summary>
    ///  获取相关组件，并对攻击工具初始化
    /// </summary>
    public void Start()
    {
        envSet = GameObject.Find("Environment").GetComponent<EnvSet>();
        ackEnergyInfo = GameObject.Find("AckEnergyInfo");
        defenderAgent = GameObject.Find("Defender").GetComponent<DefenderAgent>();
        //工具
        ack_utils = new int[10][];
        util_cost = new int[10];
        //Debug.Log(123);
        resources = new Resource[5];
        resources[0] = GameObject.Find("Resource1").GetComponent<Resource>();
        resources[1] = GameObject.Find("Resource2").GetComponent<Resource>();
        resources[2] = GameObject.Find("Resource3").GetComponent<Resource>();
        resources[3] = GameObject.Find("Resource4").GetComponent<Resource>();
        resources[4] = GameObject.Find("Resource5").GetComponent<Resource>();

        ack_utils[0] = new int[2] { 1, 2 };
        ack_utils[1] = new int[2] { 3, 4 };            
        ack_utils[2] = new int[2] { 5, 6 };
        ack_utils[3] = new int[2] { 1, 3 };
        ack_utils[4] = new int[2] { 2, 5 };
        ack_utils[5] = new int[2] { 4, 6 };
        ack_utils[6] = new int[3] { 1, 2, 4 };
        ack_utils[7] = new int[3] { 1, 2, 5 };
        ack_utils[8] = new int[3] { 3, 4, 6 };
        ack_utils[9] = new int[3] { 3, 5, 6 };

        util_cost[0] = 2;
        util_cost[1] = 2;
        util_cost[2] = 2;
        util_cost[3] = 2;
        util_cost[4] = 2;
        util_cost[5] = 2;
        util_cost[6] = 3;
        util_cost[7] = 3;
        util_cost[8] = 3;
        util_cost[9] = 3;
        
        
    }

    /// <summary>
    /// 对属性进行初始化
    /// </summary>
    public void Init()
    {
        a = false;
        lastResource = 0;
        goodChoice = false;
        resourceValue = 0f;
        chooseToAttack = 0;
        chooseUtil = 0;
        lastChooseUtil = 0;
        initMoney = 6;
        costMoney = 0;
        restMoney = initMoney;
        changeTimes = 0;
        success = false;
        nextCTA = 0;

        resource = null;      
        showEnergy();
    }

    /// <summary>
    /// 目标资源选择算法的具体实现
    /// </summary>
    public void chooseResourceAndUtil()
    {    
        if (lastResource != 0)
        {
            
            string resName = "Resource" + lastResource;
            Resource r = GameObject.Find(resName).GetComponent<Resource>();
            if (Util.lastResourceIsGood(Util.findFirst(r.en_layers), lastChooseUtil)){
                chooseToAttack = lastResource;
                chooseUtil = lastChooseUtil;
                return;
            }
        }
        int a1 = Util.CalculateRes(resources[0].en_layers);
        int a2 = Util.CalculateRes(resources[1].en_layers);
        int a3 = Util.CalculateRes(resources[2].en_layers);
        int a4 = Util.CalculateRes(resources[3].en_layers);
        int a5 = Util.CalculateRes(resources[4].en_layers);
        if(a1<=a2&& a1 <= a3 && a1 <= a4 && a1 <= a5)
        {
            chooseToAttack = 1;
            if (lastResource != chooseToAttack)
            {
                changeTimes += 1;
            }
            lastResource = 1;
            resource = resources[0];
        }
        else if(a2 <= a1 && a2 <= a3 && a2 <= a4 && a2 <= a5)
        {
            chooseToAttack = 2;
            if (lastResource != chooseToAttack)
            {
                changeTimes += 1;
            }
            lastResource = 2;
            resource = resources[1];
        }
        else if (a3 <= a1 && a3 <= a2 && a3 <= a4 && a3 <= a5)
        {
            chooseToAttack = 3;
            if (lastResource != chooseToAttack)
            {
                changeTimes += 1;
            }
            lastResource = 3;
            resource = resources[2];
        }
        else if (a4 <= a1 && a4 <= a2 && a4 <= a3 && a4 <= a5)
        {
            chooseToAttack = 4;
            if (lastResource != chooseToAttack)
            {
                changeTimes += 1;
            }
            lastResource = 4;
            resource = resources[3];
        }
        else if (a5 <= a1 && a5 <= a2 && a5 <= a3 && a5 <= a4)
        {
            chooseToAttack = 5;
            if (lastResource != chooseToAttack)
            {
                changeTimes += 1;
            }
            lastResource = 5;
            resource = resources[4];
        }
        int i = 0;
        while (resource.en_layers[i] == -1)
        {
            i++;
        }
        int[] newLayers = resource.en_layers.Skip(i).ToArray();
        if (Util.chooseUtil(newLayers) != -1)
        {
            chooseUtil = Util.chooseUtil(newLayers);
            return;
        }
        else if(Util.chooseUtil(newLayers) != -1)
        {
            chooseUtil = Util.chooseUtil(newLayers);
            return;
        }
        else
        {
            int j = newLayers[0];
            switch (j)
            {
                case 1:
                    chooseUtil = 1;
                    break;
                case 2:
                    chooseUtil = 1;
                    break;
                case 3:
                    chooseUtil = 2;
                    break;
                case 4:
                    chooseUtil = 2;
                    break;
                case 5:
                    chooseUtil = 3;
                    break;
                case 6:
                    chooseUtil = 3;
                    break;
            }
            return;
        }

    }
    /// <summary>
    ///  在canvas上展示剩余能量值
    /// </summary>
    public void showEnergy()
    {
        string s = "E: ";
        s += restMoney;
        ackEnergyInfo.GetComponent<TMP_Text>().text = s;
    }
    //public void setParams()
    //{
    //    //if (resourceId != 0 && utilId != 0)
    //    //{
    //    //    chooseToAttack = resourceId;
    //    //    chooseUtil = utilId;
    //    //}
    //    //else
    //    //{
    //    //    GameObject tmp1 = GameObject.Find("AckChoRes").gameObject;
    //    //    chooseToAttack = int.Parse(tmp1.GetComponent<TMP_InputField>().text);
    //    //    GameObject tmp2 = GameObject.Find("ChoAckUtil").gameObject;
    //    //    chooseUtil = int.Parse(tmp2.GetComponent<TMP_InputField>().text);
    //    //}
    //    chooseToAttack = new Random().Next(1, 6);

    //}


    /// <summary>
    ///    根据资源编号和攻击工具编号攻击资源
    /// </summary>
    public void attack()
    {
        //攻击
        chooseResourceAndUtil();
        if (lastChooseUtil != chooseUtil)
        {
            costMoney += util_cost[chooseUtil - 1];
            restMoney = initMoney - costMoney;
            if (restMoney < 0)
            {
                //costMoney += 1;
                envSet.gameOver();
                return;
            }
        }
        lastChooseUtil = chooseUtil;
        if (changeResource(chooseUtil, resource))
        {
            showEnergy();
            //Debug.Log(1);
            envSet.showResource();
            resource.ackTimes++;
            
            //判断是否成功
            if (resource.en_layers[resource.en_layers.Length - 1] == -1)
            {
                //Debug.Log(2);
                success = true;
                envSet.gameOver();
                return;
            }
            else
            {
                //Debug.Log(3);
                if (Util.lastResourceIsGood(Util.findFirst(resource.en_layers), lastChooseUtil))
                {
                    //Debug.Log("**********");
                    goodChoice = true;
                }
                else
                {
                    goodChoice = false;
                }
                defenderAgent.canOK = true;
                return;
            }
        }




    }

    /// <summary>
    ///修改资源的加密层
    /// </summary>
    bool changeResource(int util,Resource r)
    {
        int k = 0;
        while (r.en_layers[k]==-1)
        {
            k++;
        }
        for(int i = 0; i < ack_utils[util - 1].Length; i++)
        {
            if (ack_utils[util - 1][i] == r.en_layers[k])
            {
                r.en_layers[k] = -1;
                if(k< r.en_layers.Length - 1)
                {
                    nextCTA = r.en_layers[k+1];
                }
                return true;
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {


    }
    
}
