using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Entities.Product;

namespace SkyVisionStore.DataAccess.Seed
{
    public static class DbSeeder
    {
        public static void SeedProducts()
        {
            using var context = new SkyVisionStoreContext();

            context.Database.Migrate();

            if (context.Products.Any())
            {
                return;
            }

            var now = DateTime.UtcNow;

            var products = new List<Product>
            {
                new Product
                {
                    Title = "Death Stranding 2: On the Beach",
                    Slug = "death-stranding-2-on-the-beach",
                    Platform = "PlayStation",
                    Genre = "Adventure",
                    Price = 70m,
                    Image = "https://consogame.com/photos/products/2040/image.jpg",
                    RecommendedImage = "https://i.pinimg.com/736x/f8/ba/b2/f8bab20706c51dd15b5748048817009e.jpg",
                    Region = "Global",
                    Description = "Атмосферное приключение с кинематографичным сюжетом, исследованием большого мира и доставкой грузов в опасных условиях.",
                    Requirements = BuildRequirements(
                        "PS5",
                        "Не менее 100 GB свободного места",
                        "Интернет для активации"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Euro Truck Simulator 2",
                    Slug = "euro-truck-simulator-2",
                    Platform = "Steam",
                    Genre = "Simulation",
                    Price = 10m,
                    OldPrice = 20m,
                    Discount = 50,
                    Image = "https://i.pinimg.com/736x/5e/2d/a0/5e2da0c309e30247e50e28830be225c9.jpg",
                    RecommendedImage = "https://i.pinimg.com/736x/f7/f3/b2/f7f3b2a97d948ccc0070d873af29032a.jpg",
                    Region = "CIS",
                    Description = "Симулятор дальнобойщика, в котором игрок перевозит грузы по дорогам Европы, развивает собственную транспортную компанию и покупает новые грузовики.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "8 GB RAM",
                        "GTX 1050 Ti / RX 570",
                        "25 GB свободного места"
                    ),
                    IsPopular = false,
                    IsNew = false,
                    IsUpcoming = true,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Resident Evil Requiem",
                    Slug = "resident-evil-requiem",
                    Platform = "Steam",
                    Genre = "Horror",
                    Price = 70m,
                    Image = "https://i.pinimg.com/736x/2f/26/02/2f26026454beb1ff40d72f5d19b3b042.jpg",
                    RecommendedImage = "https://i.pinimg.com/736x/e0/bd/74/e0bd7454bb4a44c5479986b29452407e.jpg",
                    Region = "Global",
                    Description = "Мрачный survival horror с напряжённой атмосферой, ограниченными ресурсами и исследованием опасных локаций.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "16 GB RAM",
                        "RTX 3060 / RX 6700 XT",
                        "70 GB свободного места"
                    ),
                    IsPopular = false,
                    IsNew = true,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Forza Horizon 5",
                    Slug = "forza-horizon-5",
                    Platform = "Xbox",
                    Genre = "Racing",
                    Price = 15m,
                    OldPrice = 60m,
                    Discount = 75,
                    Image = "https://i.pinimg.com/736x/12/e0/c9/12e0c9c80a81c3423132cb532283b2e9.jpg",
                    RecommendedImage = "https://i.pinimg.com/1200x/c6/77/70/c67770e8909e74297a6f0ce9092e64c1.jpg",
                    Region = "EU",
                    Description = "Аркадные гонки в огромном открытом мире с сотнями автомобилей, сезонными событиями и множеством соревнований.",
                    Requirements = BuildRequirements(
                        "Xbox Series X/S или Windows 10/11",
                        "16 GB RAM",
                        "GTX 1070 / RX 590",
                        "110 GB свободного места"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Cyberpunk 2077",
                    Slug = "cyberpunk-2077",
                    Platform = "Epic Games",
                    Genre = "Action RPG",
                    Price = 60m,
                    Image = "https://i.pinimg.com/736x/14/c8/96/14c896e0730044e222018d65a338eab5.jpg",
                    Region = "Global",
                    Description = "Ролевая экшен-игра в мрачном мегаполисе будущего с открытым миром, прокачкой героя и вариативными заданиями.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "16 GB RAM",
                        "RTX 2060 / RX 5700 XT",
                        "70 GB SSD"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Silent Hill 2",
                    Slug = "silent-hill-2",
                    Platform = "Steam",
                    Genre = "Horror",
                    Price = 70m,
                    Image = "https://i.pinimg.com/736x/f8/0e/e0/f80ee0158f3ed64b6cc118e4db7e19fc.jpg",
                    Region = "Global",
                    Description = "Психологический хоррор с тяжёлой атмосферой, глубоким сюжетом и исследованием окутанного туманом города.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "16 GB RAM",
                        "RTX 2070 / RX 6800",
                        "50 GB SSD"
                    ),
                    IsPopular = false,
                    IsNew = true,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Kingdom Come: Deliverance 2",
                    Slug = "kingdom-come-deliverance-2",
                    Platform = "Steam",
                    Genre = "Action RPG",
                    Price = 45m,
                    OldPrice = 60m,
                    Discount = 25,
                    Image = "https://i.pinimg.com/1200x/6d/95/3d/6d953dcb3dc5dab732b1890a5708c504.jpg",
                    Region = "Global",
                    Description = "Ролевая игра в средневековом сеттинге с реалистичными боями, открытым миром и сильным упором на сюжет.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "16 GB RAM",
                        "RTX 2060 Super / RX 6700",
                        "100 GB SSD"
                    ),
                    IsPopular = false,
                    IsNew = false,
                    IsUpcoming = true,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "STAR WARS Jedi: Survivor",
                    Slug = "star-wars-jedi-survivor",
                    Platform = "Origin",
                    Genre = "Action Adventure",
                    Price = 70m,
                    Image = "https://i.pinimg.com/736x/b1/27/7d/b1277d36831b0663594a300b7780cad0.jpg",
                    Region = "Global",
                    Description = "Приключенческий экшен во вселенной Star Wars с динамичными боями на световых мечах и исследованием разных планет.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "16 GB RAM",
                        "RTX 2070 / RX 6700 XT",
                        "155 GB свободного места"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Borderlands 4",
                    Slug = "borderlands-4",
                    Platform = "Xbox",
                    Genre = "FPS",
                    Price = 49m,
                    Image = "https://i.pinimg.com/736x/9d/0a/c5/9d0ac5299401ef875056e47249db4517.jpg",
                    Region = "Global",
                    Description = "Безумный кооперативный шутер с огромным количеством оружия, ярким стилем и динамичными перестрелками.",
                    Requirements = BuildRequirements(
                        "Xbox Series X/S",
                        "Не менее 80 GB свободного места",
                        "Интернет для сетевой игры"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Among Us",
                    Slug = "among-us",
                    Platform = "Steam",
                    Genre = "Party",
                    Price = 2m,
                    OldPrice = 5m,
                    Discount = 60,
                    Image = "https://i.pinimg.com/736x/1e/06/5c/1e065cb0cfb8e593a221589d1ed7f315.jpg",
                    Region = "CIS",
                    Description = "Многопользовательская party-игра на дедукцию, где экипаж пытается вычислить предателя среди участников.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "4 GB RAM",
                        "Встроенная графика Intel HD",
                        "1 GB свободного места"
                    ),
                    IsPopular = false,
                    IsNew = false,
                    IsUpcoming = true,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Valheim",
                    Slug = "valheim",
                    Platform = "Steam",
                    Genre = "Survival",
                    Price = 20m,
                    Image = "https://i.pinimg.com/736x/a3/cd/67/a3cd67e342c73321168e29c797ae924f.jpg",
                    Region = "Global",
                    Description = "Выживание в мире скандинавской мифологии с исследованием, строительством базы, крафтом и боями с боссами.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "8 GB RAM",
                        "GTX 1060 / RX 580",
                        "25 GB свободного места"
                    ),
                    IsPopular = false,
                    IsNew = true,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Red Dead Redemption 2",
                    Slug = "red-dead-redemption-2",
                    Platform = "Xbox",
                    Genre = "Action Adventure",
                    Price = 15m,
                    OldPrice = 60m,
                    Discount = 75,
                    Image = "https://i.pinimg.com/736x/d1/13/9b/d1139b1ee12965f315652a3eb970aa28.jpg",
                    Region = "EU",
                    Description = "Масштабный приключенческий экшен о жизни банды на Диком Западе с открытым миром и сильной сюжетной кампанией.",
                    Requirements = BuildRequirements(
                        "Xbox One / Xbox Series X/S",
                        "Не менее 120 GB свободного места"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Phasmophobia",
                    Slug = "phasmophobia",
                    Platform = "Steam",
                    Genre = "Horror",
                    Price = 20m,
                    Image = "https://i.pinimg.com/736x/87/00/ce/8700ce7385987634bdf10d40c35f2f75.jpg",
                    Region = "Global",
                    Description = "Кооперативный хоррор, в котором команда исследует паранормальные явления, собирает улики и определяет тип призрака.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "8 GB RAM",
                        "GTX 970 / RX 580",
                        "16 GB свободного места"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Watch_Dogs 2",
                    Slug = "watch-dogs-2",
                    Platform = "Uplay",
                    Genre = "Action",
                    Price = 10m,
                    OldPrice = 30m,
                    Discount = 67,
                    Image = "https://i.pinimg.com/736x/f2/57/ea/f257eaa23a2068f06994093a134fed7a.jpg",
                    Region = "Global",
                    Description = "Экшен в открытом мире, где игрок использует взлом, гаджеты и скрытность для выполнения миссий в цифровом Сан-Франциско.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "8 GB RAM",
                        "GTX 970 / RX 470",
                        "50 GB свободного места"
                    ),
                    IsPopular = false,
                    IsNew = true,
                    IsUpcoming = false,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "Call of Duty: Black Ops 6",
                    Slug = "call-of-duty-black-ops-6",
                    Platform = "Battle.net",
                    Genre = "FPS",
                    Price = 28m,
                    OldPrice = 70m,
                    Discount = 60,
                    Image = "https://i.pinimg.com/736x/1b/25/2c/1b252ca27277df4a969dd504753bdb49.jpg",
                    Region = "Global",
                    Description = "Современный шутер от первого лица с насыщенной сюжетной кампанией, мультиплеером и быстрым темпом боя.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "16 GB RAM",
                        "RTX 2060 / RX 6600 XT",
                        "120 GB SSD"
                    ),
                    IsPopular = false,
                    IsNew = false,
                    IsUpcoming = true,
                    CreatedAt = now
                },

                new Product
                {
                    Title = "World of Warcraft: Midnight",
                    Slug = "world-of-warcraft-midnight",
                    Platform = "Battle.net",
                    Genre = "MMORPG",
                    Price = 20m,
                    OldPrice = 25m,
                    Discount = 20,
                    Image = "https://i.pinimg.com/736x/56/d5/0a/56d50aa7a3cc127dd135b66eae0b436c.jpg",
                    Region = "Global",
                    Description = "Новое дополнение для культовой MMORPG с новыми зонами, рейдами, подземельями и развитием истории Азерота.",
                    Requirements = BuildRequirements(
                        "Windows 10/11",
                        "8 GB RAM",
                        "GTX 1080 / RX Vega 64",
                        "128 GB SSD"
                    ),
                    IsPopular = true,
                    IsNew = false,
                    IsUpcoming = false,
                    CreatedAt = now
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static string BuildRequirements(params string[] requirements)
        {
            return string.Join(Environment.NewLine, requirements);
        }
    }
}