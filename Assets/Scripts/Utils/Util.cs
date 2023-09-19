using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

/*在 Assets.Scripts.Utils 命名空间中定义了一个 Util 类。该类包含各种实用方法。下面概述了每个方法的作用：

ack_utils：该字段是一个二维整数数组，包含长度为 2 和 3 的预定义数组。

GetRandomCharacters：该方法根据指定的参数（字符数、包含的数字、小写字母和大写字母）生成随机字符串。

CalculateRes：该方法根据输入的整数数组 a 计算结果，它遵循特定条件并递归计算结果。

compare：比较：该方法将输入的整数数组 a 与预定义的整数数组进行比较，检查是否匹配。如果发现匹配，则返回 true；否则返回 false。

chooseUtil：该方法通过检查输入的整数数组 a 是否与预定义的模式匹配来确定一个实用程序。它返回一个代表实用程序的整数，如果没有匹配，则返回-1。

findFirst：如果所有元素都是-1，则返回-1。

lastResourceIsGood：该方法检查输入数组 a 中的最后一个资源是否为好资源：此方法根据指定的实用程序检查序列中的最后一个资源是否良好。
        如果最后一个资源符合实用程序，则返回 true；否则返回 false。

PortInUse：此方法通过检查活动的 TCP 监听器，检查本地计算机上的特定网络端口是否在使用中。如果端口正在使用，则返回 true，否则返回 false。

Util 类提供了一系列实用程序方法，可用于各种情况，包括字符串生成、模式匹配和网络端口检查。*/
namespace Assets.Scripts.Utils
{
    
    public class Util
    {
        public static int[][] ack_utils = new int[][]
        {
            new int[2] { 1, 2 },
            new int[2] { 3, 4 },
            new int[2] { 5, 6 },
            new int[2] { 1, 3 },
            new int[2] { 2, 5 },
            new int[2] { 4, 6 },
            new int[3] { 1, 2, 4 },
            new int[3] { 1, 2, 5 },
            new int[3] { 3, 4, 6 },
            new int[3] { 3, 5, 6 }
        };
        
