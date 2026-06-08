using System;
using System.Collections.Generic;
using System.Linq;

namespace SelfPosRegister
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product { Id = 1, Name = "Tシャツ", Price = 1500 },
                new Product { Id = 2, Name = "ジーンズ", Price = 3990 },
                new Product { Id = 3, Name = "パーカー", Price = 2990 },
                new Product { Id = 4, Name = "靴下", Price = 390 }
            };

            List<Product> cart = new List<Product>();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("====================================");
                Console.WriteLine("      セルフPOSレジシステム");
                Console.WriteLine("====================================");
                Console.WriteLine();

                Console.WriteLine("【商品一覧】");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id}. {product.Name}  ￥{product.Price}");
                }

                Console.WriteLine();
                Console.WriteLine("【現在の購入商品】");

                if (cart.Count == 0)
                {
                    Console.WriteLine("商品がありません");
                }
                else
                {
                    foreach (var item in cart)
                    {
                        Console.WriteLine($"{item.Name} ￥{item.Price}");
                    }
                }

                int total = cart.Sum(x => x.Price);

                Console.WriteLine("------------------------------------");
                Console.WriteLine($"小計：￥{total}");
                Console.WriteLine("------------------------------------");
                Console.WriteLine();

                Console.WriteLine("1. 商品追加");
                Console.WriteLine("2. 商品削除");
                Console.WriteLine("3. 会計");
                Console.WriteLine("0. 終了");
                Console.Write("選択：");

                string menu = Console.ReadLine();

                switch (menu)
                {
                    case "1":
                        Console.Write("追加する商品番号：");

                        if (int.TryParse(Console.ReadLine(), out int addId))
                        {
                            Product addProduct =
                                products.FirstOrDefault(x => x.Id == addId);

                            if (addProduct != null)
                            {
                                cart.Add(addProduct);
                                Console.WriteLine($"{addProduct.Name}を追加しました。");
                            }
                            else
                            {
                                Console.WriteLine("商品番号が存在しません。");
                            }
                        }

                        Pause();
                        break;

                    case "2":
                        if (cart.Count == 0)
                        {
                            Console.WriteLine("削除する商品がありません。");
                            Pause();
                            break;
                        }

                        Console.WriteLine("【購入商品】");

                        for (int i = 0; i < cart.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {cart[i].Name}");
                        }

                        Console.Write("削除する番号：");

                        if (int.TryParse(Console.ReadLine(), out int removeNo))
                        {
                            if (removeNo >= 1 && removeNo <= cart.Count)
                            {
                                Console.WriteLine(
                                    $"{cart[removeNo - 1].Name}を削除しました。");

                                cart.RemoveAt(removeNo - 1);
                            }
                            else
                            {
                                Console.WriteLine("無効な番号です。");
                            }
                        }

                        Pause();
                        break;

                    case "3":
                        if (cart.Count == 0)
                        {
                            Console.WriteLine("商品が登録されていません。");
                            Pause();
                            break;
                        }

                        Console.WriteLine();
                        Console.WriteLine($"合計金額：￥{total}");
                        Console.Write("お預かり金額：");

                        if (int.TryParse(Console.ReadLine(), out int payment))
                        {
                            if (payment < total)
                            {
                                Console.WriteLine("金額が不足しています。");
                            }
                            else
                            {
                                int change = payment - total;

                                Console.Clear();

                                Console.WriteLine("==============================");
                                Console.WriteLine("          レシート");
                                Console.WriteLine("==============================");

                                foreach (var item in cart)
                                {
                                    Console.WriteLine(
                                        $"{item.Name,-10} ￥{item.Price}");
                                }

                                Console.WriteLine("------------------------------");
                                Console.WriteLine($"合計金額   ￥{total}");
                                Console.WriteLine($"お預かり   ￥{payment}");
                                Console.WriteLine($"おつり     ￥{change}");
                                Console.WriteLine("------------------------------");
                                Console.WriteLine("ありがとうございました。");

                                cart.Clear();
                            }
                        }

                        Pause();
                        break;

                    case "0":
                        Console.WriteLine("システムを終了します。");
                        return;

                    default:
                        Console.WriteLine("正しい番号を入力してください。");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Enterキーで続行...");
            Console.ReadLine();
        }
    }
}