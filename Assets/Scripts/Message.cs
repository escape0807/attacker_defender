using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Message
    {
        //最初的信息文本
        public string rawText;
        //携带的信息
        public string text;
        //加密方法 1 2 3 4 5 6
        public int en_methold; 
        //加密密钥
        public string key;
        //加密的初始向量;
        public string iv;
        //该资源的价值
        //public float value;

        public Message()
        {
            //最初的信息文本，使用随机生成，10位
            this.rawText = Util.GetRandomCharacters();
            this.text = this.rawText;

            //加密信息初始化

            this.key = "";
            this.iv = "";
            this.en_methold = 0;

            //随机生成0.1-0.9的权重
            //this.value = new Random().Next(1, 10) / 10.0f;
        }
    }
}