        public static string GetRandomCharacters(int n = 10, bool Number = true, bool Lowercase = true, bool Capital = true)  // 生成随机字符串
        {
            StringBuilder tmp = new StringBuilder();
            Random rand = new Random();
            string characters = (Capital ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : null) + (Number ? "0123456789" : null) + (Lowercase ? "abcdefghijklmnopqrstuvwxyz" : null);
            if (characters.Length < 1)
            {
                return (null);
            }
            for (int i = 0; i < n; i++)
            {
                tmp.Append(characters[rand.Next(0, characters.Length)].ToString());
            }
            return (tmp.ToString());
        }
        public static int CalculateRes(int[] a)
        {
            if (a.Length != 0)
            {
                if (a[0] == -1)
                {
                    int i = 0;
                    while (a[i] == -1)
                    {
                        i++;
                    }
                    a = a.Skip(i).ToArray();

                }
            }

            if (a.Length == 2)
            {
                if (compare(a))
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else if (a.Length == 1)
            {
                return 1;
            }
            else if (a.Length == 0 || a == null)
            {
                return 0;
            }
            if (compare(a.Skip(a.Length - 3).ToArray()))
            {
                return CalculateRes(a.Take(a.Length - 3).ToArray()) + 3;
            }
            else if (compare(a.Skip(a.Length - 2).ToArray()))
            {
                return CalculateRes(a.Take(a.Length - 2).ToArray()) + 2;
            }
            else
            {
                return CalculateRes(a.Take(a.Length - 1).ToArray()) + 2;
            }


        }
        public static bool compare(int[] a)
        {
            int[][] arrays = new int[][] {
            new int[] { 1, 2, 4 },
            new int[] { 1, 4, 2 },
            new int[] { 2, 1, 4 },
            new int[] { 2, 4, 1 },
            new int[] { 4, 2, 1 },
            new int[] { 4, 1, 2 },

            new int[] { 1, 2, 5 },
            new int[] { 1, 5, 2 },
            new int[] { 2, 1, 5 },
            new int[] { 2, 5, 1 },
            new int[] { 5, 2, 1 },
            new int[] { 5, 1, 2 },

            new int[] { 3, 4, 6 },
            new int[] { 3, 6, 4 },
            new int[] { 4, 3, 6 },
            new int[] { 4, 6, 3 },
            new int[] { 6, 3, 4 },
            new int[] { 6, 4, 3 },

            new int[] { 3, 5, 6 },
            new int[] { 3, 6, 5 },
            new int[] { 5, 3, 6 },
            new int[] { 5, 6, 3 },
            new int[] { 6, 5, 3 },
            new int[] { 6, 3, 5 },

            new int[] { 1, 2 },
            new int[] { 2, 1 },
            new int[] { 3, 4 },
            new int[] { 4, 3 },
            new int[] { 5, 6 },
            new int[] { 6, 5 },
            new int[] { 1, 3 },
            new int[] { 3, 1 },
            new int[] { 2, 5 },
            new int[] { 5, 2 },
            new int[] { 4, 6 },
            new int[] { 6, 4 }
        };
            for (int i = 0; i < arrays.Length; i++)
            {

                if (arrays[i].SequenceEqual(a))
                {
                    return true;
                }
            }
            return false;
        }

        public static int chooseUtil(int[] a)
        {
            int[][] arrays = new int[][] {
            new int[] { 1, 2, 4 },
            new int[] { 1, 4, 2 },
            new int[] { 2, 1, 4 },
            new int[] { 2, 4, 1 },
            new int[] { 4, 2, 1 },
            new int[] { 4, 1, 2 },

            new int[] { 1, 2, 5 },
            new int[] { 1, 5, 2 },
            new int[] { 2, 1, 5 },
            new int[] { 2, 5, 1 },
            new int[] { 5, 2, 1 },
            new int[] { 5, 1, 2 },

            new int[] { 3, 4, 6 },
            new int[] { 3, 6, 4 },
            new int[] { 4, 3, 6 },
            new int[] { 4, 6, 3 },
            new int[] { 6, 3, 4 },
            new int[] { 6, 4, 3 },

            new int[] { 3, 5, 6 },
            new int[] { 3, 6, 5 },
            new int[] { 5, 3, 6 },
            new int[] { 5, 6, 3 },
            new int[] { 6, 5, 3 },
            new int[] { 6, 3, 5 },

            new int[] { 1, 2 },
            new int[] { 2, 1 },

            new int[] { 3, 4 },
            new int[] { 4, 3 },

            new int[] { 5, 6 },
            new int[] { 6, 5 },

            new int[] { 1, 3 },
            new int[] { 3, 1 },

            new int[] { 2, 5 },
            new int[] { 5, 2 },

            new int[] { 4, 6 },
            new int[] { 6, 4 }
        };
            int index = 0;
            bool flag = false;
            for (index = 0; index < arrays.Length; index++)
            {
                //for(int i=0;i< a.Length; i++)
                //{
                //    for(int j = 0; j < arrays[index].Length.j++)
                //    {
                //        if(a[i]==arrays[index][j])
                //    }
                //}
                if (arrays[index].SequenceEqual(a))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                if (0 <= index && index <= 5)
                {
                    return 7;
                }
                else if(6 <= index && index <= 11)
                {
                    return 8;
                }
                else if (12 <= index && index <= 17)
                {
                    return 9;
                }
                else if (18 <= index && index <= 23)
                {
                    return 10;
                }
                else if (24 <= index && index <= 25)
                {
                    return 1;
                }
                else if (26 <= index && index <= 27)
                {
                    return 2;
                }
                else if (28 <= index && index <= 29)
                {
                    return 3;
                }
                else if (30 <= index && index <= 31)
                {
                    return 4;
                }
                else if (32 <= index && index <= 33)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
            }
            else
            {
                return -1;
            }
            
        }
        public static int findFirst(int[] a)
        {
            int i = 0;
            while (i < a.Length && a[i] == -1)
            {
                i++;
            }
            if (i >= a.Length)
            {
                return -1;
            }
            else
            {
                return a[i];
            }
            
        }

        public static bool lastResourceIsGood(int begin,int util)
        {
            for (int i = 0; i < ack_utils[util - 1].Length; i++)
            {
                if (ack_utils[util - 1][i] == begin)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;  // 返回true说明端口被占用

        }

    }
}
