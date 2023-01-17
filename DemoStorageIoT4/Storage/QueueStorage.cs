using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace DemoStorageIoT4.Storage
{
    public class QueueStorage : IQueueStorage
    {


        public async Task CreateQueue( string connection,string queueName)
        {
            try
            {
                string connectionString = connection;

                // Instantiate a QueueClient which will be used to manipulate the queue
                Azure.Storage.Queues.QueueClient queueClient = new Azure.Storage.Queues.QueueClient(connectionString, queueName);

                // Create the queue if it doesn't already exist
                //if()
                //{
                //    Console.WriteLine($"Queue '{queueClient.Name}' is  created");
                //}

                if (!await queueClient.ExistsAsync())
                {
                    Console.WriteLine($"Queue '{queueClient.Name}' created");
                    await queueClient.CreateIfNotExistsAsync();
                }
                else
                {
                    Console.WriteLine($"Queue '{queueClient.Name}' exists");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public void DeleteQueue(string connection,string queueName)
        {
            // throw new NotImplementedException();
            string connectionString = connection;

            // Instantiate a QueueClient which will be used to manipulate the queue
            Azure.Storage.Queues.QueueClient queueClient = new Azure.Storage.Queues.QueueClient(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Receive and process 20 messages
                QueueMessage[] receivedMessages = queueClient.ReceiveMessages(20, TimeSpan.FromMinutes(5));

                foreach (QueueMessage message in receivedMessages)
                {
                    // Process (i.e. print) the messages in less than 5 minutes
                    Console.WriteLine($"De-queued message: '{message.Body}'");

                    // Delete the message
                    queueClient.DeleteMessage(message.MessageId, message.PopReceipt);

                }
            }
        }

        public void DequeueMessages(string queueName)
        {
            throw new NotImplementedException();
        }

        public void InsertMessage(string connection,string queueName, string message)
        {
            // throw new NotImplementedException();
            string connectionString = connection;

            // Instantiate a QueueClient which will be used to create and manipulate the queue
            Azure.Storage.Queues.QueueClient queueClient = new Azure.Storage.Queues.QueueClient(connectionString, queueName);

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                queueClient.SendMessage(message);
            }

            Console.WriteLine($"Inserted: {message}");
        }

        public Task QueueAsync(string queueName)
        {
            throw new NotImplementedException();

        }

        public void UpdateMessage(string connection,string queueName,string updatemessage)
        {
            //  throw new NotImplementedException();
            string connectionString = connection;

            // Instantiate a QueueClient which will be used to manipulate the queue
            Azure.Storage.Queues.QueueClient queueClient = new Azure.Storage.Queues.QueueClient(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Get the message from the queue
                QueueMessage[] message = queueClient.ReceiveMessages();


                // Update the message contents
                queueClient.UpdateMessage(message[0].MessageId,
                        message[0].PopReceipt,
                        updatemessage,
                        TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
                    );
            }
        }
    }
}
