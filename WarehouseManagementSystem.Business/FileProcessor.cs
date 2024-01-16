using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class FileProcessor : IDisposable
    {
        private readonly OrderProcessor processor;

        public FileProcessor(OrderProcessor processor) 
        {
            this.processor = processor;
            this.processor.OrderCreatedInherit += Processor_OrderCreated;
        }

        private void Processor_OrderCreated(object? sender, OrderCreatedEventArgs e)
        {
            Console.WriteLine($"Processed {e.Order?.OrderNumber}");
        }

        public void Start()
        {
            var data = File.ReadAllText("orders.json");
            var orders = JsonSerializer.Deserialize<Order[]>(data);

            foreach (var item in orders)
            {
                this.processor.Process(item);
            }
        }



        public void Dispose()
        {
            this.processor.OrderCreatedInherit -= Processor_OrderCreated;
        }
    }
}
