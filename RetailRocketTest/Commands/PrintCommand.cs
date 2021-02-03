using RetailRocketTest.Models;
using RetailRocketTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailRocketTest.Commands
{
    class PrintCommand : ICommand
    {
        private string shopId;
        private IMessageReceiver messageReceiver;
        private ShopRepository shopRepository;

        public PrintCommand(IMessageReceiver messageReceiver, ShopRepository shopRepository, string shopId)
        {
            this.messageReceiver = messageReceiver;
            this.shopRepository = shopRepository;
            this.shopId = shopId;
        }


        public void Execute()
        {
            IEnumerable<Product> products = shopRepository.GetProducts(shopId);

            if (products.ToList().Count > 0)
            {
                messageReceiver.WriteCommandResultMessage($"Товары из магазина {shopId}:");

                messageReceiver.WriteCommandResultMessage("Id;Name;ShopId");
                foreach (var p in products)
                {
                    messageReceiver.WriteCommandResultMessage($"{p.Id};{p.Name};{p.Shop.ShopId}");
                }
            }
            else
            {
                messageReceiver.WriteCommandResultMessage("в данном магазине нет товаров");
            }
        }
    }
}
