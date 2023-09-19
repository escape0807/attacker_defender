using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Text;
using System.Security.Cryptography;

public class Resource : MonoBehaviour
{
    public Message[] messages;
    public int[] en_layers;//加密层 用数字表示 每个数字代表一种加密方法ּ
    public Attacker attacker;
    public EnvSet envSet;

    public float value;

    public int ackTimes;//�������Ĵ���  //次数
    // Start is called before the first frame update
    public void ResetMe()
    {
        //this.Start();
        //Debug.Log("Resource��Message��������");//Resource：Message初始化
    }
    void Awake()
    {
        
        messages = null;
        Init();
        
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void Init()
    {
        //����4-5�����ܲ�// 随机生成4-5层加密
        this.value = new Random().Next(1, 11) / 10.0f;
        ackTimes = 0;
        int cnt = new Random().Next(4, 6);
        //int cnt = 5;
        en_layers = getRandomNum(cnt, 1, 6);
        //bubbleSort(en_layers);
        this.messages = new Message[cnt];
        for(int i = 0; i < cnt; i++)
        {
            this.messages[i] = new Message();
            en_message(this.messages[i], en_layers[i]);
        }
        //for(int i = 0; i < cnt; i++)
        //{
        //    Debug.Log(messages[i].en_methold + "||" + messages[i].rawText + "||" + messages[i].text + "||" + messages[i].key + "||" + messages[i].iv + "||" + messages[i].value);
        //}      
    }
    //�������num����ͬ����// ... 这里是随机生成不重复数字的代码 ...
    public int[] getRandomNum(int num, int minValue, int maxValue)
    {
        if ((maxValue + 1 - minValue - num < 0))
            maxValue += num - (maxValue + 1 - minValue);
        Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
        int[] arrNum = new int[num];
        int tmp = 0;
        StringBuilder sb = new StringBuilder(num * maxValue.ToString().Trim().Length);

        for (int i = 0; i <= num - 1; i++)
        {
            tmp = ra.Next(minValue, maxValue);
            while (sb.ToString().Contains("#" + tmp.ToString().Trim() + "#"))
                tmp = ra.Next(minValue, maxValue + 1);
            arrNum[i] = tmp;
            sb.Append("#" + tmp.ToString().Trim() + "#");
        }
        attacker = GameObject.Find("Attacker").GetComponent<Attacker>();
        envSet = GameObject.Find("Environment").GetComponent<EnvSet>();
        if (envSet.stradegy == 1)
        {
            attacker.costMoney = 1;
        }
        return arrNum;
    }

    //�������С��������// ... 这里是冒泡排序的代码 ...
    private void bubbleSort(int[] array)
    {
        int median = 0;
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i] > array[j])
                {
                    median = array[i];
                    array[i] = array[j];
                    array[j] = median;
                }
            }
        }
    }

    //��message���м��ܣ����ҽ������õ�key��iv�����message,���ܷ���Ϊ1 2 3 4 5 6 // ... 这里是加密 message 的代码 ...
    void en_message(Message message,int en_methold)
    {
        string s = "";//���ܺ������
        string key = "";
        string iv = "";
        string rawText = message.rawText;//����ǰ������
        switch (en_methold)
        {
            case 1:
                KeyTool.CreateSymmetricAlgorithmKey<DESCryptoServiceProvider>(out key, out iv, 64);
                s = DesTool.EncryptString(key, iv, rawText);
                break;

            case 2:
                KeyTool.CreateSymmetricAlgorithmKey<TripleDESCryptoServiceProvider>(out key, out iv, 128);
                s = _3DesTool.EncryptString(key, iv, rawText);
                break;
            case 3:
                KeyTool.CreateSymmetricAlgorithmKey<RijndaelManaged>(out key, out iv, 192);
                s = AesTool.EncryptString(key, rawText);
                break;
            case 4:
                DESCryptoServiceProvider desCrypto1 = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
                key = ASCIIEncoding.ASCII.GetString(desCrypto1.Key);
                s = MD5Tool.EncryptString(key, rawText);
                break;
            case 5:
                DESCryptoServiceProvider desCrypto2 = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
                key = ASCIIEncoding.ASCII.GetString(desCrypto2.Key);
                s = Base64Tool.Encrypt(key, rawText);
                break;
            case 6:
                DES rc4 = DES.Create();
                rc4.GenerateKey();
                byte[] b = rc4.Key;
                key = System.Convert.ToBase64String(b);
                key = key.Substring(0, 8);
                s = RC4Tool.EncryptString(key, rawText);
                break;
        }
        message.text = s;
        message.key = key;
        message.iv = iv;
        message.en_methold = en_methold;
    }
    override
    public string ToString()  // ... 这里是将 en_layers 转换为字符串的代码 ...
    {
        string s = "{";
        for(int i = 0; i < en_layers.Length-1; i++)
        {
            if (en_layers[i] == -1)
            {
                s += "-";
            }
            else
            {
                s += en_layers[i];
            }
            
            s += ",";
        }
        //Debug.Log(en_layers.Length);
        if (en_layers[en_layers.Length - 1] == -1)
        {
            s += "-";
        }
        else
        {
            s += en_layers[en_layers.Length - 1];
        }
        s += "}";
        return s;
    }
}
