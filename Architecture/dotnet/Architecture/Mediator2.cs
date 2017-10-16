using System;
using System.Collections.Generic;

namespace Architecture
{
    public class Mediator2
    {
        public static void Execute()
        {
            ICommunicationTool tool = new Radio();

            ICommunicator plane = new Plane(tool);
            ICommunicator radioTower = new RadioTower(tool);


            tool.RegisterObject(plane);
            tool.RegisterObject(radioTower);

            plane.SendMessage("Message from a plane");

            radioTower.SendMessage("Message back from radiotower");


        }



    }

    /// <summary>
    /// Communication tools
    /// </summary>
    interface ICommunicationTool
    {
        List<ICommunicator> Communicators { get; }

        void RegisterObject(ICommunicator communicator);
        void SendMessage(string message, ICommunicator communicator);
        
    }

    class Radio : ICommunicationTool
    {
        private List<ICommunicator> _communicators;
        public List<ICommunicator> Communicators
        {
            get
            {
                if(_communicators == null)
                {
                    _communicators = new List<ICommunicator>();
                }
                return _communicators;
            }
        }

        public void RegisterObject(ICommunicator communicator)
        {
            Communicators.Add(communicator);
        }

        /// <summary>
        /// Logic here to handle
        /// </summary>
        /// <param name="message"></param>
        /// <param name="communicator"></param>
        public void SendMessage(string message, ICommunicator communicator)
        {
            var type = communicator.GetType();
            if(type == typeof(Plane))
            {
                Console.WriteLine($"Plane sends a message: {message}");
            }
            else if(type == typeof(RadioTower))
            {
                Console.WriteLine($"Radio tower sends a message: {message}");
            }
        }
    }

    /// <summary>
    /// Communicators
    /// </summary>
    interface ICommunicator
    {
        void SendMessage(string message);
    }

    class Plane: ICommunicator
    {
        protected ICommunicationTool CommunicationTool { get; set; }

        public Plane(ICommunicationTool communicationTool)
        {
            CommunicationTool = communicationTool;
        }

        public void SendMessage(string message)
        {
            CommunicationTool.SendMessage(message, this);
        }
    }

    class RadioTower: ICommunicator
    {
        
        public ICommunicationTool CommunicationTool { get; set; }

        public RadioTower(ICommunicationTool communicationTool)
        {
            CommunicationTool = communicationTool;
        }

        public void SendMessage(string message)
        {
            CommunicationTool.SendMessage(message, this);
        }
    }


}
