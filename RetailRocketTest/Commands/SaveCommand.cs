using RetailRocketTest.Models;
using RetailRocketTest.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailRocketTest.Commands
{
    class SaveCommand : ICommand
    {
        private string shopId;
        private string url;
        private IMessageReceiver messageReceiver;
        private ShopRepository shopRepository;
        private YmlParser ymlParser;

        public SaveCommand(IMessageReceiver messageReceiver, ShopRepository shopRepository, YmlParser ymlParser, string shopId, string url)
        {
            this.messageReceiver = messageReceiver;
            this.shopRepository = shopRepository;
            this.ymlParser = ymlParser;
            this.shopId = shopId;
            this.url = url;
        }


        public void Execute()
        {
            Shop shop = shopRepository.GetShop(shopId);
            IEnumerable<Product> products = ymlParser.GetProducts(url, shop);
            try
            {
                if (products != null)
                {
                    shopRepository.AddProducts(products);
                    messageReceiver.WriteCommandResultMessage($"Товары из файла {url} успешно добавлены в базу данных");
                }
                else
                {
                    messageReceiver.WriteCommandResultMessage($"В документе {url} нет товаров");
                }
            }
            catch(Exception ex)
            {
                messageReceiver.WriteCommandResultMessage("Не удалось добавить товары в базу данных");
            }
            

            
        }
    }
}
