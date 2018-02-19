using System;

namespace TestarwKaiGoustarw.MethodsAsParameters
{
    public class Service
    {
        private readonly Action workAction;
        private bool work;

        public Service(Action workAction)
        {
            this.workAction = workAction;
            this.work = true;
        }

        public void DoSomething()
        {
            while (work)
            {
                workAction();
            }
        }

        public void StopDoingThatSomething()
        {
            work = false;
        }
    }
}