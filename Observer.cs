using System;
using System.Collections.Generic;
using System.Text;


namespace Observer
{
    class Program
    {
        public static void TestRun()
        {

            WeatherStation weatherStation = new WeatherStation();

            NewsAgency agency1 = new NewsAgency("Alpha News Agency");
            weatherStation.Attach(agency1);

            NewsAgency agency2 = new NewsAgency("Omega News Agency");
            weatherStation.Attach(agency2);


            weatherStation.Temperature = 31.2f;
            weatherStation.Temperature = 28.5f;
            weatherStation.Temperature = 33.1f;
            weatherStation.Temperature = 29.2f;

            Console.ReadLine();

        }

        interface ISubject
        {
            void Attach(IObserver observer);
            void Notify();

        }

        class WeatherStation : ISubject
        {
            private List<IObserver> _observers;
            public float Temperature
            {
                get { return _temperature; }
                set { _temperature = value; Notify(); }
            }
            private float _temperature;
            public WeatherStation()
            {
                _observers = new List<IObserver>();
            }

            public void Attach(IObserver observer)
            {
                _observers.Add(observer);
            }

            public void Notify()
            {
                _observers.ForEach(o =>
                {
                    o.Update(this);
                });
            }
        }

        interface IObserver
        {
            void Update(ISubject subject);
        }


        class NewsAgency : IObserver
        {
            public string AgencyName { get; set; }
            public NewsAgency(string name)
            {
                this.AgencyName = name;
            }
            public void Update(ISubject subject)
            {
                //WeatherStation weatherStation = subject as WeatherStation; //?
                //if(weatherStation != null)
                if (subject is WeatherStation weatherStation)
                {
                    Console.WriteLine(String.Format("{0} REPORTING TEMPERATURE {1} DEGREE CECIUS", AgencyName, weatherStation.Temperature));
                    //Console.WriteLine()
                }
            }
        }
    }
}
