namespace Operating_system_task
{
    class Process
    {
        public int num { get; set; }
        public int ArrivalTime { get; set; }
        public int ServiceTime { get; set; }
        public int FinsihTime { get; set; }
        public int WaitingTime { get; set; }
        public int ResponseTime { get; set; }


        private int RemainingTime;

        public Process(int num, int arrivalTime, int serviceTime)
        {
            this.ArrivalTime = arrivalTime;
            this.ServiceTime = serviceTime;
            RemainingTime = serviceTime;
            this.num = num;
        }

        public int ExecuteProcess(int ExecuteTime, int CurrentTime)
        {
            int tempTime = RemainingTime;
            if ((RemainingTime - ExecuteTime) > 0)
            {
                RemainingTime -= ExecuteTime;
                return ExecuteTime;
            }
            else
            {
                
                RemainingTime = 0;
                return tempTime;
            }
        }

        public bool IsFinished()
        {
            if (RemainingTime == 0)
            {
                return true;
            }

            return false;
        }

        public void Calculate(int FinishTime)
        {
            this.FinsihTime = FinishTime;
            ResponseTime = FinsihTime - ArrivalTime;
            WaitingTime = ResponseTime - ServiceTime;
        }
        

    }
}
