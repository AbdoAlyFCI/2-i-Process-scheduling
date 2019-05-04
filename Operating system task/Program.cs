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
                NextarrivalTime = 0,
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
                if (i == 0)
                    NextarrivalTime = a;
            }
            Levels[0].Processes.Enqueue(ArrivalList.Dequeue());
            RealTime = Levels[0].Processes.Peek().ArrivalTime;
            NextarrivalTime = ArrivalList.Peek().ArrivalTime;
            while (ActiveTime != TotalTime || FinishedProcess.Count !=n)
            {

                for (int i = 0; i < Levels.Count; i++)
                {
                    if (Levels[i].Processes.Count != 0)
                    {
                        (int, Process) values = Levels[i].ExecuteTopProcess(ActiveTime);
                        ActiveTime += values.Item1;
                        if (ArrivalList.Count !=0 && NextarrivalTime == RealTime)
                        {   
                                Levels[0].Processes.Enqueue(ArrivalList.Dequeue());
                            if (ArrivalList.Count != 0)
                            {
                                NextarrivalTime = ArrivalList.Peek().ArrivalTime;
                            }
                        }
                        Process temp = values.Item2;
                        for (int j = Levels.Count - 1; j >= i; j--)
                        {

                            if (j == i)
                            {

                                if (j == 0)
                                {
                                    if (Levels[j].Processes.Count != 0)
                                        AddProcess(i + 1, temp);
                                    else
                                        AddProcess(i, temp);
                                }
                                else if (j == Levels.Count - 1)
                                {
                                    AddProcess(j, temp);
                                }
                                else
                                {
                                    AddProcess(i+1, temp);

                                }
                                break;
                            }
                            else if (Levels[j].Processes.Count != 0)
                            {
                                AddProcess(i + 1, temp);
                                break;
                            }
                        }
                        break;
                    }
                }
            }


            foreach (var item in FinishedProcess)
            {
                
                Console.WriteLine(item.num+"\t"+ item.WaitingTime+"\t "+item.ResponseTime);
            }

            void AddProcess(int i,Process process)
            {
                if (!process.IsFinished())
                {
                    Levels[i].Processes.Enqueue(process);
                }
                else
                {
                    process.Calculate(ActiveTime);
                    FinishedProcess.Enqueue(process);
                }
            }
        }
    }
}
