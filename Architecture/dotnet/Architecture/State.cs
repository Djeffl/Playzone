using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public class State
    {
        public static void Execute()
        {
            var phone = new Phone();

            phone.Charge();
            phone.Charge();
            Console.WriteLine(phone.BatteryLevel);

            phone.PhoneState = new ActivePhoneState(phone.BatteryLevel, phone);
            phone.Charge();
            phone.DrownBattery();

        }
    }

    class Phone
    {
        public PhoneState PhoneState { get; set; }
        public double BatteryLevel { get; set; }

        public Phone()
        {
            PhoneState = new StandbyPhoneState(0.0, this);
        }

        public void Charge()
        {
            PhoneState.Charge();
        }
        
        public void DrownBattery()
        {
            PhoneState.DrownBattery();
        }

        public void DisplayBatteryZone()
        {
            Console.WriteLine($"{PhoneState.BatteryZone} is the current battery state");
        }

    }

    abstract class PhoneState
    {
        public double BatteryCharge { get; set; }
        public double DrownageAmount { get; set; }
        public double ChargingAmount { get; set; }
        public string BatteryZone { get; set; }

        public Phone Phone { get; set; }

        public abstract void DrownBattery();
        public abstract void Charge();
        
        public void Log()
        {
            Console.WriteLine($"Current battery level is: {BatteryCharge}");
        }

    }

    class ActivePhoneState : PhoneState
    {
        public ActivePhoneState(double batteryCharge, Phone phone)
        {
            BatteryCharge = batteryCharge;
            Phone = phone;
            Initialize();
        }

        private void Initialize()
        {
            DrownageAmount = 5;
            ChargingAmount = 5;
        }

        public override void Charge()
        {
            BatteryCharge += ChargingAmount;
            Console.WriteLine(ChargingAmount);
        }

        public override void DrownBattery()
        {
            BatteryCharge -= DrownageAmount;
            Console.WriteLine(DrownageAmount);
        }
    }

    class StandbyPhoneState : PhoneState
    {
        public StandbyPhoneState(PhoneState state):
            this(state.BatteryCharge, state.Phone)
        {

        }
        public StandbyPhoneState(double batteryCharge, Phone phone)
        {
            BatteryCharge = batteryCharge;
            Phone = phone;
            Initialize();
        }

        private void Initialize()
        {
            DrownageAmount = 2;
            ChargingAmount = 10;
        }

        public override void Charge()
        {
            BatteryCharge += ChargingAmount;
            Console.WriteLine(ChargingAmount);
        }

        public override void DrownBattery()
        {
            BatteryCharge -= DrownageAmount;
            Console.WriteLine(DrownageAmount);
        }
    }

}
