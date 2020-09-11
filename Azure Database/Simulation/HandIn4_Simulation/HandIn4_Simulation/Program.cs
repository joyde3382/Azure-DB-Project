using HandIn4_Simulation.Controllers;
using HandIn4_Simulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn4_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables
            TradersRepoController mTradersRepoController = new TradersRepoController();
            ProsumerController mProsumerController = new ProsumerController();
            SmartGridsController mSmartGridsController = new SmartGridsController();

            // Clear SmartGrid
            ClearSmartGrid(mSmartGridsController);

            // Clear Trades
            ClearTraders(mTradersRepoController);

            /// Clear Prosumers
            ClearProsumers(mProsumerController);

            /// Populate Prosumers
            PopulateProsumers(mProsumerController);

            // Find Producer and Consumer
            List<Prosumer> mProducerList = mProsumerController.GetProsumerWithProduction("Overproducing").Result.ToList();
            List<Prosumer> mConsumerList = mProsumerController.GetProsumerWithProduction("Underproducing").Result.ToList();

            // Insert Producers and Consumer into SmartGrid object
            SmartGrid mSmartGrid = new SmartGrid();
            mSmartGrid.Producers = mProducerList;
            mSmartGrid.Consumers = mConsumerList;

            // Post SmartGrid to SmartGrid Database
            mSmartGridsController.Post(mSmartGrid).Wait();

            // Get All from SmartGrid
            var AllSmartGrid = mSmartGridsController.GetAllSmartGridsAsync().Result.LastOrDefault();
            var mProducers = AllSmartGrid.Producers;
            var mConsumers = AllSmartGrid.Consumers;

            // Calculate Producers- and Consumers kWhAmount
            var ProducersAmount = 0;
            var ConsumersAmount = 0;
            foreach (var v in mProducers)
            {
                ProducersAmount += v.KWhAmount;
            }
            foreach (var v in mConsumers)
            {
                ConsumersAmount += v.KWhAmount;
            }
            var difference = Math.Abs(ProducersAmount) - Math.Abs(ConsumersAmount);

            Console.WriteLine("\nDifference: " + difference);
            Console.WriteLine("Producers Amount: " + Math.Abs(ProducersAmount));
            Console.WriteLine("Consumers Amount: " + Math.Abs(ConsumersAmount));

            // Let's Trade
            int loopCounter = 0;
            foreach (var consumer in mConsumers)
            {
                loopCounter++;
                Console.WriteLine("\n******************** Consumer " + loopCounter + " *******************\n");
                foreach (var producer in mProducers)
                {
                    if (consumer.KWhAmount == 0) continue;
                    if (producer.KWhAmount == 0) continue;

                    //Console.WriteLine(">> " + producer.Name + " is selling " + Math.Abs(producer.KWhAmount) + " kWh");
                    //Console.WriteLine(">> " + consumer.Name + " is buying " + Math.Abs(consumer.KWhAmount) + " kWh\n");
                    if (Math.Abs(consumer.KWhAmount) < producer.KWhAmount)
                    {
                        producer.KWhAmount += consumer.KWhAmount;
                        Console.WriteLine(">>>> TRADE: " + consumer.Name + " has bought " + Math.Abs(consumer.KWhAmount) + " kWh from " + producer.Name);
                        Trader trader = new Trader()
                        {
                            ProducerId = producer.ProsumerId.ToString(),
                            ConsumerId = consumer.ProsumerId.ToString(),
                            KWhTransferred = Math.Abs(consumer.KWhAmount).ToString(),
                            TransferDate = DateTime.Now
                        };
                        mTradersRepoController.Post(trader).Wait();
                        consumer.KWhAmount = 0;
                        mProsumerController.Put(producer.ProsumerId, producer).Wait();
                        mProsumerController.Put(consumer.ProsumerId, consumer).Wait();
                    }
                    else if (Math.Abs(consumer.KWhAmount) > producer.KWhAmount)
                    {
                        consumer.KWhAmount += producer.KWhAmount;
                        Console.WriteLine(">>>> TRADE: " + consumer.Name + " has bought " + Math.Abs(producer.KWhAmount) + " kWh from " + producer.Name);
                        Trader trader = new Trader()
                        {
                            ProducerId = producer.ProsumerId.ToString(),
                            ConsumerId = consumer.ProsumerId.ToString(),
                            KWhTransferred = producer.KWhAmount.ToString(),
                            TransferDate = DateTime.Now
                        };
                        mTradersRepoController.Post(trader).Wait();
                        producer.KWhAmount = 0;
                        mProsumerController.Put(producer.ProsumerId, producer).Wait();
                        mProsumerController.Put(consumer.ProsumerId, consumer).Wait();
                    }
                    Console.WriteLine(producer.Name + " is selling " + producer.KWhAmount + " and " +
                        consumer.Name + " is buying " + Math.Abs(consumer.KWhAmount));
                }
                if (consumer.KWhAmount != 0)
                {
                    Console.WriteLine("Buyer is missing " + Math.Abs(consumer.KWhAmount) +
                        ", but he somehow manages to get free kWh from the powerplant (happy ending)");
                    consumer.KWhAmount = 0;
                }
            }

            // Remove Consumers and Producers if their kWhAmount equals 0.
            foreach (var producer in mProducers.ToList())
            {
                if (producer.KWhAmount == 0)
                {
                    mProducers.Remove(producer);
                }
            }
            foreach (var consumer in mConsumers.ToList())
            {
                if (consumer.KWhAmount == 0)
                {
                    mConsumers.Remove(consumer);
                }
            }

            // Post the SmartGrid
            mSmartGrid.Producers = mProducers;
            mSmartGrid.Consumers = mConsumers;
            mSmartGridsController.Post(mSmartGrid).Wait();

            // Check difference now and how much involved powerplant was as buy/sell
            if (difference > 0)
            {
                Console.WriteLine("\nSold " + difference + " kWh to powerplant");
            }
            else
            {
                Console.WriteLine("\nBought " + Math.Abs(difference) + " kWh from powerplant");
            }

            Console.WriteLine("\n\nProgram has finished ...");
            Console.ReadLine();

        }

        private static void PopulateProsumers(ProsumerController prosumerController)
        {
            // Populate 33x Household Prosumer
            int householdSize = 33;
            Console.WriteLine("Populating Private Prosumers ...");
            for (int i = 0; i < householdSize; i++)
            {
                Random random = new Random();
                string name = "household" + (i + 1);

                Prosumer prosumer = new Prosumer();
                prosumer.Name = name;
                prosumer.prosumerType = Prosumer.ProsumerType.Private;
                prosumer.KWhAmount = random.Next(-200, 200);

                prosumerController.Post(prosumer).Wait();
                Console.WriteLine("Private Prosumer " + (i+1) + "/" + householdSize + " Completed");

            }

            // Populate 12x Company Prosumer
            int companySize = 12;
            Console.WriteLine("\nPopulating Company Prosumers ...");
            for (int i = 0; i < companySize; i++)
            {
                Random random = new Random();
                string name = "company" + (i + 1);

                Prosumer prosumer = new Prosumer();
                prosumer.Name = name;
                prosumer.prosumerType = Prosumer.ProsumerType.Company;
                prosumer.KWhAmount = random.Next(-200, 200);

                prosumerController.Post(prosumer).Wait();

                Console.WriteLine("Company Prosumer " + (i+1) + "/" + companySize + " Completed");

            }
        }

        private static void ClearProsumers(ProsumerController prosumerController)
        {
            List<Prosumer> GetAllProsumers = prosumerController.GetAllProsumersAsync().Result.ToList();
            int counter = 0;
            foreach (var v in GetAllProsumers)
            {
                counter++;
                prosumerController.Delete(v.ProsumerId).Wait();
                Console.WriteLine("Deleted " + counter + "/" + GetAllProsumers.Count + " Prosumer with id = " + v.ProsumerId);
            }
        }

        private static void ClearSmartGrid(SmartGridsController smartGridsController)
        {
            List<SmartGrid> GetAllSmartGrid = smartGridsController.GetAllSmartGridsAsync().Result.ToList();
            int counter = 0;
            foreach (var v in GetAllSmartGrid)
            {
                counter++;
                smartGridsController.Delete(v.SmartGridId).Wait();
                Console.WriteLine("Deleted " + counter + "/" + GetAllSmartGrid.Count + " SmartGrid with id = " + v.SmartGridId);
            }
        }

        private static void ClearTraders(TradersRepoController tradersRepoController)
        {
            List<Trader> GetAllTraders = tradersRepoController.GetAllTradersAsync().Result.ToList();
            int counter = 0;
            foreach(var v in GetAllTraders)
            {
                counter++;
                tradersRepoController.Delete(v.Id).Wait();
                Console.WriteLine("Deleted " + counter + "/" + GetAllTraders.Count + " Trader with id = " + v.Id);
            }
        }



    }
}
