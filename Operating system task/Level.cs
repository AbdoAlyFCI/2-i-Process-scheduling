using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operating_system_task
{
    class Level
    {
        private Queue<Process> Processes { get; set; }

        private readonly int Quantum=0;

        public Level (int quantum)
        {
            Processes = new Queue<Process>();
            this.Quantum = quantum;
        }
        public void AddProcess(Process process)
        {
            Processes.Enqueue(process);
        }

        public int ProcessCount()
        {
            return Processes.Count;
        }
        public (int, Process) ExecuteTopProcess(int CurrentTime)
        {
            Process process = Processes.Dequeue();
            int time= process.ExecuteProcess(Quantum, CurrentTime);
            return (time, process);
        }
    }
}
