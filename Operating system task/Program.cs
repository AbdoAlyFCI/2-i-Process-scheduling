using System;
using System.Collections.Generic;

namespace Operating_system_task
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Level> Levels = new List<Level>();
            //initialize Queue Levels
            Console.Write("Select the number of Levels:");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Levels.Add(new Level(Convert.ToInt32(Math.Pow(2, i))));
            }

            //initialize process
            int TotalTime = 0,
                ActiveTime = 0,
                RealTime = 0;
            Queue<Process> ArrivalList = new Queue<Process>();
            Queue<Process> FinishedProcess = new Queue<Process>();
            Console.Write("Select the number of process to perform: ");
            n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Process {i + 1}: ");
                string input = Console.ReadLine();
                int a = int.Parse(input.Split(' ')[0]);
                int s = int.Parse(input.Split(' ')[1]);
                ArrivalList.Enqueue(new Process(i+1,a, s));
                TotalTime += s;
            }
            bool getItem = false;
            while (ActiveTime != TotalTime)
            {
                getItem = false;
                if (ArrivalList.Count != 0 && ArrivalList.Peek().ArrivalTime <= RealTime)
                {
                    Levels[0].AddProcess(ArrivalList.Dequeue());
                }
                for (int i = 0; i < Levels.Count; i++)
                {
                    if (Levels[i].ProcessCount() != 0)
                    {
                        (int, Process) values = Levels[i].ExecuteTopProcess(RealTime);
                        ActiveTime += values.Item1;
                        RealTime += values.Item1;
                        if (ArrivalList.Count != 0 && ArrivalList.Peek().ArrivalTime <= RealTime)
                        {
                            Levels[0].AddProcess(ArrivalList.Dequeue());
                        }
                        for (int j = Levels.Count - 1; j >= i; j--)
                        {
                            if (j == i)
                            {
                                if (j == 0)
                                {
                                    if (Levels[j].ProcessCount() != 0)
                                    {
                                        AddProcess(i + 1, values.Item2);
                                    }
                                    else
                                    {
                                        AddProcess(i, values.Item2);
                                    }
                                }
                                else if (j == Levels.Count - 1)
                                {
                                    AddProcess(j, values.Item2);
                                }
                                else
                                {
                                    AddProcess(i + 1, values.Item2);
                                }
                                getItem = true;
                                break;
                            }
                            else if (Levels[j].ProcessCount() != 0)
                            {
                                AddProcess(i + 1, values.Item2);
                                getItem = true;
                                break;
                            }
                        }
                    }
                    if (getItem == true)
                    {
                        break;
                    }
                }
                if (getItem == true)
                {
                    continue;
                }
                RealTime++;

            }


            foreach (var item in FinishedProcess)
            {
                
                Console.WriteLine(item.num+"\t"+ item.WaitingTime+"\t "+item.ResponseTime);
            }

            void AddProcess(int i,Process process)
            {
                if (!process.IsFinished())
                {
                    Levels[i].AddProcess(process);
                }
                else
                {
                    process.Calculate(RealTime);
                    FinishedProcess.Enqueue(process);
                }
            }
        }
    }
}
