using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadLampProblem
{
    class Program
    {
        /// <summary>
        /// 题目描述
        /// 一条长l的笔直的街道上有n个路灯，若这条街的起点为0，终点为l，第i个路灯坐标为ai ，每盏灯可以覆盖到的最远距离为d，为了照明需求，所有灯的灯光必须覆盖整条街，但是为了省电，要使这个d最小，请找到这个最小的d。
        /// 输入描述:
        /// 每组数据第一行两个整数n和l（n大于0小于等于1000，l小于等于1000000000大于0）。第二行有n个整数(均大于等于0小于等于l)，为每盏灯的坐标，多个路灯可以在同一点。
        /// 输出描述:
        /// 输出答案，保留两位小数。
        /// 思路：输入内容中已固定灯的坐标和数目，从而问题更简化为查找两相邻灯或灯和街道顶端之间的最大值，则必然满足覆盖整条街且d的值最小。
        /// </summary>
        /// <param name="args"></param>

        private static long roadLength;

        private static int lampCount;

        private static long[] lampCoordinate;

        private static bool isFirst = true;

        static void Main(string[] args)
        {
            string str = Console.ReadLine();

            AnalysisInputData(str);

            string str2 = Console.ReadLine();

            AnalysisInputData(str2);

            Console.WriteLine(GetLeastLampLightLength());

            Console.ReadLine();
        }

        private static void AnalysisInputData(string dataStr)
        {
            if (isFirst)
            {
                string[] roadDataStrs = dataStr.Split(' ');
                lampCount = int.Parse(roadDataStrs[0]);
                roadLength = long.Parse(roadDataStrs[1]);
                isFirst = false;
            }
            else
            {
                string[] lampDataStrs = dataStr.Split(' ');
                List<long> lampData = new List<long>();

                for (int index = 0; index < lampDataStrs.Length; index++)
                {
                    long lampDataValue = long.Parse(lampDataStrs[index]);

                    if (index == 0)
                    {
                        lampData.Add(lampDataValue);
                    }
                    else
                    {
                        for (int k = 0; k < lampData.Count; k++)
                        {
                            if (lampDataValue < lampData[k])
                            {
                                lampData.Insert(k, lampDataValue);
                                break;
                            }
                            else if (k == lampData.Count - 1)
                            {
                                lampData.Add(lampDataValue);
                                break;
                            }
                        }
                    }
                }

                lampCoordinate = new long[lampCount + 2];
                for (int j = 0; j < lampCoordinate.Length; j++)
                {
                    if (j == 0)
                    {
                        lampCoordinate[j] = 0;
                    }
                    else if (j == lampCoordinate.Length - 1)
                    {
                        lampCoordinate[j] = roadLength;
                    }
                    else
                    {
                        lampCoordinate[j] = lampData[j - 1];
                    }
                }
            }
        }

        private static float GetLeastLampLightLength()
        {
            long maxLength = 0;

            for (int i = 0; i < lampCoordinate.Length - 1; i++)
            {
                if (i == 0)
                {
                    maxLength = lampCoordinate[i + 1] - lampCoordinate[i];
                }
                else
                {
                    if (lampCoordinate[i + 1] - lampCoordinate[i] > maxLength)
                    {
                        maxLength = lampCoordinate[i + 1] - lampCoordinate[i];
                    }
                }
            }

            return maxLength / 2f;
        }
    }
}
