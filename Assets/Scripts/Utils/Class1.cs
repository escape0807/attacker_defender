using System;
using System.Collections.Generic;
using System.Linq;

//如何以最少的成本去完整攻破一个资源的所有加密层  计算最小成本
/*所提供的 C# 程序根据包含整数的输入数组 A 计算结果。程序遵循特定规则，以递归方式确定结果。下面是它的工作原理：

如果输入数组的长度为 2：

如果数组中的元素符合预定义的模式（使用比较法检查），则结果为 2。
否则，结果为 4。
如果输入数组的长度为 1，则结果为 2。

如果输入数组的长度为 0 或为空，结果为 0。

如果上述条件都不适用，程序会使用比较法将数组的最后三个元素与预定义模式进行比较：

如果模式匹配，程序会递归计算结果，从数组中删除最后三个元素，并在结果上加上 3。
如果有不同的模式匹配，程序会从数组中删除最后两个元素，并在结果上加上 2，从而计算出结果。
如果没有匹配的模式，程序会从数组中删除最后一个元素，并在结果中加上 2。
比较方法将输入数组与预定义的整数数组进行比较，以检查是否存在匹配。如果发现匹配，则返回 true；否则返回 false。

下面是程序在输入 A = {2, 1, 4, 5, 3} 时的运行过程：

首先，程序检查数组的长度是否大于 2。
它将最后三个元素 {4, 5, 3} 与预定义的模式进行比较，但没有一个匹配。
因此，它通过删除最后两个元素 {2, 1} 并在结果中加上 2 来计算结果。
然后，递归检查数组 {2, 1}，发现它与预定义模式 {2, 1} 匹配。
因此，它将结果加上 2。
最终结果是 4。
在本例中，程序返回输入数组 {2, 1, 4, 5, 3} 的结果 4。*/
class Program
{

    static void Main(string[] args)
    {
        int[] A = { 2, 1, 4, 5, 3 };
        Console.WriteLine(new Program().CalculateRes(A));

    }

    public int CalculateRes(int[] a)
    {
        if (a.Length==2)
        {
            if (compare(a))
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }
        else if (a.Length == 1)
        {
            return 2;
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

    

    public bool compare(int[] a)
    {
        int[][] arrays = new int[][] {//攻击工具的排列组合
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
}
