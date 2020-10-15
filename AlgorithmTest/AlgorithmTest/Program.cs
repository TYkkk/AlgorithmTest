using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgorithmTest
{
    /// <summary>
    ///题目描述
    ///小v今年有n门课，每门都有考试，为了拿到奖学金，小v必须让自己的平均成绩至少为avg。每门课由平时成绩和考试成绩组成，满分为r。现在他知道每门课的平时成绩为ai ,若想让这门课的考试成绩多拿一分的话，小v要花bi 的时间复习，不复习的话当然就是0分。同时我们显然可以发现复习得再多也不会拿到超过满分的分数。为了拿到奖学金，小v至少要花多少时间复习。
    ///输入描述:
    ///第一行三个整数n,r,avg(n大于等于1小于等于1e5，r大于等于1小于等于1e9, avg大于等于1小于等于1e6)，接下来n行，每行两个整数ai和bi，均小于等于1e6大于等于1
    ///输出描述:
    ///一行输出答案。
    /// </summary>
    class Program
    {
        private static int courseNum;

        private static int gradeMax;

        private static int targetAvgGrade;

        private static List<CourseData> courseDatas;

        static void Main(string[] args)
        {
            string str = Console.ReadLine();

            courseDatas = new List<CourseData>();

            AnalysisInputData(str);

            GetLeastConsumeTimeProgram();

            Console.ReadLine();
        }

        /// <summary>
        /// 此方法用于输入一行内容后解析
        /// </summary>
        /// <param name="dataStr"></param>
        private static void AnalysisInputData(string dataStr)
        {
            string[] temp = dataStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < temp.Length; i++)
            {
                if (string.IsNullOrEmpty(temp[i]))
                {
                    break;
                }

                if (i == 0)
                {
                    AnalysisConstData(temp[i]);
                }
                else
                {
                    AnalysisCourseData(temp[i]);
                }
            }
        }

        private static void AnalysisConstData(string dataStr)
        {
            string[] constDataStrs = dataStr.Split(' ');

            courseNum = int.Parse(constDataStrs[0]);
            gradeMax = int.Parse(constDataStrs[1]);
            targetAvgGrade = int.Parse(constDataStrs[2]);
        }

        private static void AnalysisCourseData(string dataStr)
        {
            string[] constDataStrs = dataStr.Split(' ');

            CourseData courseData = new CourseData(int.Parse(constDataStrs[0]), int.Parse(constDataStrs[1]));
            if (courseDatas.Count <= 0)
            {
                courseDatas.Add(courseData);
            }
            else
            {
                for (int index = 0; index < courseDatas.Count; index++)
                {
                    if (courseData.ConsumeTime < courseDatas[index].ConsumeTime)
                    {
                        courseDatas.Insert(index, courseData);
                        break;
                    }
                    else if (index == courseDatas.Count - 1)
                    {
                        courseDatas.Add(courseData);
                        break;
                    }
                }
            }
        }

        private static long GetLeastConsumeTimeProgram()
        {
            long consumeTime = 0;

            ChangeConsumeTimeGetGrade(ref consumeTime);

            Console.WriteLine(consumeTime);

            return consumeTime;
        }

        private static void ChangeConsumeTimeGetGrade(ref long consumeTime)
        {
            if (CheckGradeEnough())
            {
                return;
            }

            for (int i = 0; i < courseDatas.Count; i++)
            {
                ConsumeTimeAddGrade(courseDatas[i], ref consumeTime);

                if (CheckGradeEnough())
                {
                    return;
                }
            }
        }

        private static void ConsumeTimeAddGrade(CourseData courseData, ref long consumeTime)
        {
            if (courseData.CurrentGrade < gradeMax)
            {
                courseData.CurrentGrade += 1;
                consumeTime += courseData.ConsumeTime;

                if (CheckGradeEnough())
                {
                    return;
                }
                else
                {
                    ConsumeTimeAddGrade(courseData, ref consumeTime);
                }
            }
        }

        private static bool CheckGradeEnough()
        {
            if (courseDatas == null)
            {
                return false;
            }

            int total = 0;

            foreach (var child in courseDatas)
            {
                total += child.CurrentGrade;
            }

            return (total / courseNum) >= targetAvgGrade;
        }
    }

    class CourseData
    {
        public int NormalGrade { get; }
        public long ConsumeTime { get; }
        public int CurrentGrade { get; set; }

        public CourseData(int normalGrade, long consumeTime)
        {
            NormalGrade = normalGrade;
            ConsumeTime = consumeTime;
            CurrentGrade = NormalGrade;
        }
    }
}
