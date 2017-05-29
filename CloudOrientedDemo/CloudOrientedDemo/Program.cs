using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudOrientedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var url = "https://Microsoft.com";
            Console.WriteLine("Enter \n\n 1 for Tight loop \n\n 2 for ConstantInterval \n\n 3 for Random Interval \n\n 4 for Progressive backoff");
            var result= Console.ReadLine();
            //if(result == 1 || result ==2 || result ==3 || result ==4)
            switch (result)
            {
                case "1":
                    CallAPIInTightLoop(url, 10);
                    break;
                case "2":
                    CallAPIWithConstantWaitInterval(url, 10, 2);
                    break;

                case "3":
                    CallAPIWithRandomWaitAndTry(url, 10);
                    break;

                case "4":
                    CallAPIWithProgressiveBackOff(url, 10);
                    break;
                default:
                    throw new InvalidOperationException();
                    
                
            }

        }

        private static void CallAPIWithProgressiveBackOff(string url, int noOfRetries)
        {
            int randomInterval =1;
            for (int i = 0; i < noOfRetries; i++)
            {
                randomInterval = randomInterval * 2;
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse webResp = webRequest.GetResponse();
                Thread.Sleep(randomInterval * 1000);
            }
        }

        private static void CallAPIWithRandomWaitAndTry(string url, int noOfRetries)
        {

            var random = new Random();
           
            for (int i = 0; i < noOfRetries; i++)
            {
                int randomInterval = random.Next(1, 10);
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse webResp = webRequest.GetResponse();
                Thread.Sleep(randomInterval * 1000);
            }

        }

        private static void CallAPIWithConstantWaitInterval(string url, int noOfRetries, int waitInterval)
        {
            for (int i = 0; i < noOfRetries; i++)
            {
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse webResp = webRequest.GetResponse();
                Thread.Sleep(waitInterval * 1000);
            }
        }

        private static void CallAPIInTightLoop(string url, int noOfRetries)
        {
            for (int i = 0; i < noOfRetries; i++)
            {
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse webResp = webRequest.GetResponse();
            }
        }
    }
}
